using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FacilityList : MonoBehaviour
{
    [SerializeField] RectTransform prefab = null;                                         //施設表示用ボタン
    [SerializeField] GameObject mapImage;

    public static string facilityNum;                                                     //施設ナンバー保持用変数
    public static string sceneNum;                                                        //シーンナンバー保持用変数

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(getFacilitys());

        result();
    }

    //施設リスト取得処理
    private IEnumerator getFacilitys()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        //トップ画面から遷移した場合と変更対象施設検索画面から遷移した場合で送信する値を変える
        if (sceneNum == "1")
        {
            dic.Add("prefecture", Top.prefectureValue);
            dic.Add("city", Top.cityValue);
            dic.Add("word1", Top.word1Value);
            if (Top.word2Value == null)
            {
                Top.word2Value = "";
            }
            dic.Add("word2", Top.word2Value);
            dic.Add("now", Top.nowValue);
        }
        else if (sceneNum == "2")
        {
            mapImage.gameObject.SetActive(false);
            dic.Add("prefecture", UpSearch.prefectureValue);
            dic.Add("city", UpSearch.cityValue);
            dic.Add("word1", UpSearch.word1Value);
            if (UpSearch.word2Value == null)
            {
                UpSearch.word2Value = "";
            }
            dic.Add("word2", UpSearch.word2Value);
            dic.Add("now", "");
        }

        //obj.ServerAddress = "http://localhost/unity/get.php";                     //ローカル用アドレス
        obj.ServerAddress = "http://shigotoyo.starfree.jp/opensearch/get.php";    //Web用アドレス

        obj.Dic = dic;

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    //検索条件に基づいて施設ボタンを表示
    private void result()
    {
        var facilitys = JsonUtility.FromJson<Facititys>(DbProcess.returnText);


        foreach (var facility in facilitys.button)
        {
            Debug.Log(facility.name);

            var item = GameObject.Instantiate(prefab) as RectTransform;
            item.SetParent(transform, false);

            var text = item.GetComponentInChildren<Text>();
            item.name = facility.content_no;
            text.text = facility.name;

        }
    }

    //戻るボタン押下時処理
    public void backScene()
    {
        //遷移してきたシーンに戻る
        if (sceneNum == "1")
        {
            SceneManager.LoadScene("top");
        }
        else if (sceneNum == "2")
        {
            SceneManager.LoadScene("upSearch");
        }

    }
}


[Serializable]

public class Facitity
{
    public string content_no;
    public string name;
}

[Serializable]
public class Facititys
{
    public List<Facitity> button;
}
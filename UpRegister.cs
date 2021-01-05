using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpRegister : MonoBehaviour
{
    [SerializeField] public Text text;                                                       //登録可否テキスト

    IEnumerator Start()
    {
        yield return StartCoroutine(updateFacility());

        result();
    }

    private IEnumerator updateFacility()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("content_no", FacilityList.facilityNum);
        dic.Add("prefecture", UpInput.prefectureValue);
        dic.Add("city", UpInput.cityValue);
        dic.Add("name", UpInput.nameValue);
        dic.Add("url", UpInput.urlValue);
        dic.Add("post", UpInput.postValue);
        dic.Add("address", UpInput.addressValue);
        dic.Add("tel", UpInput.telValue);
        dic.Add("open1", UpInput.open1Value);
        dic.Add("close1", UpInput.close1Value);
        dic.Add("open2", UpInput.open2Value);
        dic.Add("close2", UpInput.close2Value);
        dic.Add("start", UpInput.startValue);
        dic.Add("end", UpInput.endValue);
        dic.Add("close", UpInput.closeValue);
        dic.Add("remark", UpInput.remarkValue);
        dic.Add("tag", UpInput.tagValue);

        //DbProcess.ServerAddress = "http://localhost/unity/update.php";                      //ローカル用アドレス
        obj.ServerAddress = "http://shigotoyo.starfree.jp/opensearch/update.php";     //Web用アドレス

        obj.Dic = dic;

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private void result()
    {
        if (DbProcess.returnText == "")
        {
            text.text = "変更できませんでした";
        }
        else
        {
            text.text = DbProcess.returnText;
        }


    }

    public void topScene()
    {
        SceneManager.LoadScene("top");
    }
}



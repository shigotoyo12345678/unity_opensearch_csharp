using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FacilityDetail : MonoBehaviour
{

    public Text prefectureText;                                                               //県表示テキスト
    public Text cityText;                                                                     //市表示テキスト
    public Text nameText;                                                                     //施設名表示テキスト
    public Text urlText;                                                                      //URL表示テキスト
    public Text postText;                                                                     //郵便番号表示テキスト
    public Text addressText;                                                                  //住所表示テキスト
    public Text telText;                                                                      //電話番号表示テキスト
    public Text WeekdaysText;                                                                 //特別営業時間（平日）表示テキスト
    public Text WeekendText;                                                                  //特別営業時間（土日祝）表示テキスト
    public Text periodText;                                                                   //特別営業適用期間表示テキスト
    public Text remarkText;                                                                   //備考表示テキスト
    public Text closeText1;                                                                   //一時閉店テキスト１
    public Text closeText2;                                                                   //一時閉店テキスト２
    public Text closeText3;                                                                   //一時閉店テキスト３

    public static string addressStr;                                                          //Googleマップ検索用変数        

    private static string webStr;                                                             //Webサイト検索用変数

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(getFacilityDetail());

        result();
    }

    private IEnumerator getFacilityDetail()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("number", FacilityList.facilityNum);

        //DbProcess.ServerAddress = "http://localhost/unity/getFacility.php";                    //ローカル用アドレス
        obj.ServerAddress = "http://shigotoyo.starfree.jp/opensearch/getFacility.php";     //Web用アドレス

        obj.Dic = dic;

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    //データベースから施設詳細を取得して各テキストに設定
    private void result()
    {
        Debug.Log(DbProcess.returnText);

        string a = DbProcess.returnText; ;

        string b = a.Remove(0, 1);
        string d = b.Remove(b.Length - 1, 1); ///文字列の調整

        var facilityStr = JsonUtility.FromJson<FacilityValue>(d);

        prefectureText.text = facilityStr.prefecture;
        cityText.text = facilityStr.city;
        nameText.text = facilityStr.name;
        urlText.text = facilityStr.url;
        postText.text = facilityStr.post;
        addressText.text = facilityStr.address;
        telText.text = facilityStr.tel;
        if (facilityStr.closeflg == "1")
        {
            closeText1.gameObject.SetActive(true);
            closeText2.gameObject.SetActive(false);
            closeText3.gameObject.SetActive(false);
            WeekdaysText.gameObject.SetActive(false);
            WeekendText.gameObject.SetActive(false);
        }
        else if (facilityStr.closeflg == "0")
        {
            WeekdaysText.text = facilityStr.open1 + "～" + facilityStr.close1;
            WeekendText.text = facilityStr.open2 + "～" + facilityStr.close2;
        }
        periodText.text = facilityStr.start + "～" + facilityStr.end;
        remarkText.text = facilityStr.remark;

        addressStr = facilityStr.address;
        webStr = facilityStr.url;
    }

    //戻るボタン押下時処理
    public void backScene()
    {
        SceneManager.LoadScene("facilityList");
    }

    //Googleマップボタン押下時処理
    public void openMap()
    {
        string url = "https://www.google.com/maps/search/?api=1&query=" + addressStr;

        Application.OpenURL(url);
    }

    //URL表示用テキスト押下時処理
    public void openWeb()
    {
        Application.OpenURL(webStr);
    }
}

[System.Serializable]
public class FacilityValue
{
    public string prefecture;
    public string city;
    public string name;
    public string url;
    public string post;
    public string address;
    public string tel;
    public string open1;
    public string close1;
    public string open2;
    public string close2;
    public string start;
    public string end;
    public string closeflg;
    public string remark;
    public string tag;
}
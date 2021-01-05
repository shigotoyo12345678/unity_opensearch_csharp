using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpInput : MonoBehaviour
{
    [SerializeField] public Dropdown prefecture;                     //県選択セレクトボックス
    [SerializeField] public Dropdown city;                           //市選択セレクトボックス
    [SerializeField] public InputField name;                         //施設名入力インプットフィールド
    [SerializeField] public InputField url;                          //URL入力インプットフィールド
    [SerializeField] public InputField post;                         //郵便番号入力インプットフィールド
    [SerializeField] public InputField address;                      //住所入力インプットフィールド
    [SerializeField] public InputField tel;                          //電話番号入力インプットフィールド
    [SerializeField] public Toggle close;                            //一時閉店チェックボックス
    [SerializeField] public Dropdown one;                            //特別営業開始時間（平日）選択セレクトボックス
    [SerializeField] public Dropdown two;
    [SerializeField] public Dropdown three;                          //特別営業終了時間（平日）選択セレクトボックス
    [SerializeField] public Dropdown four;
    [SerializeField] public Dropdown five;                           //特別営業開始時間（土日祝）選択セレクトボックス
    [SerializeField] public Dropdown six;
    [SerializeField] public Dropdown seven;                          //特別営業終了時間（土日祝）選択セレクトボックス
    [SerializeField] public Dropdown eight;
    [SerializeField] public Dropdown start1;                         //特別営業開始日時選択セレクトボックス
    [SerializeField] public Dropdown start2;
    [SerializeField] public Dropdown start3;
    [SerializeField] public Dropdown end1;                           //特別営業終了日時選択セレクトボックス
    [SerializeField] public Dropdown end2;
    [SerializeField] public Dropdown end3;
    [SerializeField] public InputField remark;                       //備考入力インプットフィールド
    [SerializeField] public InputField tag;                          //タグ入力インプットフィールド

    [SerializeField] public Button button;                           //現在ボタン

    //値保持用変数

    public static int sceneNo = 0;                                   //シーンナンバー

    public static string prefectureValue;                            //県
    public static string cityValue;                                  //市
    public static string nameValue;                                  //施設名
    public static string urlValue;                                   //URL;
    public static string postValue;                                  //郵便番号
    public static string addressValue;                               //住所
    public static string telValue;                                   //電話番号
    public static string closeValue;                                 //一時閉店チェック
    public static string open1Value;                                 //特別営業開始時間（平日）
    public static string close1Value;                                //特別営業終了時間（平日）
    public static string open2Value;                                 //特別営業開始時間（土日祝）
    public static string close2Value;                                //特別営業終了時間（土日祝）
    public static string startValue;                                 //特別営業適用開始日時
    public static string endValue;                                   //特別営業適用終了日時
    public static string remarkValue;                                //備考
    public static string tagValue;                                   //タグ

    // Start is called before the first frame update
    IEnumerator Start()
    {

        var ins = new Common();
        ins.prefectureOptionSet(prefecture, city, Common.prefectures);
        ins.timeSet(one, two, three, four, five, six, seven, eight, start2, end2, start3, end3);

        //変更対象施設を入力した場合
        if (sceneNo == 0)
        {
            yield return StartCoroutine(getFacilityDetail());

            StartCoroutine(result());
        }
        //変更内容確認画面から戻るボタンで遷移した場合は一度入力した値を再設定する
        else if (sceneNo == 1)
        {
            prefecture.value = System.Array.IndexOf(Common.prefectures, prefectureValue) + 1;
            Common.citySet(System.Array.IndexOf(Common.prefectures, prefectureValue) + 1, city, cityValue);
            name.text = nameValue;
            url.text = urlValue;
            post.text = postValue;
            address.text = addressValue;
            tel.text = telValue;
            if (closeValue == "0")
            {
                close.isOn = false;
            }
            else if (closeValue == "1")
            {
                close.isOn = true;
            }

            var ins3 = new Common();

            ins3.hourSet(one, open1Value.Substring(0, 2));
            ins3.minuteSet(two, open1Value.Substring(3, 2));
            ins3.hourSet(three, close1Value.Substring(0, 2));
            ins3.minuteSet(four, close1Value.Substring(3, 2));
            ins3.hourSet(five, open2Value.Substring(0, 2));
            ins3.minuteSet(six, open2Value.Substring(3, 2));
            ins3.hourSet(seven, close2Value.Substring(0, 2));
            ins3.minuteSet(eight, close2Value.Substring(3, 2));

            ins3.yearSet(start1, startValue.Substring(0, 4));
            ins3.monthSet(start2, startValue.Substring(5, 2));
            ins3.daySet(start3, startValue.Substring(8, 2));
            ins3.yearSet(end1, endValue.Substring(0, 4));
            ins3.monthSet(end2, endValue.Substring(5, 2));
            ins3.daySet(end3, endValue.Substring(8, 2));

            remark.text = remarkValue;
            tag.text = tagValue;
        }



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

    private IEnumerator result()
    {
        string a = DbProcess.returnText;

        string b = a.Remove(0, 1);
        string d = b.Remove(b.Length - 1, 1); ///文字列の調整

        var facilityStr = JsonUtility.FromJson<facilityValue2>(d);

        // facilityNum = facilityStr.number;
        prefecture.value = System.Array.IndexOf(Common.prefectures, facilityStr.prefecture) + 1;
        citySet(prefecture.value, city, facilityStr.city);
        name.text = facilityStr.name;
        url.text = facilityStr.url;
        post.text = facilityStr.post;
        address.text = facilityStr.address;
        tel.text = facilityStr.tel;
        if (facilityStr.closeflg == "0")
        {
            close.isOn = false;
        }
        else if (facilityStr.closeflg == "1")
        {
            close.isOn = true;
        }
        hourSet(one, facilityStr.open1.Substring(0, 2));
        minuteSet(two, facilityStr.open1.Substring(3, 2));
        hourSet(three, facilityStr.close1.Substring(0, 2));
        minuteSet(four, facilityStr.close1.Substring(3, 2));
        hourSet(five, facilityStr.open2.Substring(0, 2));
        minuteSet(six, facilityStr.open2.Substring(3, 2));
        hourSet(seven, facilityStr.close2.Substring(0, 2));
        minuteSet(eight, facilityStr.close2.Substring(3, 2));

        yearSet(start1, facilityStr.start.Substring(0, 4));
        monthSet(start2, facilityStr.start.Substring(5, 2));
        daySet(start3, facilityStr.start.Substring(8, 2));
        yearSet(end1, facilityStr.end.Substring(0, 4));
        monthSet(end2, facilityStr.end.Substring(5, 2));
        daySet(end3, facilityStr.end.Substring(8, 2));

        remark.text = facilityStr.remark;
        tag.text = facilityStr.tag;

        yield return null;
    }

    //県ドロップダウンを変えた時の処理
    public void chengePrefecture()
    {
        var ins = new Common();
        ins.cityOptionSet(prefecture, city);
    }

    //変更内容確認ボタン押下時処理
    public void verificationScene()
    {

        prefectureValue = prefecture.options[prefecture.value].text;
        cityValue = city.options[city.value].text;
        nameValue = name.text;
        urlValue = url.text;
        if (post.text != "")
        {
            postValue = post.text;
        }
        else
        {
            postValue = "";
        }
        addressValue = address.text;
        if (tel.text != "")
        {
            telValue = tel.text;
        }
        else
        {
            telValue = "";
        }
        if (close.isOn == true)
        {
            closeValue = "1";
        }
        else if (close.isOn == false)
        {
            closeValue = "0";
        }
        open1Value = one.options[one.value].text + ":" + two.options[two.value].text;
        close1Value = three.options[three.value].text + ":" + four.options[four.value].text;
        open2Value = five.options[five.value].text + ":" + six.options[six.value].text;
        close2Value = seven.options[seven.value].text + ":" + eight.options[eight.value].text;
        startValue = start1.options[start1.value].text + "-" + start2.options[start2.value].text + "-" + start3.options[start3.value].text;
        endValue = end1.options[end1.value].text + "-" + end2.options[end2.value].text + "-" + end3.options[end3.value].text;
        remarkValue = remark.text;
        tagValue = tag.text;

        DateTime startDate = DateTime.Parse(start1.options[start1.value].text + "-" + start2.options[start2.value].text + "-" + start3.options[start3.value].text);
        DateTime endDate = DateTime.Parse(end1.options[end1.value].text + "-" + end2.options[end2.value].text + "-" + end3.options[end3.value].text);

        if (startDate <= endDate)
        {
        }
        else
        {
            button.enabled = false;
        }

        //入力、選択チェック
        if (prefectureValue == "県を選択してください" || cityValue == "市を選択してください" || nameValue == "" || telValue == "")
        {
            button.enabled = false;

        }
        else
        {
            SceneManager.LoadScene("upVerification");
        }
    }

    public void backScene()
    {
        SceneManager.LoadScene("facilityList");
    }

    #region 市ドロップダウン設定関数
    public void citySet(int prefectureInt, Dropdown city, string cityText)
    {
        switch (prefectureInt)
        {
            case 1:
                city.value = System.Array.IndexOf(Common.hokkaido, cityText) + 1;
                break;
            case 2:
                city.value = System.Array.IndexOf(Common.aomori, cityText) + 1;
                break;
            case 3:
                city.value = System.Array.IndexOf(Common.iwate, cityText) + 1;
                break;
            case 4:
                city.value = System.Array.IndexOf(Common.miyagi, cityText) + 1;
                break;
            case 5:
                city.value = System.Array.IndexOf(Common.akita, cityText) + 1;
                break;
            case 6:
                city.value = System.Array.IndexOf(Common.yamagata, cityText) + 1;
                break;
            case 7:
                city.value = System.Array.IndexOf(Common.hukushima, cityText) + 1;
                break;
            case 8:
                city.value = System.Array.IndexOf(Common.ibaraki, cityText) + 1;
                break;
            case 9:
                city.value = System.Array.IndexOf(Common.totigi, cityText) + 1;
                break;
            case 10:
                city.value = System.Array.IndexOf(Common.gunma, cityText) + 1;
                break;
            case 11:
                city.value = System.Array.IndexOf(Common.saitama, cityText) + 1;
                break;
            case 12:
                city.value = System.Array.IndexOf(Common.chiba, cityText) + 1;
                break;
            case 13:
                city.value = System.Array.IndexOf(Common.tokyo, cityText) + 1;
                break;
            case 14:
                city.value = System.Array.IndexOf(Common.kanagawa, cityText) + 1;
                break;
            case 15:
                city.value = System.Array.IndexOf(Common.niigata, cityText) + 1;
                break;
            case 16:
                city.value = System.Array.IndexOf(Common.toyama, cityText) + 1;
                break;
            case 17:
                city.value = System.Array.IndexOf(Common.ishikawa, cityText) + 1;
                break;
            case 18:
                city.value = System.Array.IndexOf(Common.hukui, cityText) + 1;
                break;
            case 19:
                city.value = System.Array.IndexOf(Common.yamanashi, cityText) + 1;
                break;
            case 20:
                city.value = System.Array.IndexOf(Common.nagano, cityText) + 1;
                break;
            case 21:
                city.value = System.Array.IndexOf(Common.gihu, cityText) + 1;
                break;
            case 22:
                city.value = System.Array.IndexOf(Common.shizuoka, cityText) + 1;
                break;
            case 23:
                city.value = System.Array.IndexOf(Common.aichi, cityText) + 1;
                break;
            case 24:
                city.value = System.Array.IndexOf(Common.mie, cityText) + 1;
                break;
            case 25:
                city.value = System.Array.IndexOf(Common.shiga, cityText) + 1;
                break;
            case 26:
                city.value = System.Array.IndexOf(Common.kyouto, cityText) + 1;
                break;
            case 27:
                city.value = System.Array.IndexOf(Common.osaka, cityText) + 1;
                break;
            case 28:
                city.value = System.Array.IndexOf(Common.hyougo, cityText) + 1;
                break;
            case 29:
                city.value = System.Array.IndexOf(Common.nara, cityText) + 1;
                break;
            case 30:
                city.value = System.Array.IndexOf(Common.wakayama, cityText) + 1;
                break;
            case 31:
                city.value = System.Array.IndexOf(Common.tottori, cityText) + 1;
                break;
            case 32:
                city.value = System.Array.IndexOf(Common.shimane, cityText) + 1;
                break;
            case 33:
                city.value = System.Array.IndexOf(Common.okayama, cityText) + 1;
                break;
            case 34:
                city.value = System.Array.IndexOf(Common.hiroshima, cityText) + 1;
                break;
            case 35:
                city.value = System.Array.IndexOf(Common.yamaguchi, cityText) + 1;
                break;
            case 36:
                city.value = System.Array.IndexOf(Common.tokushima, cityText) + 1;
                break;
            case 37:
                city.value = System.Array.IndexOf(Common.kagawa, cityText) + 1;
                break;
            case 38:
                city.value = System.Array.IndexOf(Common.ehime, cityText) + 1;
                break;
            case 39:
                city.value = System.Array.IndexOf(Common.kouchi, cityText) + 1;
                break;
            case 40:
                city.value = System.Array.IndexOf(Common.hukuoka, cityText) + 1;
                break;
            case 41:
                city.value = System.Array.IndexOf(Common.saga, cityText) + 1;
                break;
            case 42:
                city.value = System.Array.IndexOf(Common.nagasaki, cityText) + 1;
                break;
            case 43:
                city.value = System.Array.IndexOf(Common.kumamoto, cityText) + 1;
                break;
            case 44:
                city.value = System.Array.IndexOf(Common.oita, cityText) + 1;
                break;
            case 45:
                city.value = System.Array.IndexOf(Common.miyazaki, cityText) + 1;
                break;
            case 46:
                city.value = System.Array.IndexOf(Common.kagoshima, cityText) + 1;
                break;
            case 47:
                city.value = System.Array.IndexOf(Common.okinawa, cityText) + 1;
                break;


        }
    }

    #endregion

    #region 時間設定関数


    //時間
    public void hourSet(Dropdown hour, string hourStr)
    {
        if (hourStr == "00")
        {
            hour.value = 0;
        }
        else
        {
            hour.value = System.Array.IndexOf(Common.hours, hourStr);
        }

    }


    //分
    public void minuteSet(Dropdown minute, string minuteStr)
    {
        switch (minuteStr)
        {
            case "00":
                minute.value = 0;
                break;
            case "30":
                minute.value = 1;
                break;
        }
    }
    #endregion

    #region 期間設定関数


    //年
    public void yearSet(Dropdown year, string yearStr)
    {
        switch (yearStr)
        {
            case "2020":
                year.value = 0;
                break;
            case "2021":
                year.value = 1;
                break;
        }
    }

    //月
    public void monthSet(Dropdown month, string monthStr)
    {
        if (monthStr == "01")
        {
            month.value = 0;
        }
        else
        {
            month.value = System.Array.IndexOf(Common.months, monthStr);
        }
    }

    //日
    public void daySet(Dropdown day, string daySrt)
    {
        if (daySrt == "01")
        {
            day.value = 0;
        }
        else
        {
            day.value = System.Array.IndexOf(Common.days, daySrt);
        }
    }


    #endregion
}

[System.Serializable]
public class facilityValue2
{
    public string number;
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
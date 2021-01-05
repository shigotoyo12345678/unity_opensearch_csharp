using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewInput : MonoBehaviour
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

    void Start()
    {
        var ins = new Common();
        ins.prefectureOptionSet(prefecture, city, Common.prefectures);
        ins.timeSet(one, two, three, four, five, six, seven, eight, start2, end2, start3, end3);

        //新規入力する場合は全ての値を初期化する
        if (sceneNo == 0)
        {
            prefectureValue = null;
            cityValue = null;
            nameValue = null;
            urlValue = null;
            postValue = null;
            addressValue = null;
            telValue = null;
            closeValue = null;
            open1Value = null;
            close1Value = null;
            open2Value = null;
            close2Value = null;
            startValue = null;
            endValue = null;
            remarkValue = null;
            tagValue = null;
        }
        //登録内容確認画面から戻るボタンで遷移した場合は一度入力した値を再設定する
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

    //戻るボタン押下時処理
    public void backScene()
    {
        SceneManager.LoadScene("registerSel");
    }

    //登録内容確認ボタン押下時処理
    public void newVerificationScene()
    {
        //値保持用変数に入力した値を代入する
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

        //特別営業開始時間が特別営業終了時間より後の場合はボタンを非活性にする
        if (startDate <= endDate)
        {
        }
        else
        {
            button.enabled = false;
        }

        //県、市のいずれかが未設定、施設名、電話番号のいずれかが未入力の場合はボタンを非活性にする
        if (prefectureValue == "県を選択してください" || cityValue == "市を選択してください" || nameValue == "" || telValue == "")
        {
            button.enabled = false;

        }
        else
        {
            SceneManager.LoadScene("newVerification");
        }

    }

    //県セレクトボックスを変えた時の処理
    public void chengePrefecture()
    {
        var ins = new Common();
        ins.cityOptionSet(prefecture, city);
    }


}

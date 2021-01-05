using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewVerification : MonoBehaviour
{
    [SerializeField] public Text prefecture;          //県表示用テキスト
    [SerializeField] public Text city;                //市表示用テキスト
    [SerializeField] public Text name;                //施設名表示用テキスト
    [SerializeField] public Text url;                 //URL表示用テキスト
    [SerializeField] public Text post;                //郵便番号表示用テキスト
    [SerializeField] public Text address;             //住所表示用テキスト
    [SerializeField] public Text tel;                 //電話番号表示用テキスト
    [SerializeField] public Text open1;               //特別営業開始時間（平日）表示用テキスト
    [SerializeField] public Text close1;              //特別営業終了時間（平日）表示用テキスト
    [SerializeField] public Text open2;               //特別営業開始時間（土日祝）表示用テキスト
    [SerializeField] public Text close2;              //特別営業終了時間（土日祝）表示用テキスト
    [SerializeField] public Text start;               //特別営業開始日時表示用テキスト
    [SerializeField] public Text end;                 //特別営業終了日時表示用テキスト
    [SerializeField] public Text remark;              //備考表示用テキスト
    [SerializeField] public Text tag;                 //タグ表示用テキスト

    [SerializeField] public Text closeText1;                                                                   //一時閉店テキスト１
    [SerializeField] public Text closeText2;                                                                   //一時閉店テキスト２
    [SerializeField] public Text closeText3;                                                                   //一時閉店テキスト３


    //施設情報入力画面で入力、設定した値を表示する
    void Start()
    {
        prefecture.text = NewInput.prefectureValue;
        city.text = NewInput.cityValue;
        name.text = NewInput.nameValue;
        url.text = NewInput.urlValue;
        post.text = NewInput.postValue;
        address.text = NewInput.addressValue;
        tel.text = NewInput.telValue;
        //施設情報入力画面で一時閉店チェックボックスにチェックをした場合は時間表示テキストを非表示にし、一時閉店テキストを表示する
        if (NewInput.closeValue == "1")
        {
            closeText1.gameObject.SetActive(true);
            closeText2.gameObject.SetActive(false);
            closeText3.gameObject.SetActive(false);
            open1.gameObject.SetActive(false);
            close1.gameObject.SetActive(false);
            open2.gameObject.SetActive(false);
            close2.gameObject.SetActive(false);
        }
        //チェックをしていない場合は設定した時間を設定する
        else if (NewInput.closeValue == "0")
        {
            open1.text = NewInput.open1Value;
            close1.text = NewInput.close1Value;
            open2.text = NewInput.open2Value;
            close2.text = NewInput.close2Value;
        }
        start.text = NewInput.startValue;
        end.text = NewInput.endValue;
        remark.text = NewInput.remarkValue;
        tag.text = NewInput.tagValue;

    }

    //登録ボタン押下時処理
    public void registerScene()
    {
        SceneManager.LoadScene("newRegister");
    }

    //戻るボタン押下時処理
    public void backScene()
    {
        NewInput.sceneNo = 1;
        SceneManager.LoadScene("newInput");

    }
}

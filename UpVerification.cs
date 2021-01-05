using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpVerification : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        prefecture.text = UpInput.prefectureValue;
        city.text = UpInput.cityValue;
        name.text = UpInput.nameValue;
        url.text = UpInput.urlValue;
        post.text = UpInput.postValue;
        address.text = UpInput.addressValue;
        tel.text = UpInput.telValue;
        if (UpInput.closeValue == "1")
        {
            closeText1.gameObject.SetActive(true);
            closeText2.gameObject.SetActive(false);
            closeText3.gameObject.SetActive(false);
            open1.gameObject.SetActive(false);
            close1.gameObject.SetActive(false);
            open2.gameObject.SetActive(false);
            close2.gameObject.SetActive(false);
        }
        else if (UpInput.closeValue == "0")
        {
            open1.text = UpInput.open1Value;
            close1.text = UpInput.close1Value;
            open2.text = UpInput.open2Value;
            close2.text = UpInput.close2Value;
        }
        start.text = UpInput.startValue;
        end.text = UpInput.endValue;
        remark.text = UpInput.remarkValue;
        tag.text = UpInput.tagValue;
    }

    public void updateScene()
    {
        SceneManager.LoadScene("upRegister");
    }

    public void backScene()
    {
        UpInput.sceneNo = 1;
        SceneManager.LoadScene("upInput");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Top : MonoBehaviour
{
    [SerializeField] public Dropdown prefecture;            //県
    [SerializeField] public Dropdown city;                  //市
    [SerializeField] public InputField word1;               //検索ワード１
    [SerializeField] public InputField word2;               //検索ワード２
    [SerializeField] public Toggle now;                     //現在

    [SerializeField] public Button searchBtn;               //検索ボタン

    [SerializeField] public Text checkText;                 //チェックテキスト

    public static string prefectureValue;                   //県値保持用変数
    public static string cityValue;                         //市値保持用変数
    public static string word1Value;                        //検索ワード１保持用変数
    public static string word2Value;                        //検索ワード２保持用変数
    public static string nowValue;                          //営業中のみ検索用変数

    // Start is called before the first frame update
    void Start()
    {
        //県セレクトボックスに値をセットし、検索ボタンを非活性にする
        var ins = new Common();
        ins.prefectureOptionSet(prefecture, city, Common.prefectures);
        searchBtn.enabled = false;
    }

    private void registerScene()
    {
        SceneManager.LoadScene("registerSel");
    }

    //検索ボタン押下時処理
    public void facilityListScene()
    {
        //値保持用変数に値を代入する
        prefectureValue = prefecture.options[prefecture.value].text;

        if (city.value == 0)
        {
            cityValue = "";
        }
        else
        {
            cityValue = city.options[city.value].text;
        }
        word1Value = word1.text;
        if (word2.text != "")
        {
            word2Value = word2.text;
        }
        if (now.isOn == true)
        {
            nowValue = "1";
        }
        else if (now.isOn == false)
        {
            nowValue = "0";
        }

        FacilityList.sceneNum = "1";

        SceneManager.LoadScene("facilityList");
    }

    #region 県ドロップダウンを変更したときの処理
    public void chengePrefecture()
    {
        var ins = new Common();
        ins.cityOptionSet(prefecture, city);
    }

    #endregion

    #region 県選択チェック関数
    public void prefectureCheck()
    {
        prefectureValue = prefecture.options[prefecture.value].text;
        if (prefectureValue == "県を選択してください" || prefectureValue == null)
        {
            searchBtn.enabled = false;
            checkText.gameObject.SetActive(true);
        }
        else
        {
            searchBtn.enabled = true;
            checkText.gameObject.SetActive(false);
        }
    }


    #endregion

}

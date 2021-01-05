using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpSearch : MonoBehaviour
{
    [SerializeField] public Dropdown prefecture;            //県
    [SerializeField] public Dropdown city;                  //市
    [SerializeField] public InputField word1;               //検索ワード１
    [SerializeField] public InputField word2;               //検索ワード２

    [SerializeField] public Button button;                  //現在ボタン

    public static string prefectureValue;                   //県値保持用変数
    public static string cityValue;                         //市値保持用変数
    public static string word1Value;                        //検索ワード１保持用変数
    public static string word2Value;                        //検索ワード２保持用変数

    // Start is called before the first frame update
    void Start()
    {
        var ins = new Common();
        ins.prefectureOptionSet(prefecture, city, Common.prefectures);
        button.enabled = false;
    }

    public void searchFacility()
    {
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

        FacilityList.sceneNum = "2";

        SceneManager.LoadScene("facilityList");
    }

    //県ドロップダウンを変えた時の処理
    public void chengePrefecture()
    {
        var ins = new Common();
        ins.cityOptionSet(prefecture, city);
    }

    #region 県選択チェック関数
    public void prefectureCheck()
    {
        prefectureValue = prefecture.options[prefecture.value].text;
        if (prefectureValue == "県を選択してください" || prefectureValue == null)
        {
            button.enabled = false;
        }
        else
        {
            button.enabled = true;
        }
    }

    #endregion

    public void backScene()
    {
        SceneManager.LoadScene("registerSel");
    }
}

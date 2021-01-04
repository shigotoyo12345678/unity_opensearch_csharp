using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickBtn : MonoBehaviour
{
    public Button facilityBtn;               //施設ボタン

    //施設ボタン押下時処理
    public void facilityBtnClick()
    {

        FacilityList.facilityNum = facilityBtn.name;

        //施設リスト画面のシーンナンバーによって遷移先を変える
        if (FacilityList.sceneNum == "1")
        {
            SceneManager.LoadScene("facilityDetail");
        }
        else if (FacilityList.sceneNum == "2")
        {
            SceneManager.LoadScene("upInput");
        }

    }


}

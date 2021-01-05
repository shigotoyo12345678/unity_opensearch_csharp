using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    public void openMapFunc()
    {
        string url = "http://shigotoyo.starfree.jp/opensearch/map.php/?prefecture=" + Top.prefectureValue + "&city=" + Top.cityValue + "&word=" + Top.word1Value;
        if (Top.word2Value != "")
        {
            url = url + "&word2 = " + Top.word2Value;
        }
        if (Top.nowValue == "0")
        {
            url = url + "&now=" + Top.nowValue;
        }
        else if (Top.nowValue == "1")
        {
            url = url + "&now=" + Top.nowValue;
        }
        Application.OpenURL(url);
    }
}

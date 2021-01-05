using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewRegister : MonoBehaviour
{

    [SerializeField] public Text text;                                                     //登録可否テキスト


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(register());

        StartCoroutine(result());
    }

    private void topScene()
    {
        SceneManager.LoadScene("top");
    }

    //施設情報入力画面で設定した値を送信する
    private IEnumerator register()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("prefecture", NewInput.prefectureValue);
        dic.Add("city", NewInput.cityValue);
        dic.Add("name", NewInput.nameValue);
        dic.Add("url", NewInput.urlValue);
        dic.Add("post", NewInput.postValue);
        dic.Add("address", NewInput.addressValue);
        dic.Add("tel", NewInput.telValue);
        dic.Add("open1", NewInput.open1Value);
        dic.Add("close1", NewInput.close1Value);
        dic.Add("open2", NewInput.open2Value);
        dic.Add("close2", NewInput.close2Value);
        dic.Add("start", NewInput.startValue);
        dic.Add("end", NewInput.endValue);
        dic.Add("close", NewInput.closeValue);
        dic.Add("remark", NewInput.remarkValue);
        dic.Add("tag", NewInput.tagValue);

        //DbProcess.ServerAddress = "http://localhost/unity/sql.php";                     //ローカル用アドレス
        obj.ServerAddress = "http://shigotoyo.starfree.jp/opensearch/sql.php";      //Web用アドレス

        obj.Dic = dic;

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    //登録可否を表示する
    private IEnumerator result()
    {
        if (DbProcess.returnText == "")
        {
            text.text = "登録できませんでした";
        }
        else
        {
            text.text = DbProcess.returnText;
        }

        yield return null;
    }
}

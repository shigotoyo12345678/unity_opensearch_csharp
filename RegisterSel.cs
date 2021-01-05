using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterSel : MonoBehaviour
{
    public void newScene()
    {
        NewInput.sceneNo = 0;
        SceneManager.LoadScene("newInput");

    }

    public void chengeScene()
    {
        SceneManager.LoadScene("upSearch");
    }
}

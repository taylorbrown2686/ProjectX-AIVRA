using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpinToWinGlobal : MonoBehaviour
{
    public void GoToSpinToWin()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("SpinToWin");
    }
}

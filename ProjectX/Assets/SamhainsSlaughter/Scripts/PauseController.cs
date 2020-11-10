using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    void Start() {
      this.gameObject.SetActive(false);
    }

    public void BackToGame() {
        this.gameObject.SetActive(false);
    }

    public void RestartGame() {
      //replace mainmenu
    }

    public void BackToAIVRA() {
      SceneManager.LoadScene("AIVRAHome");
    }

}

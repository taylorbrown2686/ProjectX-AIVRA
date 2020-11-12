using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void Restart() {
      ArcadeRoundController.Instance.SetLevel(ArcadeRoundController.Instance.selectedLevelString);
    }

    public void BackToMainMenu() {
      ArcadeRoundController.Instance.SetLevel("MainMenu");
    }

    public void BackToAIVRA() {
      SceneManager.LoadScene("AIVRAHome");
    }
}

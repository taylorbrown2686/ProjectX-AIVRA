using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamesController : MonoBehaviour
{
    [SerializeField] protected Image[] gameImages; //The ones shown in the wheel
    private int selectedGameIndex = 0;
    [SerializeField] private AIVRASays aivraSays;

    void Update() {

    }

    public void Arrow(bool left) {
      if (left) {
        foreach (Image image in gameImages) {
          image.gameObject.GetComponent<GameImage>().ChangeSprite(true);
        }
      } else {
        foreach (Image image in gameImages) {
          image.gameObject.GetComponent<GameImage>().ChangeSprite(false);
        }
      }
    }

    public void Play() {
      switch (gameImages[2].sprite.name) {
        case "ghostsInTheGraveyard":
          SceneManager.LoadScene("ShootEmUp");
        break;

        case "arFishin":
          aivraSays.StartCoroutine(aivraSays.Say("AR Fishin' is currently in development, check back later!"));
        break;

        case "arTetris":
          aivraSays.StartCoroutine(aivraSays.Say("AR Tetris is currently in development, check back later!"));
        break;

        case "buckHunter":
          SceneManager.LoadScene("BuckHunter");
        break;

        case "barDice":
          SceneManager.LoadScene("BarDice");
        break;
      }
    }
}

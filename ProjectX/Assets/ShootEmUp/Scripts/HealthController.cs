using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private int health;
    private int maxHealth = 4;
    public int Health {get => health; set => health = value;}
    public int MaxHealth {get => maxHealth;}

    private GameObject endOfGameImage;
    [SerializeField] private Sprite[] vignettes;
    [SerializeField] private Image vignetteOverlay;

    private bool gameHasStarted = false;

    void Start() {
      health = 4;
      //yield return new WaitForSeconds(0.05f); //Delay for canvas to turn on
      endOfGameImage = GameObject.Find("EndOfGameImage");
      endOfGameImage.SetActive(false);
      vignetteOverlay = GameObject.Find("VignetteOverlay").GetComponent<Image>();
      vignetteOverlay.enabled = false;
      gameHasStarted = true;
    }

    public void DecreaseHealth() {
      health -= 1;
    }

    void Update() {
      if (gameHasStarted) {
        switch (health) {
          case 4:
            vignetteOverlay.enabled = false;
          break;
          case 3:
            vignetteOverlay.enabled = true;
            vignetteOverlay.sprite = vignettes[0];
          break;
          case 2:
            vignetteOverlay.sprite = vignettes[1];
          break;
          case 1:
            vignetteOverlay.sprite = vignettes[2];
          break;
          case 0:
            endOfGameImage.SetActive(true);
            this.gameObject.GetComponent<Shoot>().enabled = false;
          break;
        }
      }
    }

    public void ResetHealth() {
      health = maxHealth;
    }

    public void PowerUp() {
      health += 1; //You can go above maxHealth if you get the powerup
    }

}

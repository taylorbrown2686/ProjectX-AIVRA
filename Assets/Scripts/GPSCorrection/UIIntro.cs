using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script is an animation to play while we wait for the portals to load (from Azure AI)
public class UIIntro : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image background, logo;
    public Text loadingText, tooltipText;
    public Sprite aivra, earthMedia;
    public SendSatImageToServer satImage;

    private string[] tooltips;

    void Awake() {
      tooltips = new string[7];
      tooltips[0] = "AIVRA isn't just a game, it's a social platform and marketing service.";
      tooltips[1] = "AIVRA was started in Eau Claire, Wisconsin.";
      tooltips[2] = "You can get real-world deals for stores in-game! Try opening some chests near businesses.";
      tooltips[3] = "You can trade items in game to other players for real money, in-game currency, or other items.";
      tooltips[4] = "Pokemon GO has nothing on AIVRA!";
    }

    IEnumerator Start() {
      logo.sprite = aivra;
      StartCoroutine(Fade(true, logo));
      yield return new WaitForSeconds(2f);
      StartCoroutine(Fade(false, logo));
      yield return new WaitForSeconds(1f);

      logo.sprite = earthMedia;
      StartCoroutine(Fade(true, logo));
      yield return new WaitForSeconds(2f);
      StartCoroutine(Fade(false, logo));
      yield return new WaitForSeconds(1f);

      logo.enabled = false;
      loadingText.text = "Welcome to AIVRA!" + "\n" + "We are finding a portal to our world, please wait...";
      StartCoroutine(Fade(true, null, loadingText));
      yield return new WaitForSeconds(3f);

      do {
        tooltipText.text = tooltips[Random.Range(0, 4)];
        StartCoroutine(Fade(true, null, tooltipText));
        yield return new WaitForSeconds(3f);
        StartCoroutine(Fade(false, null, tooltipText));
        yield return new WaitForSeconds(2f);
      } while (!satImage.PortalsSpawned);

      loadingScreen.SetActive(false);
    }

    IEnumerator Fade(bool fadeIn, Image image = null, Text text = null) {
      if (image != null) {
        if (fadeIn) {
          while (image.color.a < 1) {
              image.color += new Color(0, 0, 0, 0.02f);
              yield return new WaitForSeconds(0.01f);
          }
        } else {
          while (image.color.a > 0) {
              image.color -= new Color(0, 0, 0, 0.02f);
              yield return new WaitForSeconds(0.01f);
          }
        }
      } else if (text != null) {
        if (fadeIn) {
          while (text.color.a < 1) {
              text.color += new Color(0, 0, 0, 0.02f);
              yield return new WaitForSeconds(0.01f);
          }
        } else {
          while (text.color.a > 0) {
              text.color -= new Color(0, 0, 0, 0.02f);
              yield return new WaitForSeconds(0.01f);
          }
        }
      } else {
        Debug.Log("Only images and text can be faded!");
      }
    }
}

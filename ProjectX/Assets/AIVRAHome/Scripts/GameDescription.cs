using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDescription : MonoBehaviour
{
    [SerializeField] private GameImage selectedImage;
    private int index;

    [SerializeField] private Text title, maxPlayers, rating, description;
    [SerializeField] private Sprite[] gameScreenshots;
    [SerializeField] private Image screenshot;

    void Update() {
      index = selectedImage.Index;
      switch (index) {
        case 0:
          title.text = "AR Tetris";
          maxPlayers.text = "Max Players: 4";
          rating.text = "Rating: E";
          description.text = "The arcade classic brought to AR! Play with your friends to be the last one eliminated.";
          screenshot.sprite = gameScreenshots[index];
        break;
        case 1:
          title.text = "AR Fishin'";
          maxPlayers.text = "Max Players: 8";
          rating.text = "Rating: E";
          description.text = "Hit the lake whenever you want, no boat required! Cast and reel in trophy fish to sell for better gear.";
          screenshot.sprite = gameScreenshots[index];
        break;
        case 2:
          title.text = "Ghosts in the Graveyard";
          maxPlayers.text = "Max Players: 8";
          rating.text = "Rating: E";
          description.text = "Angry ghosts are attacking from the grave! Take them out with halloween candy to put their souls at rest.";
          screenshot.sprite = gameScreenshots[index];
        break;
        case 3:
          title.text = "HuntAR";
          maxPlayers.text = "Max Players: 1";
          rating.text = "Rating: T";
          description.text = "It's open season! Hunt deer in a realistic forest on your desk or in your front yard. Don't shoot the does!";
          screenshot.sprite = gameScreenshots[index];
        break;
        case 4:
          title.text = "AR Bar Dice";
          maxPlayers.text = "Max Players: Unlimited";
          rating.text = "Rating: T";
          description.text = "Is it happy hour? What better time to play bar dice! Play with an unlimited amount of people to roll the highest matching dice. Last player remaining loses.";
          screenshot.sprite = gameScreenshots[index];
        break;
      }
    }

}

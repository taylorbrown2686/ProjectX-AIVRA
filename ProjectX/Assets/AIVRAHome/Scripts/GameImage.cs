using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Attached to game images (Children of SelectableGamesContainer)
public class GameImage : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private Sprite[] gameSprites;
    [SerializeField] private string[] gameTitles;
    [SerializeField] private Color[] gameColors;
    [SerializeField] private Text gameText;
    private Image image;
    private int totalGames = 5;
    private Vector2 originalPosition;

    public string[] GameTitles {get => gameTitles;}
    public int Index {get => index;}

    void Start() {
      image = this.GetComponent<Image>();
      originalPosition = this.GetComponent<RectTransform>().anchoredPosition;
    }

    public void ChangeSprite(bool left) {
      //StartCoroutine(AnimateImage(left));
      if (left) {
        index -= 1;
      } else {
        index += 1;
      }
      if (index > totalGames - 1) {
        index = 0;
      }
      if (index < 0) {
        index = totalGames - 1;
      }
      image.sprite = gameSprites[index];
      gameText.text = gameTitles[index];
      this.gameObject.transform.parent.transform.gameObject.GetComponent<Image>().color = gameColors[index];
    }

    private IEnumerator AnimateImage(bool left) {
      if (left) {
        for (int i = 0; i < 50; i++) {
          this.GetComponent<RectTransform>().anchoredPosition -= new Vector2(5, 0);
          yield return new WaitForEndOfFrame();
        }
      } else {
        for (int i = 0; i < 50; i++) {
          this.GetComponent<RectTransform>().anchoredPosition += new Vector2(5, 0);
          yield return new WaitForEndOfFrame();
        }
      }
      image.sprite = gameSprites[index];
      this.GetComponent<RectTransform>().anchoredPosition = originalPosition;
    }
}

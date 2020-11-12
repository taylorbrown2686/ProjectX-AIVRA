using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamhainHealthController : MonoBehaviour
{
    private int health;
    public Image smiley;
    [SerializeField] private Sprite[] smilies;
    private static SamhainHealthController _instance;

    public static SamhainHealthController Instance {get => _instance;}

    void Start() {
      smilies = GlobalSamhainAssets.Instance.smilies;

      if (_instance != null && _instance != this) {
        Destroy(_instance);
      } else {
        _instance = this;
      }

      health = 5;
      smiley.sprite = smilies[health - 1];
    }

    public void DamagePlayer() {
      health -= 1;
      if (health <= 0) {
        EndGame();
        return;
      }
      smiley.sprite = smilies[health - 1];
    }

    private void EndGame() {
      //destroy round controller
      //turn on death screen
      //death screen then handles all interactions
      Debug.Log("you died");
    }

}

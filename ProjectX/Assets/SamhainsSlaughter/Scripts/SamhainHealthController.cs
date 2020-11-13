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
    private bool dead = false;

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

    void Update() {
      if (Input.GetKeyDown(KeyCode.I)) {
        DamagePlayer();
      }
    }

    public void DamagePlayer() {
      if (!dead) {
        health -= 1;
        if (health <= 0) {
          dead = true;
          EndGame();
          return;
        }
        smiley.sprite = smilies[health - 1];
      }
    }

    private void EndGame() {
      var deathUI = Instantiate(GlobalSamhainAssets.Instance.deathUI, Vector3.zero, Quaternion.identity);
      deathUI.transform.SetParent(GameObject.Find("GameUI").transform, false);
      Destroy(ArcadeRoundController.Instance);
    }

}

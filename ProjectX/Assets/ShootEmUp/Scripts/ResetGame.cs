using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private GameObject gameZonePrefab;
    private GameObject currentGameZone;

    void Start() {
      currentGameZone = GameObject.Find("GameZone(Clone)");
    }

    public void ReplayGame() {
      Vector3 pos = currentGameZone.transform.position;
      Quaternion rot = currentGameZone.transform.rotation;
      Vector3 scale = currentGameZone.transform.localScale;
      Destroy(currentGameZone);
      GameObject newGame = Instantiate(gameZonePrefab, pos, rot, GameObject.Find("AR Session Origin").transform);
      newGame.transform.localScale = scale;
    }
}

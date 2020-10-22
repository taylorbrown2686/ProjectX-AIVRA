using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAdvertisingGPSObjects : MonoBehaviour
{
    [SerializeField] private List<Advertisement> ads = new List<Advertisement>();

    void Awake() {
      ads.Add(new Advertisement(new Vector2(44.814783f, -91.501039f), new GameObject()));
      //ads.Add(new Advertisement(new Vector2(44, -), gameobject));   TEMPLATE TEMPLATE TEMPLATE TEMPLATE
    }

    void Start() {
      foreach (Advertisement ad in ads) {
        Debug.Log(ad);
        //Place a GPS Stage object with Advertisement's fields
      }
    }
}
[System.Serializable]
struct Advertisement {
    public Advertisement(Vector2 c, GameObject obj) {
      coords = c;
      objToSpawn = obj;
    }

    public Vector2 coords;
    public GameObject objToSpawn;
}

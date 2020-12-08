using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMiscellaneousGPSObjects : MonoBehaviour
{
  [SerializeField] private List<MiscObject> miscObjs = new List<MiscObject>();

  void Awake() {
    //miscObjs.Add(new MiscObject(new Vector2(44.814783f, -91.501039f), ));
    //miscObjs.Add(new MiscObject(new Vector2(44, -), gameobject));   TEMPLATE TEMPLATE TEMPLATE TEMPLATE
  }

  void Start() {
    foreach (MiscObject obj in miscObjs) {
      Debug.Log(obj);
      //Place a GPS Stage object with Advertisement's fields
    }
  }
}
[System.Serializable]
struct MiscObject {
  public MiscObject(Vector2 c, GameObject obj) {
    coords = c;
    objToSpawn = obj;
  }

  public Vector2 coords;
  public GameObject objToSpawn;
}

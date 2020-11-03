using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardedAd : MonoBehaviour
{
    [SerializeField] private GameObject adPlane, spawnedAdPlane, adPlaneAnchorPoint;
    [SerializeField] private Texture2D adToDisplay;

    void Start() {
      adPlaneAnchorPoint = GameObject.Find("BillboardAdAnchor");
    }

    public void PopulateScreenWithAd() {
      spawnedAdPlane = Instantiate(adPlane, Vector3.zero, Quaternion.identity, adPlaneAnchorPoint.transform);
      spawnedAdPlane.transform.localPosition = adPlane.transform.localPosition;
      spawnedAdPlane.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = adToDisplay;
    }

    void Update() {
      if (spawnedAdPlane != null) {
        spawnedAdPlane.transform.LookAt(Camera.main.transform); //TODO: Change to face camera at all times
      }
    }

    public void DestroyAd() {
      Destroy(spawnedAdPlane);
    }
}

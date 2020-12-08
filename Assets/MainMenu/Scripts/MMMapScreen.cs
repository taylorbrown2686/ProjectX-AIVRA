using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMMapScreen : MonoBehaviour
{
    [SerializeField] private GameObject scaleMarkerSlider, firstStepText, secondStepText;
    private OnlineMapsMarker createdMarker;
    [SerializeField] private OnlineMaps map;
    private GetCoordsFromBusinessName getCoords = new GetCoordsFromBusinessName();

    private void Start() {
        OnlineMapsControlBase.instance.OnMapClick += OnMapClick;
        getCoords.SendRequest(MMUIController.Instance.businessAddress);
    }

    void OnEnable() {
      scaleMarkerSlider.SetActive(false);
      firstStepText.SetActive(true);
      secondStepText.SetActive(false);
    }

    void Update() {
      if (getCoords.lat != 0) {
        OnlineMaps.instance.SetPosition(getCoords.lng, getCoords.lat);
      }
      if (Input.GetKeyDown(KeyCode.Escape)) {
        MMUIController.Instance.ChangeScreen(4); //Signup
      }
      if (createdMarker != null) {
        if (!scaleMarkerSlider.activeSelf) {
          createdMarker.scale = 1;
        } else {
          createdMarker.scale = scaleMarkerSlider.GetComponent<Slider>().value;
        }
      }
    }

    public void Continue() {
      if (!scaleMarkerSlider.activeSelf) {
        if (createdMarker == null) {
          //error text
        } else {
          scaleMarkerSlider.SetActive(true);
          firstStepText.SetActive(false);
          secondStepText.SetActive(true);
        }
      } else {
        MMUIController.Instance.AddValueToStoredFields("latitude", createdMarker.latitude.ToString());
        MMUIController.Instance.AddValueToStoredFields("longitude", createdMarker.longitude.ToString());
        MMUIController.Instance.AddValueToStoredFields("radius", createdMarker.scale.ToString());
        MMUIController.Instance.ChangeScreen(6);
      }
    }

    private void OnMapClick() {
        double lng, lat;
        OnlineMapsControlBase.instance.GetCoords(out lng, out lat);
        if (createdMarker == null) {
          string label = "Marker " + (OnlineMapsMarkerManager.CountItems + 1);
          createdMarker = OnlineMapsMarkerManager.CreateItem(lng, lat, label);
        } else {
          createdMarker.SetPosition(lng, lat);
          //createdMarker.latitude = lat;
          //createdMarker.longitude = lng;
        }
    }
}

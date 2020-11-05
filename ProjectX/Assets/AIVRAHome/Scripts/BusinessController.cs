using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusinessController : MonoBehaviour
{
    [SerializeField] private InputField businessName, businessAddress;
    [SerializeField] private Text businessNameText, businessAddressText;
    [SerializeField] private GameObject dealsOverlay, gamesOverlay;
    private bool isEditingInfo = false;

    void OnEnable() {
      businessName.gameObject.SetActive(false);
      businessAddress.gameObject.SetActive(false);
      dealsOverlay.SetActive(false);
      gamesOverlay.SetActive(false);
    }

    public void EditInfo() {
      if (!isEditingInfo) {
        isEditingInfo = true;
        businessName.gameObject.SetActive(true);
        businessAddress.gameObject.SetActive(true);
        businessName.text = businessNameText.text;
        businessAddress.text = businessAddressText.text;
      } else {
        isEditingInfo = false;
        businessName.gameObject.SetActive(false);
        businessAddress.gameObject.SetActive(false);
      }
    }

    public void OnInfoChanged(string infoChanged) {
      if (infoChanged == "businessName") {
        businessNameText.text = businessName.text;
      } else if (infoChanged == "businessAddress") {
        businessAddressText.text = businessAddress.text;
      }
      isEditingInfo = false;
      businessName.gameObject.SetActive(false);
      businessAddress.gameObject.SetActive(false);
    }

    public void AdjustMarker() {
      dealsOverlay.SetActive(false);
      gamesOverlay.SetActive(false);
    }

    public void SetUpDeals() {
      gamesOverlay.SetActive(false);
      dealsOverlay.SetActive(true);
    }

    public void SetUpGames() {
      dealsOverlay.SetActive(false);
      gamesOverlay.SetActive(true);
    }
}

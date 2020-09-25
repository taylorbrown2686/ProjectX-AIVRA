using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartRace : MonoBehaviour
{
    public Image detailImage;
    public Sprite[] sprites;
    private GameObject[] cars;

    void Awake() {
      cars = GameObject.FindGameObjectsWithTag("Car");
      foreach (GameObject car in cars) {
        car.GetComponent<VehicleController>().enabled = false;
        car.GetComponent<RaceController>().enabled = false;
        car.GetComponent<ConditionController>().enabled = false;
      }
    }

    IEnumerator Start() {
      detailImage.sprite = sprites[0];
      while (detailImage.color.a < 1) {
          detailImage.color += new Color(0, 0, 0, 0.05f);
          yield return new WaitForSeconds(0.01f);
      }
      yield return new WaitForSeconds(2f);
      while (detailImage.color.a > 0) {
          detailImage.color -= new Color(0, 0, 0, 0.05f);
          yield return new WaitForSeconds(0.01f);
      }
      detailImage.sprite = sprites[1];
      yield return new WaitForSeconds(2f);
      while (detailImage.color.a < 1) {
          detailImage.color += new Color(0, 0, 0, 0.05f);
          yield return new WaitForSeconds(0.01f);
      }
      yield return new WaitForSeconds(2f);
      detailImage.sprite = sprites[2];
      yield return new WaitForSeconds(2f);
      detailImage.sprite = sprites[3];
      yield return new WaitForSeconds(0.5f);
      while (detailImage.color.a > 0) {
          detailImage.color -= new Color(0, 0, 0, 0.05f);
          yield return new WaitForSeconds(0.01f);
      }
      foreach (GameObject car in cars) {
        car.GetComponent<VehicleController>().enabled = true;
        car.GetComponent<RaceController>().enabled = true;
        car.GetComponent<ConditionController>().enabled = true;
      }
    }
}

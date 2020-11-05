using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeDice : MonoBehaviour
{
    [SerializeField] private GameObject die, diceCup, diceSpawnPoint;
    private bool isShaking = false;
    private bool readyToCollect = false;
    private List<GameObject> activeDice = new List<GameObject>();
    private Vector3 originalPosition, originalRotation;
    [SerializeField] private Text resultText;

    public List<GameObject> ActiveDice {get => activeDice;}

    void Start() {
      originalPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
      originalRotation = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
    }

    public void OnclickShake() {
      if (!isShaking) {
        isShaking = true;
        StartCoroutine(Shake());
      } else {
        if (readyToCollect) {
          StartCoroutine(Pickup());
        }
      }
    }

    private IEnumerator Shake() {
      for (float i = -0.5f; i < 0.5f; i+=0.2f) {
        GameObject newDie = Instantiate(die, diceSpawnPoint.transform.position + new Vector3(i, 0, i), RandomRotation());
        activeDice.Add(newDie);
        newDie.name = "Dice " + i;
        yield return new WaitForSeconds(0.25f);
      }
      yield return new WaitForSeconds(0.5f);
      for (int i = 0; i < 25; i++) {
        diceCup.transform.Translate(Vector3.down * 0.25f);
        diceCup.transform.Rotate(new Vector3(0, 0, 6));
        yield return new WaitForEndOfFrame();
      }
      foreach (GameObject die in activeDice) {
        while (die.GetComponent<Die>().rigidbody.velocity != Vector3.zero && die.GetComponent<Die>().rigidbody.angularVelocity != Vector3.zero) {
          yield return new WaitForSeconds(0.1f);
        }
        die.GetComponent<Die>().ReadDie();
      }
      RollResult result = new RollResult();
      resultText.text = result.GetResult(activeDice);
      readyToCollect = true;
    }

    private IEnumerator Pickup() {
      resultText.text = "";
      foreach (GameObject die in activeDice) {
        for (int i = 0; i < 25; i++) {
          die.transform.position = Vector3.Lerp(die.transform.position, diceSpawnPoint.transform.position, 0.1f);
          yield return new WaitForEndOfFrame();
        }
        Destroy(die);
      }
      for (int i = 0; i < 25; i++) {
        this.transform.position = Vector3.Lerp(this.transform.position, originalPosition, 0.5f);
        yield return new WaitForEndOfFrame();
      }
      this.transform.rotation = Quaternion.Euler(originalRotation);
      activeDice.Clear();
      isShaking = false;
      readyToCollect = false;
    }

    private Quaternion RandomRotation() {
      return Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }
}

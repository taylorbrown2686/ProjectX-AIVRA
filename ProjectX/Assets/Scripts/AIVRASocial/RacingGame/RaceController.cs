using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script controls the race. It stores checkpoints passed, laps complete with times, and how long the race is.
public class RaceController : MonoBehaviour
{
    [SerializeField]
    private int totalCheckpoints;
    [SerializeField]
    private int totalLaps;
    private int completeCheckpoints;
    private int completeLaps;
    private float timer;

    public Text totalLapsText, subDetail;

    void Update() {
      totalLapsText.text = "Lap " + completeLaps + "/" + totalLaps;
    }

    private void OnTriggerEnter(Collider col) {
      if (col.gameObject.tag == "FinishLine") {
        //show timer on screen for lap time
        if (completeCheckpoints == totalCheckpoints) {
          StartCoroutine(FadeLapText());
          completeLaps += 1;
          completeCheckpoints = 0;
          timer = 0;
        } else {
          //show message that checkpoint was missed, lap must be restarted
          completeCheckpoints = 0;
        }
        if (completeLaps == totalLaps) {
          //stop race, it is over. show success screen and lap times
        }
      } else if (col.gameObject.tag == "Checkpoint") { //TODO: PEOPLE COULD DRIVE THROUGH CHECKPOINT MULTIPLE TIMES AND CHEAT!!!
        completeCheckpoints += 1;
      }
    }

    private void OnCollisionStay(Collision col) {
      if (col.gameObject.tag == "Ground") {
        //start OoB timer, show on screen and respawn them at start if it takes too long
      }
    }

    private IEnumerator FadeLapText() {
      subDetail.text = "Lap Completed! Time: " + timer;
      while (subDetail.color.a < 1) {
          subDetail.color += new Color(0, 0, 0, 0.05f);
          yield return new WaitForSeconds(0.01f);
      }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGPAUSETIMESCALE : MonoBehaviour
{
    bool isPaused = false;

    public void OnClickPause() {
      if (isPaused) {
        isPaused = false;
        Time.timeScale = 1;
      } else {
        isPaused = true;
        Time.timeScale = 0;
      }
    }
}

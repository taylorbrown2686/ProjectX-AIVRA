using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script handles the rotation of player objects
public class Rotate : AxesBase
{
    void Update() {
      base.Update();
      if (Input.touchCount == 1) {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved) {
          float touchMag = touch.deltaPosition.magnitude;
          switch (ActiveAxis) {

            case "xAxis":
              this.gameObject.transform.parent.transform.rotation *= Quaternion.Euler(touchMag / 1000, 0, 0);
            break;

            case "yAxis":
              this.gameObject.transform.parent.transform.rotation *= Quaternion.Euler(0, touchMag / 1000, 0);
            break;

            case "zAxis":
              this.gameObject.transform.parent.transform.rotation *= Quaternion.Euler(0, 0, touchMag / 1000);
            break;

          }
        }
      }
    }
}

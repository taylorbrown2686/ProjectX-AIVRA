using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : AxesBase
{
    void Update() {
      base.Update();
      if (Input.touchCount == 1) {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved) {
          switch (ActiveAxis) {

            case "xAxis":
              this.gameObject.transform.parent.transform.rotation *= Quaternion.Euler(0.1f, 0, 0);
            break;

            case "yAxis":
              this.gameObject.transform.parent.transform.rotation *= Quaternion.Euler(0, 0.1f, 0);
            break;

            case "zAxis":
              this.gameObject.transform.parent.transform.rotation *= Quaternion.Euler(0, 0, 0.1f);
            break;

          }
        }
      }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script handles the scaling of player objects
public class Scale : AxesBase
{
    void Update() {
      base.Update();
      if (Input.touchCount == 1) {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved) {
          float touchMag = touch.deltaPosition.magnitude;
          switch (ActiveAxis) {

            case "xAxis":
              this.gameObject.transform.parent.transform.localScale += new Vector3(touchMag / 10000, 0, 0);
            break;

            case "yAxis":
              this.gameObject.transform.parent.transform.localScale += new Vector3(0, touchMag / 10000, 0);
            break;

            case "zAxis":
              this.gameObject.transform.parent.transform.localScale += new Vector3(0, 0, touchMag / 10000);
            break;

          }
        }
        this.gameObject.transform.localScale = new Vector3(0.1f, 1f, 0.1f); //Reset the scale so it doesn't change with the block
      }
    }
}

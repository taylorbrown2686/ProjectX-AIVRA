﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script handles the movement of player objects
public class Move : AxesBase
{
    void Update() {
      base.Update(); //Call AxesBase.Update()
      if (Input.touchCount == 1) {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved) {
          float touchMag = touch.deltaPosition.magnitude;
          switch (ActiveAxis) {

            case "xAxis":
              if (dirChange.moveNegative) {
                this.gameObject.transform.parent.transform.position += new Vector3(-(touchMag / 1000), 0, 0);
              } else {
                this.gameObject.transform.parent.transform.position += new Vector3(touchMag / 1000, 0, 0);
              }
            break;

            case "yAxis":
              if (dirChange.moveNegative) {
                this.gameObject.transform.parent.transform.position += new Vector3(0, -(touchMag / 1000), 0);
              } else {
                this.gameObject.transform.parent.transform.position += new Vector3(0, touchMag / 1000, 0);
              }
            break;

            case "zAxis":
              if (dirChange.moveNegative) {
                this.gameObject.transform.parent.transform.position += new Vector3(0, 0, -(touchMag / 1000));
              } else {
                this.gameObject.transform.parent.transform.position += new Vector3(0, 0, touchMag / 1000);
              }
            break;

          }
        }
      }
    }
}

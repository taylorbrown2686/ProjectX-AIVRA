using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMenuEnable : MonoBehaviour
{
    private ARMenuSelect[] options;

    public void Start() {
      options = (ARMenuSelect[])FindObjectsOfType(typeof(ARMenuSelect));
      foreach (ARMenuSelect menu in options) {
        menu.menuIsOn = true;
      }
    }
}

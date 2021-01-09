using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuLookAt : MonoBehaviour
{
    void Update() {
      this.transform.LookAt(Camera.main.transform);
      this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0);
    }
}

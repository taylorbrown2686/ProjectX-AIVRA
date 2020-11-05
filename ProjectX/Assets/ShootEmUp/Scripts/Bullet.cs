using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public string shotBy;

    void OnCollisionEnter() {
      Destroy(this.gameObject);
    }

    void OnTriggerEnter() {
      Destroy(this.gameObject);
    }
}

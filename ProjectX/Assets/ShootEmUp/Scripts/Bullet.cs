using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public string shotBy;
    public int damage;

    void Start() {
      StartCoroutine(DestroyAfterTime());
    }

    void OnCollisionEnter() {
      Destroy(this.gameObject);
    }

    void OnTriggerEnter() {
      Destroy(this.gameObject);
    }

    private IEnumerator DestroyAfterTime() {
      yield return new WaitForSeconds(5f);
      Destroy(this.gameObject);
    }
}

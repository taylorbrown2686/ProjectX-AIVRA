using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObject : MonoBehaviour
{

    protected Rigidbody rb;
    [SerializeField] protected float speedMultiplier;
    [SerializeField] protected ScoreController scoreController;

    void Awake() {
      rb = this.gameObject.GetComponent<Rigidbody>();
      //We do this because ScoreController is on a prefab, and can't be assigned manually

      //TEMP Change how this is assigned for multiplayer (multiple score controllers)
    //  scoreController = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreController>();

    }

    public virtual void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(5,10) * speedMultiplier);
      //transform.Translate(Vector3.up * speedMultiplier);
      StartCoroutine(DestroyAfterTime(UnityEngine.Random.Range(1.5f, 2f)));
    }

    public virtual void OnTriggerEnter(Collider col) {
      if (col.tag == "Bullet") {
            // int scoreToAdd = Convert.ToInt32(this.gameObject.name.Substring(0, 1));
            // scoreController.AddScore(scoreToAdd);
            GameController.instance.AddScore();
            Destroy(this.gameObject);
      }
    }

    protected IEnumerator DestroyAfterTime(float time) {
      yield return new WaitForSeconds(time);
      Destroy(this.gameObject);
    }
}

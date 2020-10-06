using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObject : MonoBehaviour
{

    protected Rigidbody rb;
    [SerializeField] protected float speedMultiplier;
    [SerializeField] protected ScoreController scoreController;
    protected ParticleSystem deathParticles;
    protected RotateOnY rotateOnY;
    protected Material material;
    private bool isDying = false; //true when hit with a bullet
    private bool objectCanBeShot = true; //false when the object begins to vanish
    private float initialY; //used for enemy vanish

    void Awake() {
      rb = this.gameObject.GetComponent<Rigidbody>();
      deathParticles = this.gameObject.GetComponentInChildren<ParticleSystem>();
      rotateOnY = this.GetComponent<RotateOnY>();
      material = this.gameObject.GetComponentInChildren<Renderer>().material;
      initialY = this.transform.position.y;
      //We do this because ScoreController is on a prefab, and can't be assigned manually
      //TEMP Change how this is assigned for multiplayer (multiple score controllers)
      scoreController = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreController>();
    }

    void Update() {
      if (this.gameObject.transform.position.y > initialY + 0.25f && objectCanBeShot) {
        StartCoroutine(Vanish());
      }
    }

    public virtual void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(5,10) * speedMultiplier);
    }

    public virtual void OnTriggerEnter(Collider col) {
      if (col.tag == "Bullet" && objectCanBeShot) {
        objectCanBeShot = false;
        isDying = true;
        int scoreToAdd = Convert.ToInt32(this.gameObject.name.Substring(0, 1));
        scoreController.AddScore(scoreToAdd);
        StartCoroutine(EnemyDeath());
      }
    }

    protected IEnumerator Vanish() {
      objectCanBeShot = false;
      while (material.color.a > 0) {
        material.color -= new Color(0, 0, 0, .01f);
        yield return new WaitForSeconds(0.01f);
      }
      Destroy(this.gameObject);
    }

    protected IEnumerator EnemyDeath() {
      deathParticles.Play();
      while (this.transform.localScale.x > 0) {
        this.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        rotateOnY.Speed += 1f;
        yield return new WaitForSeconds(0.01f);
      }
      Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float health, currentHealth, speed, timeAlive, points;
    private bool timerCanTick = true;
    protected bool isDying = false;
    protected Rigidbody rb;
    [SerializeField] private GameObject healthBarPlane;
    private Animator animator;
    private bool rotateToPathfind = false;

    public virtual void Start() {
      timeAlive = 0;
      points = 1000;
      rb = this.GetComponent<Rigidbody>();
      animator = this.gameObject.GetComponent<Animator>();
      currentHealth = health;
      StartCoroutine(DiminishPoints());
    }

    void Update() {
      if (Input.GetKeyDown(KeyCode.Y)) {
        Damage(3);
      }
      if (timerCanTick) {
        StartCoroutine(TimeAlive());
      }
      healthBarPlane.transform.localScale = new Vector3(currentHealth / health, 1, 1);
      if (currentHealth <= 0) {
        Death();
      }
    }

    private IEnumerator TimeAlive() {
      timerCanTick = false;
      timeAlive += 1;
      yield return new WaitForSeconds(1f);
      timerCanTick = true;
    }

    private IEnumerator DiminishPoints() {
      while (!isDying) {
        points -= 5;
        yield return new WaitForSeconds(0.1f);
      }
    }

    protected virtual void Move() {}
    protected virtual void Attack() {}
    protected void Damage(int damage) {
      animator.SetTrigger("enemyHit");
      currentHealth -= damage;
    }
    protected void Death() {
      animator.SetTrigger("enemyDeath");
    }

    void OnTriggerEnter(Collider col) {
      if (col.tag == "Bullet") {
        Damage(col.gameObject.GetComponent<Bullet>().damage);
        Destroy(col.gameObject);
      }
      if (col.tag == "Obstacle") {
        rotateToPathfind = true;
        StartCoroutine(RotateToPathfind());
      }
    }

    void OnTriggerExit(Collider col) {
      if (col.tag == "Obstacle") {
        rotateToPathfind = false;
      }
    }

    private IEnumerator RotateToPathfind() {
      while (rotateToPathfind) {
        this.transform.Rotate(0, 1, 0);
        yield return new WaitForSeconds(0.01f);
      }
    }
}

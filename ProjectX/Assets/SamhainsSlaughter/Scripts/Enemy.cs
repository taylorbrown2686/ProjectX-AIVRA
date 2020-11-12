using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float health, currentHealth, speed, timeAlive, difficultyCurve;
    protected int points;
    private bool timerCanTick = true;
    protected bool isDying = false;
    protected bool isAttacking = false;
    protected Rigidbody rb;
    [SerializeField] private GameObject healthBarPlane;
    protected Animator animator;
    protected bool rotateToPathfind = false;
    protected GameObject player;

    public virtual void Start() {
      timeAlive = 0;
      points = 1000;
      difficultyCurve = 1;
      rb = this.GetComponent<Rigidbody>();
      animator = this.gameObject.GetComponent<Animator>();
      player = GameObject.FindGameObjectWithTag("Player"); //CHANGE IN MULTIPLAYER
      currentHealth = health;
      StartCoroutine(DiminishPoints());
    }

    public virtual void Update() {
      if (timerCanTick) {
        StartCoroutine(TimeAlive());
      }
      if (ArcadeRoundController.Instance != null) {
        difficultyCurve = ArcadeRoundController.Instance.difficultyCurve;
      }
      healthBarPlane.transform.localScale = new Vector3(currentHealth / health, 1, 1);
      if (healthBarPlane.transform.localScale.x < 0) {
        healthBarPlane.transform.localScale = new Vector3(0, 1, 1);
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
      currentHealth -= damage;
      if (currentHealth <= 0) {
        Death();
      } else {
        animator.SetTrigger("enemyHit");
      }
    }

    protected void Death() {
      isDying = true;
      StartCoroutine(DisableInteractionAndDestroy());
      SamhainScoreController.Instance.AddScore(points);
      animator.SetTrigger("enemyDied");
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
      if (col.tag == "AttackZone") {
        isAttacking = true;
      }
    }

    IEnumerator OnTriggerExit(Collider col) {
      if (col.tag == "Obstacle") {
        yield return new WaitForSeconds(2f);
        rotateToPathfind = false;
      }
    }

    private IEnumerator RotateToPathfind() {
      while (rotateToPathfind) {
        this.transform.Rotate(0, 1, 0);
        yield return new WaitForSeconds(0.05f);
      }
    }

    private IEnumerator DisableInteractionAndDestroy() {
      rb.isKinematic = true;
      rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
      this.GetComponent<CapsuleCollider>().enabled = false;
      yield return new WaitForSeconds(2f);
      Destroy(this.gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootableObject : MonoBehaviour
{

    protected Rigidbody rb;
    [SerializeField] protected float speedMultiplier;
    private ScoreController scoreController;
    private HealthController healthController;
    protected ParticleSystem deathParticles;
    protected RotateOnY rotateOnY;
    protected Material material;
    protected bool isDying = false; //true when hit with a bullet
    protected bool objectCanBeShot = true; //false when the object begins to vanish
    protected bool objectIsAttacking = false;
    protected float initialY; //used for enemy vanish
    [SerializeField] protected Image[] powerupImages;
    private float scaleFactor; //This changes the force applied depending on the scale of the GameZone
    private Animator anim;
    [SerializeField] private Image topJaw, bottomJaw;

    void Awake() {
      rb = this.gameObject.GetComponent<Rigidbody>();
      deathParticles = this.gameObject.GetComponentInChildren<ParticleSystem>();
      rotateOnY = this.GetComponent<RotateOnY>();
      material = this.gameObject.GetComponentInChildren<Renderer>().material;
      initialY = this.transform.position.y;
      //We do this because ScoreController is on a prefab, and can't be assigned manually
      //TEMP Change how this is assigned for multiplayer (multiple score controllers)
      scoreController = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreController>();
      healthController = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
      scaleFactor = GameObject.Find("GameZone(Clone)").transform.localScale.x * 20;
      anim = this.GetComponent<Animator>();
      topJaw = GameObject.Find("TopJaw").GetComponent<Image>();
      bottomJaw = GameObject.Find("BottomJaw").GetComponent<Image>();
    }

    public virtual void Update() {
      if (this.gameObject.transform.position.y > initialY + (scaleFactor / 8) && !objectIsAttacking) {
        StartCoroutine(Attack());
      }
    }

    public virtual void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(5,10) * speedMultiplier * scaleFactor);
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

    private IEnumerator Attack() {
      objectIsAttacking = true;
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
      rotateOnY.enabled = false;
      this.transform.LookAt(Camera.main.transform);
      rb.AddForce(transform.forward * 20);
      while (Vector3.Distance(this.transform.position, Camera.main.transform.position) > 0.1f && !isDying) {
        this.transform.LookAt(Camera.main.transform.GetChild(0).transform);
        yield return new WaitForSeconds(0.1f);
      }
      if (!isDying) {
        objectCanBeShot = false;
        anim.SetTrigger("CloseToPlayer");
        healthController.DecreaseHealth();
        yield return new WaitForSeconds(0.833f);
        StartCoroutine(Bite());
      }
    }

    protected IEnumerator Bite() {
      StartCoroutine(Vanish());
      while (topJaw.gameObject.GetComponent<RectTransform>().sizeDelta.y < Screen.height / 2) {
        topJaw.gameObject.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 200f);
        bottomJaw.gameObject.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 200f);
        yield return new WaitForSeconds(0.001f);
      }
      while (topJaw.color.a > 0) {
        topJaw.color -= new Color(0, 0, 0, 0.01f);
        bottomJaw.color -= new Color(0, 0, 0, 0.01f);
        yield return new WaitForSeconds(0.001f);
      }
      topJaw.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
      bottomJaw.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
      topJaw.color = new Color(0, 0, 0, 1f);
      bottomJaw.color = new Color(0, 0, 0, 1f);
    }

    protected IEnumerator Vanish() {
      isDying = true;
      while (material.color.a > 0) {
        material.color -= new Color(0, 0, 0, .01f);
        yield return new WaitForSeconds(0.01f);
      }
      yield return new WaitForSeconds(5f);
      Destroy(this.gameObject);
    }

    public void VanishOnRoundEnd() {
      StartCoroutine(Vanish());
    }

    protected IEnumerator EnemyDeath() {
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
      rotateOnY.enabled = true;
      deathParticles.Play();
      while (this.transform.localScale.x > 0) {
        this.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        rotateOnY.Speed += 1f;
        yield return new WaitForSeconds(0.01f);
      }
      Destroy(this.gameObject);
    }
}

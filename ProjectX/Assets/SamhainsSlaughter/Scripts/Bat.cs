using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public override void Start() {
      health = 20;
      speed = 4;
      base.Start();
    }

    public override void Update() {
      base.Update();
      if (!isDying) {
        if (!isAttacking) {
          Move();
        } else {
          Attack();
        }
      }
    }

    protected override void Move() {
      if (!rotateToPathfind) {
        this.transform.LookAt(player.transform);
      }
      rb.AddForce(transform.forward * speed * difficultyCurve * (ScaleFactor.Instance.scaleFactor * 5));
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
    }

    protected override void Attack() {
      animator.SetTrigger("jump");
      rb.useGravity = false;
      this.transform.LookAt(player.transform);
      rb.AddForce(transform.forward * speed * 2 * (ScaleFactor.Instance.scaleFactor * 20));
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
      if (Vector3.Distance(this.transform.position, player.transform.position) < 0.1f) {
        animator.SetTrigger("attackPlayer");
        SamhainHealthController.Instance.DamagePlayer();
        Die();
      }
    }

    private void Die() {
      isDying = true;
      Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public override void Start() {
      health = 100;
      speed = 8;
      base.Start();
      Move();
    }

    protected override void Move() {
      rb.AddForce(transform.forward * speed * (ScaleFactor.Instance.scaleFactor * 10));
    }
}

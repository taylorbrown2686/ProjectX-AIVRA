using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public override void Start() {
      health = 200;
      speed = 3;
      base.Start();
      Move();
    }

    protected override void Move() {
      rb.AddForce(transform.forward * speed * (ScaleFactor.Instance.scaleFactor * 10));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public override void Start() {
      health = 150;
      speed = 4;
      base.Start();
      Move();
    }

    protected override void Move() {
      rb.AddForce(transform.forward * speed * (ScaleFactor.Instance.scaleFactor * 10));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public override void Start() {
      health = 100;
      speed = 7;
      base.Start();
      Move();
    }

    protected override void Move() {
      rb.AddForce(transform.forward * speed * (ScaleFactor.Instance.scaleFactor * 10));
    }
}

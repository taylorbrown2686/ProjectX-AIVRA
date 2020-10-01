using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject smokeParticle;
    public Transform smokeSpawnOne, smokeSpawnTwo;

    void Update() {
      Instantiate(smokeParticle, smokeSpawnOne.position, Quaternion.identity, this.transform);
      Instantiate(smokeParticle, smokeSpawnTwo.position, Quaternion.identity, this.transform);
      this.transform.Rotate(0, 0, 1);
    }

    void OnCollisionEnter(Collision col) {
      Destroy(this.gameObject);
    }
}

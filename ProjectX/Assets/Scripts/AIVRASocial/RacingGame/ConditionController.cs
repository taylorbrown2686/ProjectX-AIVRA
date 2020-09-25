using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionController : MonoBehaviour
{
    [SerializeField]
    private float health;
    private VehicleController vehicle;

    void Start() {
      vehicle = this.GetComponent<VehicleController>();
    }

    void Update() {
      if ((50 < health) && (health < 75)) {

      } else if ((25 < health) && (health < 50)) {

      } else if ((0 < health) && (health < 25)) {

      }

      if (health < 0) {
        Explode();
      }
    }

    private void OnCollisionEnter(Collision col) {
      if (col.gameObject.tag == "Obstacle") {
        if (vehicle.Acceleration > (vehicle.Acceleration * 0.6)) { //If we are going 60% of top speed or higher...
          health -= vehicle.Acceleration * 5; //damage is 5 times your acceleration
        }
      }
    }

    private void Explode() {
      Debug.Log("CAR BOKE");
    }
}

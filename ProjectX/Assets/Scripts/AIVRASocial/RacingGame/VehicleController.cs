using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UltimateJoystick;

public class VehicleController : VehicleBase
{
    private Rigidbody rb;
    public GameObject leftTire, rightTire; //In front, all cars are FWD

    private void Start() {
      rb = this.GetComponent<Rigidbody>();
    }

    private void Update() {
      RotateTires();
      //We can use left or right tires for rotation, both are the same
      transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
        UltimateJoystick.GetHorizontalAxis("SteeringJoystick") * steeringForce, transform.rotation.eulerAngles.z);
      transform.position = rb.transform.position;

      switch (rb.velocity.magnitude * 10) { //Speed (in m/s)
        case float n when n > 0: //When greater than topSpeed * someRatio...
          acceleration = accelerationsAtGears[0]; //Change the acceleration accordingly
        break;

        case float n when n > topSpeed * gearRatios[0]:
          acceleration = accelerationsAtGears[1];
        break;

        case float n when n > topSpeed * gearRatios[1]:
          acceleration = accelerationsAtGears[2];
        break;

        case float n when n > topSpeed * gearRatios[2]:
          acceleration = accelerationsAtGears[3];
        break;

        case float n when n > topSpeed * gearRatios[3]:
          acceleration = accelerationsAtGears[4];
        break;

        case float n when n > topSpeed * gearRatios[4]:
          acceleration = accelerationsAtGears[5];
        break;
      }
    }

    private void FixedUpdate() { //Use FixedUpdate for physics calculations
      //rb.transform.Translate(Vector3.right * UltimateJoystick.GetVerticalAxis("MovingJoystick") * 3 * Time.deltaTime, Space.Self);
      rb.AddForce(leftTire.transform.right * UltimateJoystick.GetVerticalAxis("MovingJoystick") * acceleration * Time.deltaTime);
      rb.AddForce(rightTire.transform.right * UltimateJoystick.GetVerticalAxis("MovingJoystick") * acceleration * Time.deltaTime);
    }

    private void RotateTires() {
      leftTire.transform.rotation = Quaternion.Euler(leftTire.transform.rotation.x,
        UltimateJoystick.GetHorizontalAxis("SteeringJoystick") * 50, leftTire.transform.rotation.z);

      rightTire.transform.rotation = Quaternion.Euler(rightTire.transform.rotation.x,
        UltimateJoystick.GetHorizontalAxis("SteeringJoystick") * 50, rightTire.transform.rotation.z);
    }
}

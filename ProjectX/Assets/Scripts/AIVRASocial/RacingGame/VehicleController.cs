using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UltimateJoystick;

public class VehicleController : MonoBehaviour
{
    private Rigidbody rb;
    protected float acceleration, maxSpeed, turnForce;
    private float speedInput, turnInput;
    private bool isAccelerating, isBraking, isSteeringLeft, isSteeringRight;

    public float Acceleration {get => acceleration;}

    private void Start() {
      rb = this.GetComponent<Rigidbody>();
      //TEMP
      maxSpeed = 2f;
    }

    private void Update() {
      if (isAccelerating) {
        Accelerate();
      } else if (isBraking) {
        Brake();
      } else {
        Coast();
      }

      if (isSteeringLeft) {
        Steer(true);
      } else if (isSteeringRight) {
        Steer(false);
      } else {
        turnForce = 0; //If we aren't steering, set the turnForce to 0 to stop turning
      }

      transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
        new Vector3(0, UltimateJoystick.GetHorizontalAxis("SteeringJoystick") * 100 * Time.deltaTime, 0));
      transform.position = rb.transform.position;
    }

    private void FixedUpdate() { //Use FixedUpdate for physics calculations
      rb.transform.Translate(Vector3.right * UltimateJoystick.GetVerticalAxis("MovingJoystick") * 2 * Time.deltaTime, Space.Self);
    }

    private void Accelerate() { //Button event handler (PointerDown)
      acceleration += UltimateJoystick.GetVerticalAxis("MovingJoystick");
      if (acceleration > maxSpeed) {
        acceleration = maxSpeed;
      }
    }

    private void Brake() { //Button event handler (PointerDown)
      acceleration -= 0.66f;
      if (acceleration < 0) {
        acceleration = 0;
      }
    }

    private void Coast() {
      acceleration -= 0.33f;
      if (acceleration < 0) {
        acceleration = 0;
      }
    }

    private void Steer(bool isLeft) {
      if (isLeft) {
        turnForce -= 3.33f;
        //acceleration -= 0.5f;
      } else {
        turnForce += 3.33f;
        //acceleration -= 0.5f;
      }
    }

    //Public button event handlers
    public void OnTouchStart(string buttonHit) {
      switch (buttonHit) {
        case "Accelerate":
          isAccelerating = true;
        break;
        case "Brake":
          isBraking = true;
        break;
        case "SteerLeft":
          isSteeringRight = false; //We can't turn both directions at once, so turn off the opposite steering
          isSteeringLeft = true;
        break;
        case "SteerRight":
          isSteeringLeft = false;
          isSteeringRight = true;
        break;
      }
    }

    public void OnTouchEnd(string buttonHit) {
      switch (buttonHit) {
        case "Accelerate":
          isAccelerating = false;
        break;
        case "Brake":
          isBraking = false;
        break;
        case "SteerLeft":
          isSteeringLeft = false;
        break;
        case "SteerRight":
          isSteeringRight = false;
        break;
      }
    }
}

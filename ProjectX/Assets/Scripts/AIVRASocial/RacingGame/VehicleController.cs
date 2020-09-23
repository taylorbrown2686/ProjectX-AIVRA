using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    private Rigidbody rb;
    protected float acceleration, maxSpeed, turnForce;
    private float speedInput, turnInput;
    private bool isAccelerating, isBraking, isSteeringLeft, isSteeringRight;

    private void Start() {
      rb = this.GetComponent<Rigidbody>();
      //TEMP
      maxSpeed = 20f;
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

      transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnForce * Time.deltaTime, 0));
      transform.position = rb.transform.position;
    }

    private void FixedUpdate() { //Use FixedUpdate for physics calculations
      //rb.AddForce(transform.right * acceleration * Time.deltaTime); //My test model has axes flipped, normally use transform.forward
      rb.transform.Translate(Vector3.right * acceleration * Time.deltaTime, Space.Self);
    }

    private void Accelerate() { //Button event handler (PointerDown)
      acceleration += 3f;
      if (acceleration > maxSpeed) {
        acceleration = maxSpeed;
      }
    }

    private void Brake() { //Button event handler (PointerDown)
      acceleration -= 5f;
      if (acceleration < 0) {
        acceleration = 0;
      }
    }

    private void Coast() {
      acceleration -= 0.25f;
      if (acceleration < 0) {
        acceleration = 0;
      }
    }

    private void Steer(bool isLeft) {
      if (isLeft) {
        turnForce -= 1f;
        acceleration -= 1f;
      } else {
        turnForce += 1f;
        acceleration -= 1f;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWVehicleController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteeringAngle;
    private float currentBrakeForce;
    private bool isAccelerating, isBraking, isSteeringLeft, isSteeringRight;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider frontDriver, frontPassenger, rearDriver, rearPassenger;
    [SerializeField] private Transform frontDriverTransform, frontPassengerTransform, rearDriverTransform, rearPassengerTransform;

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
        currentSteeringAngle = 0; //If we aren't steering, set the turnForce to 0 to stop turning
      }

      if (verticalInput > 1) {
        verticalInput = 1;
      }
      if (verticalInput < 0) {
        verticalInput = 0;
      }
      if (horizontalInput > 1) {
        horizontalInput = 1;
      }
      if (horizontalInput < -1) {
        horizontalInput = -1;
      }
    }

    private void FixedUpdate() {
      HandleMotor();
      HandleSteering();
      UpdateWheels();
    }

    //Physics
    private void HandleMotor() {
      frontDriver.motorTorque = verticalInput * motorForce;
      frontPassenger.motorTorque = verticalInput * motorForce;
      currentBrakeForce = isBraking ? brakeForce : 0f;
      if (isBraking) {
        ApplyBrake();
      }
    }

    private void ApplyBrake() {
      frontDriver.brakeTorque = currentBrakeForce;
      frontPassenger.brakeTorque = currentBrakeForce;
      rearDriver.brakeTorque = currentBrakeForce;
      rearPassenger.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering() {
      currentSteeringAngle = maxSteeringAngle * horizontalInput;
      frontDriver.steerAngle = currentSteeringAngle;
      frontPassenger.steerAngle = currentSteeringAngle;
    }

    private void UpdateWheels() {
      UpdateSingleWheel(frontDriver, frontDriverTransform);
      UpdateSingleWheel(frontPassenger, frontPassengerTransform);
      //UpdateSingleWheel(rearDriver, rearDriverTransform);
      //UpdateSingleWheel(rearPassenger, rearPassengerTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
      Vector3 pos;
      Quaternion rot;
      wheelCollider.GetWorldPose(out pos, out rot);
      wheelTransform.position = pos;
      //DO NOT EDIT THE BELOW LINE!!! WHEEL COLLIDERS DO NOT MAKE MUCH SENSE AND CHANGING THIS LINE WILL BREAK THE CAR
      wheelTransform.rotation = Quaternion.Euler(rot.eulerAngles.z, rot.eulerAngles.y - 90, rot.eulerAngles.x);
    }

    //Event Listening
    private void Accelerate() { //Button event handler (PointerDown)
      verticalInput += 0.2f;
    }

    private void Brake() { //Button event handler (PointerDown)
      verticalInput -= 0.33f;
    }

    private void Coast() {
      verticalInput -= 0.1f;
    }

    private void Steer(bool isLeft) {
      if (isLeft) {
        horizontalInput -= 0.01f;
        verticalInput -= 0.05f;
      } else {
        horizontalInput += 0.01f;
        verticalInput -= 0.05f;
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

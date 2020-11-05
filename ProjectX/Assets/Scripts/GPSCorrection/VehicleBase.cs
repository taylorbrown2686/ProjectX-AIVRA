using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBase : MonoBehaviour
{
    [SerializeField]
    protected float steeringForce, topSpeed, accelerationLoss;
    [SerializeField]
    protected float[] gearRatios, accelerationsAtGears; //We have 6 gear ratios, and 6 different speeds at each ratio
    protected float acceleration;

    public float Acceleration {get => acceleration;} //Get for ConditionController

}

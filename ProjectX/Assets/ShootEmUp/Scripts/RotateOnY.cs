using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnY : MonoBehaviour
{
    [SerializeField] private float speed; //Assigned in editor, changes dynamically during death animation
    public float Speed {get => speed; set => speed = value;}

    void Update() {
      this.transform.Rotate(0, speed, 0);
    }
}

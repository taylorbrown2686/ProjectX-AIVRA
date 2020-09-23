using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

  private Vector2 touchStart, touchEnd;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

  void Update() {
    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
      touchStart = Input.GetTouch(0).position;
    }

    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {
      touchEnd = Input.GetTouch(0).position;
      float cameraFacing = Camera.main.transform.eulerAngles.y;
      Vector2 swipeVector = touchEnd - touchStart;
      Vector3 inputVector = new Vector3(swipeVector.x, 0, swipeVector.y);
      Vector3 movement = Quaternion.Euler(0, cameraFacing, 0) * Vector3.Normalize(inputVector);
      rb.velocity = movement;
    }
  }
}

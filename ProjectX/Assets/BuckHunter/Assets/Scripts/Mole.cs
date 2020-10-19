using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    public Transform destination;
    // Start is called before the first frame update
    private Vector3 pointA;
    private Vector3 pointB;
    private float speed = 3.0f;

    IEnumerator Start()
    {
        pointB = destination.position;
        pointA = transform.position;
        while (true)
        {
         //   Debug.Log("start " + transform.position);
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, speed));
            float randomx = Random.Range(start.x, end.x);
            float randomz = Random.Range(start.z, end.z);
        //    Debug.Log("end " + transform.position);
            transform.position = new Vector3(randomx, 5f, randomz);
          //  Debug.Log(transform.position);

            speed = Random.Range(1.0f, 5.0f);
            pointB = destination.position;
            pointA = transform.position;
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, speed));
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}

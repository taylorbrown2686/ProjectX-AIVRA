using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    // Start is called before the first frame update
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 pointC;
    private float speed = 3.0f;
    private float scale = 1.0f;
    private bool alive = true;
    private Animator animator;
    Dog dog;

    IEnumerator Start()
    {
        
        pointA = transform.position;
        while (alive)
        {
          //  Debug.Log("start " + transform.position);
            float randomX = Random.Range(start.x, end.x);
            float randomY = Random.Range(0, 1.5f);
          //  Debug.Log("Y " + randomY);
            float randomZ = Random.Range(start.z, end.z);
            pointB = new Vector3(randomX, randomY, randomZ);
            
           // transform.LookAt(new Vector3(look.x, transform.position.y, look.z));
            speed = Random.Range(1.0f / scale, 3.0f / scale);
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, speed));
            pointA = pointB;
            
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        Vector3 look = transform.parent.TransformPoint(endPos);
        transform.LookAt(new Vector3(look.x,transform.position.y, look.z));
        while (i < 1.0f)
        {
            if (!alive)
                break;
            i += Time.deltaTime * rate;
            thisTransform.localPosition = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }


    public void Die()
    {
        if (alive == false)
            return;
        animator = GetComponent<Animator>();
        alive = false;
        animator.SetTrigger("death");
        pointC = transform.position;
    }

    public void SetScale(float scale)
    {
        this.scale = scale;
    }

    private void Update()
    {
        
        if (!alive) {
            GetComponent<Collider>().enabled = false;
            // StopCoroutine(MoveObject);
            if (transform.localPosition.y > 0.0031f) {
                //Debug.Log(transform.localPosition.y);
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, 0.003f, transform.localPosition.z), 0.0005f*scale*40);
            }
            else { 
                animator.SetTrigger("ground");
                //GetComponent<Collider>().enabled = false;
                if (GameObject.FindGameObjectWithTag("dog") != null) { 
                    dog = GameObject.FindGameObjectWithTag("dog").GetComponent<Dog>();
                    dog.AddDuck(this.gameObject);
                }
                else
                {
                    GetComponent<Collider>().enabled = false;
                }
                enabled = false;
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (dog != null)
            if (collision.gameObject.tag == "dog" && dog.grab == false) {
                dog.GrabDuck();
            Destroy(this.gameObject);
        }
    }
}

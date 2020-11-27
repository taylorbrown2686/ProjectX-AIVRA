using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Squirrel : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 destination;
    Vector3 destination1;
    Vector3 destination2;
    Vector3 destination3;
    Vector3 destination4;
    float scale = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (agent == null)
            return;
        if (Vector3.Distance(transform.position, destination) < 2f * scale)
        {
            ChangeDestination();
        }
    }


    void ChangeDestination()
    {
        float min = -4.5f;
        float max = 4.5f;

        destination1 = transform.parent.TransformPoint(new Vector3(Random.Range(-5f,-4.5f), 0.8f, Random.Range(min, max)));
        destination2 = transform.parent.TransformPoint(new Vector3(Random.Range(min, max), 0.8f, Random.Range(4.5f, 5f)));
        destination3 = transform.parent.TransformPoint(new Vector3(Random.Range(4.5f,5f), 0.8f, Random.Range(min, max)));
        destination4 = transform.parent.TransformPoint(new Vector3(Random.Range(min, max), 0.8f, Random.Range(-5f, -4.5f)));

        float distance1, distance2, distance3, distance4,minDistance;

        distance1 = Vector3.Distance(Camera.main.transform.position, destination1);
        distance2 = Vector3.Distance(Camera.main.transform.position, destination2);
        distance3 = Vector3.Distance(Camera.main.transform.position, destination3);
        distance4 = Vector3.Distance(Camera.main.transform.position, destination4);

        if (distance1 < distance2) { 
            destination = destination1;
            minDistance = distance1;
        }
        else { 
            destination = destination2;
            minDistance = distance2;
        }

        if (distance3 < minDistance)
        {
            destination = destination3;
            minDistance = distance3;
        }

        if (distance4 < minDistance)
        {
            destination = destination4;
            minDistance = distance4;
        }


        //convert it to world position
        //destination = transform.parent.TransformPoint(destination);
        agent.SetDestination( destination);
    }

    public void SetAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeDestination();
    }

    public void SetScale(float scale)
    {
        this.scale = scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "bullet") { 
            GameManager.Instance.AddScore(30);
            Destroy(this.gameObject);
        }
    }

}

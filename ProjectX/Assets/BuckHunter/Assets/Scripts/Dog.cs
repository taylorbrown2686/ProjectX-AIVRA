using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{

    Animator animator;
    List<GameObject> duckList;
    NavMeshAgent agent;
    float distance;
    int duckIndex = 0;
    public GameObject deadDuck;
    public GameObject[] exit;
    public bool grab = false;
    int exitIndex = 0;
    bool faded = true;
    bool alive = true;
    float scale = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        duckList = new List<GameObject>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        deadDuck.SetActive(false);
        agent.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(alive==true)
            if(grab == false)
            {
                if (duckList.Count > duckIndex) {
                   
                    agent.destination = duckList[duckIndex].transform.position;
                    if (faded == true)
                    {
                        animator.SetTrigger("fadeIn");
                        faded = false;
                    }
                }
            }
            else
            {
                agent.destination = exit[exitIndex].transform.position;
                if (Vector3.Distance(transform.position, exit[exitIndex].transform.position) < 0.5f)
                {
                    //animator.SetTrigger("fade");
                    deadDuck.SetActive(false);
                    grab = false;
                    if (!(duckList.Count > duckIndex) && faded == false) { 
                        animator.SetTrigger("fade");
                        faded = true;
                    }
                }

            }


        // Debug.Log("index " + duckIndex);
        // Debug.Log("distance " + distance);

    }

    public void AddDuck(GameObject duck)
    {
        duckList.Add(duck);
        agent.speed = 4;
    }

    public void SetScale(float scale)
    {
        this.scale = scale;
        agent.speed *= scale;
    }

    public void GrabDuck()
    {
        if (grab == false) { 
            duckIndex++;
            deadDuck.SetActive(true);
            grab = true;
            exitIndex = UnityEngine.Random.Range(0, exit.Length);
        }
    }

    public void Die()
    {
     //   if (faded == false) {
            alive = false;
            animator.SetTrigger("death");
            agent.speed = 0;
            agent.isStopped = true;
       //     agent.enabled = false;
            
            GetComponent<Collider>().enabled = false;
           
            Destroy(this.gameObject, 2f);
     //   }
    }

    public void SetExit(GameObject[] exit)
    {
        this.exit = exit;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sheep : MonoBehaviour
{
    private GameObject[] food;
    GameObject[] exit;
    int index;
    GameObject lastFood = null;
    int numberofFoods;
    Animator animator;
    Collider mCollider;
    private int maxHp = 2;
    private int hp;
    bool eating = false;
    int ExitIndex;
    bool running = false;
    NavMeshAgent agent;
    Rigidbody rb;
    private float scale = 1;
    int eatAttempt = 0;
    public string kind;
    int runCounter = 3;
    int tempExit;

    Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        numberofFoods = food.Length;
        index = Random.Range(0, numberofFoods);
        animator = GetComponent<Animator>();
        mCollider = GetComponent<Collider>();
        destination = food[index].transform.position;
        
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10f);
       // Destroy(gameObject, 45);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") && kind =="deer")
        {
            Die();
        }
        // Debug.Log(transform.position);
        if (agent == null)
            return;

        if(agent.enabled == true) { 
            agent.destination = destination;
            if (running == true)
            {
                if (Vector3.Distance(transform.position, exit[ExitIndex].transform.position) < 2f * scale)
                {
                    if(runCounter == 0) { 
                        transform.LookAt(new Vector3(exit[ExitIndex].transform.position.x,transform.position.y, exit[ExitIndex].transform.position.z));
                        animator.SetTrigger("fade");
                        agent.enabled = false;
                        //rb.isKinematic = true;



                        Destroy(gameObject, 1);
                        if (kind == "deer")
                        {
                            GameManager.Instance.DeerEscaped();

                        }
                        GameManager.Instance.DeerControl(false);
                        //enabled = false;
                    }
                    else
                    {
                        
                        Debug.Log(runCounter);
                        runCounter--;

                        tempExit = ExitIndex;
                        while (ExitIndex == tempExit)
                            ExitIndex = Random.Range(0, exit.Length);

                        destination = exit[ExitIndex].transform.position;
                    }
                }
            }
        }
        else { 
            rb.AddForce(transform.forward * 20f * scale);
        }

    }

    public void SetFood(GameObject[] food)
    {
        this.food = food;
    }

    public void SetExit(GameObject[] exit)
    {
        this.exit = exit;
    }

    public void SetScale(float scale)
    {
        this.scale = scale;
        GetComponent<NavMeshAgent>().speed *= scale;
    }

    public void SetAgent()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(3);
        if (running == false) { 
            destination = food[index].transform.position;
            animator.SetTrigger("walk");
            GetComponent<NavMeshAgent>().speed = 1* scale;
        }

        eating = false;

    }

    public void ChangeVAlue()
    {
        eatAttempt++;
        if(eatAttempt == 3)
        {
            RunAway();
            return;

        }
        int currentIndex = index;
        
        while (currentIndex == index)
            index = Random.Range(0, numberofFoods);
        StartCoroutine(Wait());

    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("eating");
        if (running ==true)
            return;
        if (collider.tag == "food" && eating == false)
        {
            eating = true;
            if(animator != null)
                animator.SetTrigger("eat");
            agent.speed = 0;
            ChangeVAlue();
        }
        

    }

    public void GetFast()
    {
        animator.SetTrigger("run");
        GetComponent<NavMeshAgent>().speed = 3*scale;
    }

    public int Hit()
    {
        if (hp <= 0)
            return 0;
        Debug.Log("Hit!!!!");
       // animator.SetTrigger("run");
        hp--;
        
        if (hp <= 0) { 
            Die();
            return 0;
        }
        RunAway();
        return hp;
    }

    public void RunAway() {
        if (running == true)
            return;
        running = true;
        ExitIndex = Random.Range(0,exit.Length);
        destination = exit[ExitIndex].transform.position;
        GetFast();
    }

    public void Die()
    {
        if(kind == "deer") { 
            GameManager.Instance.AddScore(1);
            GameManager.Instance.KilledDeer();
            GameManager.Instance.DeerControl(false);
        }
        else if(kind == "doe"){ 
            GameManager.Instance.AddScore(-1);
            GameManager.Instance.DoeKilled();
            GameManager.Instance.DeerControl(true);
        }
        else
        {
            GameManager.Instance.AddScore(2);
            GameManager.Instance.DeerControl(false);
        }
            
        hp = 0;
        GetComponent<NavMeshAgent>().speed = 0;
        animator.SetTrigger("death");
        Debug.Log("died");
        enabled = false;
    }

    private void OnDestroy()
    {
 
    }

}

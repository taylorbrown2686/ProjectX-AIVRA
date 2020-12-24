using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMonster : MonoBehaviour
{
    int hp = 10;
    Animator animator;
    // Start is called before the first frame update
    float speed = 0.5f;
    bool move = false;
    bool isAttacking = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       /* if (move == false)
            return;
        
        float step = speed * Time.deltaTime; // calculate distance to move
        Vector3 target = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);

        if (Vector3.Distance(target,transform.position) > 1) { 
            if(isAttacking == true) { 
                animator.SetTrigger("move");
                isAttacking = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        else {
            if (isAttacking == false) { 
                animator.SetTrigger("attack");
                isAttacking = true;
            }
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hp < 1)
            return;
        animator.SetTrigger("hit");
        hp--;
        if(hp < 1) {
            GetComponent<Collider>().enabled = false;
            animator.SetTrigger("die");
            this.enabled = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hp < 1)
            return;
        animator.SetTrigger("hit");
        hp--;
        if (hp < 1)
        {
            GetComponent<Collider>().enabled = false;
            animator.SetTrigger("die");
            this.enabled = false;

        }
    }

    public void StartToMove()
    {
        move = true;
        animator.SetTrigger("move");
    }
}

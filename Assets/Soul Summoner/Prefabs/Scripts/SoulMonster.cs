using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMonster : MonoBehaviourPunCallbacks
{
    int hp;
    int maxHp = 3;
    Animator animator;
    // Start is called before the first frame update
    float speed = 0.5f;
    bool move = false;
    bool isAttacking = false;
    Vector3 target;
    public PhotonView photonview;
    public SoulHealthBar soulHealthBar;
    void Start()
    {
        
        hp = maxHp;
        animator = GetComponent<Animator>();
        photonview = GetComponent<PhotonView>();
     //   animator.SetTrigger("die");
    }

    // Update is called once per frame
    void Update()
    {
        if (move == false)
            return;
        
        float step = speed * Time.deltaTime; // calculate distance to move
                                             //Vector3 target = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        if (photonview.IsMine == true)
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        /*if (Vector3.Distance(target,transform.position) > 1) { 
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

    public void getHit()
    {
        if (photonview.IsMine == false) { 
            if (hp < 1)
                return;
            //hit is here
            photonView.RPC("Hit", RpcTarget.All);
            if (hp < 1)
            {
                print("monster died!");
                photonView.RPC("Death", RpcTarget.All);

            }
        }
    }

    [PunRPC]
    void Hit()
    {
        hp--;
        soulHealthBar.ChangeBar(maxHp, hp);
        if(hp>0)
            animator.SetTrigger("hit");
    }

    [PunRPC]
    void Death()
    {
        GetComponent<Collider>().enabled = false;
        animator.SetTrigger("die");
        this.enabled = false;
    }



    public void StartToMove()
    {
        move = true;
        animator.SetTrigger("move");
        if (photonview.IsMine == true)
            target = new Vector3(SoulGameManager.Instance.sa.monsterposition.position.x, transform.position.y, SoulGameManager.Instance.sa.monsterposition.position.z);
    }
}

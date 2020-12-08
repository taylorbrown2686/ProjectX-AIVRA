using UnityEngine;
using System.Collections;

public class SquirrelCharacter : MonoBehaviour
{
    Animator squirrelAnimator;
    public bool jumpStart = false;
    public float groundCheckDistance = 0.6f;
    public float groundCheckOffset = 0.01f;
    public bool isGrounded = true;
    public float jumpSpeed = 1f;
    Rigidbody squirrelRigid;
    public float forwardSpeed;
    public float turnSpeed;
    public float walkMode = 1f;
    public float jumpStartTime = 0f;
    public float maxWalkSpeed = 1f;

    void Start()
    {
        squirrelAnimator = GetComponent<Animator>();
        squirrelRigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckGroundStatus();
        Move();
        jumpStartTime += Time.deltaTime;
        maxWalkSpeed = Mathf.Lerp(maxWalkSpeed, walkMode, Time.deltaTime);
    }

    public void Attack()
    {
        squirrelAnimator.SetTrigger("Attack");
    }

    public void Hit()
    {
        squirrelAnimator.SetTrigger("Hit");
    }

    public void Eat()
    {
        squirrelAnimator.SetTrigger("Eat");
    }

    public void Gallop()
    {
        walkMode = 1f;
    }



    public void Walk()
    {
        walkMode = .5f;
    }


    public void EatStart()
    {
        squirrelAnimator.SetBool("IsEating",true);
    }
    public void EatEnd()
    {
        squirrelAnimator.SetBool("IsEating", false);
    }



    public void Jump()
    {
        if (isGrounded)
        {
            squirrelAnimator.SetTrigger("Jump");
            jumpStart = true;
            jumpStartTime = 0f;
            isGrounded = false;
            squirrelAnimator.SetBool("IsGrounded", false);
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        isGrounded = Physics.Raycast(transform.position + (transform.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);

        if (jumpStart)
        {
            if (jumpStartTime > .25f)
            {
                jumpStart = false;
                squirrelRigid.AddForce((transform.up + transform.forward * forwardSpeed) * jumpSpeed, ForceMode.Impulse);
                squirrelAnimator.applyRootMotion = false;
                squirrelAnimator.SetBool("IsGrounded", false);
            }
        }

        if (isGrounded && !jumpStart && jumpStartTime > .5f)
        {
            squirrelAnimator.applyRootMotion = true;
            squirrelAnimator.SetBool("IsGrounded", true);
        }
        else
        {
            if (!jumpStart)
            {
                squirrelAnimator.applyRootMotion = false;
                squirrelAnimator.SetBool("IsGrounded", false);
            }
        }
    }

    public void Move()
    {
        squirrelAnimator.SetFloat("Forward", forwardSpeed);
        squirrelAnimator.SetFloat("Turn", turnSpeed);
    }
}

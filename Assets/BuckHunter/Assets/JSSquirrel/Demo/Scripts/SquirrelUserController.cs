using UnityEngine;
using System.Collections;

public class SquirrelUserController : MonoBehaviour
{
    SquirrelCharacter squirrelCharacter;

    void Start()
    {
        squirrelCharacter = GetComponent<SquirrelCharacter>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            squirrelCharacter.Attack();
        }
        if (Input.GetButtonDown("Jump"))
        {
            squirrelCharacter.Jump();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            squirrelCharacter.Hit();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            squirrelCharacter.EatStart();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            squirrelCharacter.EatEnd();
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            squirrelCharacter.Gallop();
        }
   
        if (Input.GetKeyDown(KeyCode.X))
        {
            squirrelCharacter.Walk();
        }





        squirrelCharacter.forwardSpeed = squirrelCharacter.maxWalkSpeed * Input.GetAxis("Vertical");
        squirrelCharacter.turnSpeed = Input.GetAxis("Horizontal");
    }

}

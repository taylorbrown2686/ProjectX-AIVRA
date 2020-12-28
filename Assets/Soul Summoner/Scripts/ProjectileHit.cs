using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    void Start()
    {
        var tm = GetComponentInChildren<RFX1_TransformMotion>(true);
        if (tm != null) tm.CollisionEnter += Tm_CollisionEnter;
    }

    private void Tm_CollisionEnter(object sender, RFX1_TransformMotion.RFX1_CollisionInfo e)
    {
        Debug.Log(e.Hit.transform.name); //will print collided object name to the console.
        if(e.Hit.transform.gameObject.GetComponent<SoulMonster>() != null)
        {
            e.Hit.transform.gameObject.GetComponent<SoulMonster>().getHit();
        }
    }
}

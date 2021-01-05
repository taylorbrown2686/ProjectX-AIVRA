using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    public int damage;
    public int dps = 0;
    public int duration = 0;
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

        if (e.Hit.transform.gameObject.transform.parent.GetComponent<SoulAvatar>() != null)
        {
            print("hitttttttttt");
            e.Hit.transform.gameObject.transform.parent.GetComponent<SoulAvatar>().UseHp(damage);
            if (dps > 0)
            {
                StartCoroutine(e.Hit.transform.gameObject.transform.parent.GetComponent<SoulAvatar>().HpPerSecond(-1 * dps, duration));
            }
        }
    }

    public void setDPS(int dps,int duration)
    {
        this.dps = dps;
        this.duration = duration;
    }
}

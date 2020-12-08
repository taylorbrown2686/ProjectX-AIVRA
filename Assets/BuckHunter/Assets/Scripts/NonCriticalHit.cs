using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCriticalHit : MonoBehaviour
{
    public GameObject parent;
    public string type;
    int hp;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "bullet") {
        //    Debug.Log("non-critical hit");
            Destroy(other.gameObject);
            if(parent.gameObject.GetComponent<Sheep>().kind =="deer")
                if (type == "back")
                {
                    parent.gameObject.GetComponent<Sheep>().back++;
                }
            hp = parent.gameObject.GetComponent<Sheep>().Hit();
            if (hp <= 0)
                gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}

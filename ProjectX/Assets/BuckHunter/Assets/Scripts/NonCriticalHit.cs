using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCriticalHit : MonoBehaviour
{
    public GameObject parent;
    int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void OnCollisionEnter(Collision collision)
    {
     //   
        Debug.Log("sdfsd");
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "bullet") {
            Debug.Log("non-critical hit");
            Destroy(other.gameObject);
            hp = parent.gameObject.GetComponent<Sheep>().Hit();
            if (hp <= 0)
                gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}

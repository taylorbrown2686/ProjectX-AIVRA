using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scare : MonoBehaviour
{
    public GameObject parent;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            if (parent.gameObject.GetComponent<Sheep>().enabled == true)
                parent.gameObject.GetComponent<Sheep>().RunAway();
        }

    }
}

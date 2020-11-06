using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalHit : MonoBehaviour
{
    public GameObject parent;
    public string type;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
        //    Debug.Log("Critical Hit!!!!!");
            Destroy(other.gameObject);
            gameObject.GetComponent<Collider>().enabled = false;
            Sheep deer = parent.gameObject.GetComponent<Sheep>();
            if (type == "head") {
                deer.head++;
            //    deer.Headshot();
            }
            if (type == "neck") {
                deer.neck++;
               // deer.Neckshot();

            }

            if (deer.enabled == true) {
                if(deer.kind == "deer")
                    GameManager.Instance.AddScore(50);
                deer.Die();
            }
        }

    }
}

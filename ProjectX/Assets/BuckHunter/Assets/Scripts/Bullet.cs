using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.gameObject.tag == "duck")
        {
            collision.collider.gameObject.GetComponent<Duck>().Die();
            GameManager.Instance.AddScore(+5);

        }
        if (collision.collider.gameObject.tag == "dog")
        {
            collision.collider.gameObject.GetComponent<Dog>().Die();
            GameManager.Instance.AddScore(-3);

        }
        Destroy(this.gameObject);

    }

}

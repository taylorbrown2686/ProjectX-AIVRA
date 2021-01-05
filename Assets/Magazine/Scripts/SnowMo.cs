using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMo : MonoBehaviour
{
    [SerializeField]
    Transform transform1, transform2;
    Vector3 pos1, pos2, target;
    // Start is called before the first frame update
    void Start()
    {
        pos1 = new Vector3(transform1.position.x, transform1.position.y, transform1.position.z);
        pos2 = new Vector3(transform2.position.x, transform2.position.y, transform2.position.z);
        target = pos2;
    }

    // Update is called once per frame
    void Update()
    {
        float step = 0.1f;

        transform.position = Vector3.MoveTowards(transform.position, target, step * Time.deltaTime);
        if(transform.position == pos2)
        {
            transform.LookAt(pos1);
            target = pos1;
        }

        if (transform.position == pos1)
        {
            transform.LookAt(pos2);
            target = pos2;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerShoot : MonoBehaviour
{
    public GameObject[] point;
    public bool shot = false;
    public DeerShoot[] ds;
    static int shootCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
       // DeActivateAll();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePoint(int number)
    {

        
        point[number].gameObject.SetActive(true);
        Debug.Log(point[number] + " " +  point[number].activeSelf);

        shot = true;

        
    }

    public void DeActivateAll()
    {
        for(int i=0;i<point.Length;i++)
            point[i].gameObject.SetActive(false);
        shot = false;
        shootCounter = 0;
    }


}

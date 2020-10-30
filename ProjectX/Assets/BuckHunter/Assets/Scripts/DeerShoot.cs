using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerShoot : MonoBehaviour
{
    public GameObject[] point;
    public bool shot;
    public DeerShoot[] ds;
    static int shootCounter = 0;
    RectTransform first, second, third;
    // Start is called before the first frame update
    void Start()
    {
        DeActivateAll();

        first = ds[0].GetComponent<RectTransform>();
        second = ds[1].GetComponent<RectTransform>();
        third = ds[2].GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePoint(int number)
    {
        /*if (shot == false) {
            RectTransform rt = GetComponent<RectTransform>();
            if (shootCounter == 0) {

                rt = first;
            }
            if (shootCounter == 1)
                rt = second;
            if (shootCounter == 2)
                rt = third;
            shootCounter++;
        }*/
        
        point[number].gameObject.SetActive(true);
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

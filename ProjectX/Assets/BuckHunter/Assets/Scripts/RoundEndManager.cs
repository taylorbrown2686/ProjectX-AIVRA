using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundEndManager : MonoBehaviour
{

    public GameObject[] buckList;
    public GameObject[] transferBuckList;
    private GameObject[] point;
    private GameObject[] pointTransfer;
    public Text[] text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Transfer()
    {
        for(int i=0,k=0; i < buckList.Length; i++,k++)
        {
            if(k == 3)
                break;
            point = buckList[i].GetComponent<DeerShoot>().point;

            while (transferBuckList[k].GetComponent<DeerShoot>().shot == false) { 

                k++;
                Debug.Log("!!!!!!!!!!!!!!!!!!here " + k);
                if (k == 3)
                    break;
            }
            if (k == 3)
                break;
            Debug.Log("!!!!!!!!!!!!!!!!!!here2 " + k);
            pointTransfer = transferBuckList[k].GetComponent<DeerShoot>().point;


            for (int j = 0; j < point.Length; j++) { 
                point[j].gameObject.SetActive(pointTransfer[j].activeSelf);
               
            }
            buckList[i].SetActive(true);
            text[i].gameObject.SetActive(true);
        }

        
    }

    public void Restart()
    {
        foreach (GameObject buck in buckList)
        {
            buck.SetActive(false);
        }

        foreach(Text tx in text)
            tx.gameObject.SetActive(false);

    }
}

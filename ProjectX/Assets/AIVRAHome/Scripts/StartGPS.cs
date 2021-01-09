using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGPS : MonoBehaviour
{
    [SerializeField] private GameObject gpsBlock;

    private void Start()
    {
        gpsBlock.SetActive(true);
        StartCoroutine(GPSDelay());
    }

    private IEnumerator GPSDelay()
    {
        yield return new WaitForSeconds(6f);
        if (Input.location.status == LocationServiceStatus.Running)
        {
            gpsBlock.SetActive(false);
        }
    }
}

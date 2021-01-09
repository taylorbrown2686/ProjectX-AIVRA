using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForUpdate : MonoBehaviour
{

    private IEnumerator Start()
    {
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getVersion.php");
        yield return www;
        if (Application.version != www.text)
        {
            print("OUT OF DATE!!!");
            //Notifier.Instance.StartCoroutine(Notifier.Instance.SendNotification(
                //"AIVRA", CrossSceneVariables.Instance.email, "UpdateAvailable", "There is an update available for AIVRA! Touch to download."));
        }
    }
}

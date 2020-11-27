using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDatabaseQuery : MonoBehaviour
{
    public IEnumerator RunQuery(Dictionary<string, string> formDataDict, string phpToRun)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> pair in formDataDict)
        {
            form.AddField(pair.Key, pair.Value);
        }
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/" + phpToRun + ".php", form);
        yield return www;
        if (www.text != "0")
        {
            yield return www.text;
            Debug.Log(www.text);
            Destroy(this);
        }
        else
        {
            yield return "Success!";
            Debug.Log("Success!");
            Destroy(this);
        }
    }
}

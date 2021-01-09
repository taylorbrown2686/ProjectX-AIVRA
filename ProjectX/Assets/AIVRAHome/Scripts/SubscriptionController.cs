using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscriptionController : MonoBehaviour
{
    [SerializeField] private Text errorText;
    private string subType;
    private DateTime renewal;
    public void OnSuccess()
    {
        StartCoroutine(UploadSubscriptionToDB());
    }

    public void OnFail()
    {
        errorText.text = "Payment failed. Try again with a different payment method";
    }

    private IEnumerator UploadSubscriptionToDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("subtype", subType);
        form.AddField("renewal", renewal.ToString());
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/uploadSubscriptionInfo.php", form);
        yield return www;
        Destroy(this.gameObject);
    }

    public void ChangeSubTypeAndRenewal(bool monthly)
    {
        if (monthly)
        {
            subType = "Monthly";
            renewal = DateTime.Now.AddDays(90);
        } else
        {
            subType = "Biannual";
            renewal = DateTime.Now.AddDays(182);
        }
    }

    public void CloseSubBox()
    {
        Destroy(this.gameObject);
    }
}

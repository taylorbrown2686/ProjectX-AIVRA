using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanningDealController : MonoBehaviour
{
    [SerializeField] private Text internalName, location, amount, expiry, originalPrice, newPrice;
    public string email;
    [SerializeField] private InputField price;
    private float totalPrice;

    public void Populate(string internalNameM, string locationM, string amountM, string expiryM)
    {
        internalName.text = internalNameM;
        location.text = locationM;
        amount.text = amountM;
        expiry.text = expiryM;
    }

    public void Calculate()
    {
        if (price.text != "")
        {
            StartCoroutine(RemoveDealForCustomer());
            if (amount.text.Contains("%"))
            {
                totalPrice = float.Parse(price.text) - (float.Parse(price.text) * float.Parse(amount.text.Trim(new Char[] {'%'})));
                originalPrice.text = "Original Price: " + price.text;
                newPrice.text = "Price After Deal: " + totalPrice.ToString();
            }
            else
            {
                totalPrice = float.Parse(price.text) - float.Parse(amount.text.Trim(new Char[] {'$'}));
                originalPrice.text = "Original Price: " + price.text;
                newPrice.text = "Price After Deal: " + totalPrice.ToString();
            }
        } 
        else
        {
            Debug.Log("NO PRICE ENTERED");
        }
    }

    private IEnumerator RemoveDealForCustomer()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("dealInternalName", internalName.text);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/removeDealFromSavedCustomerDeals.php", form);
        yield return www;
        Debug.Log(www.text);
    }

    public void Done()
    {
        Destroy(this.gameObject);
    }
}

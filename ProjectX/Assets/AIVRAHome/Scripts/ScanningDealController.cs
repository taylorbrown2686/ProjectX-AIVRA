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
                originalPrice.text = price.text;
                newPrice.text = totalPrice.ToString();
            }
            else
            {
                totalPrice = float.Parse(price.text) - float.Parse(amount.text.Trim(new Char[] {'$'}));
                originalPrice.text = price.text;
                newPrice.text = totalPrice.ToString();
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
        WWW www = new WWW("http://localhost:8080/AIVRA-PHP/saveDealToCustomerAccount.php", form);
        yield return www;
    }

    public void Done()
    {
        Destroy(this.gameObject);
    }
}

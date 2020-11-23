using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealsController : MonoBehaviour
{
    public List<Deal> deals = new List<Deal>();
    public List<Deal> yourDeals = new List<Deal>();

    void Start()
    {
        StartCoroutine(GetDealsInRadius());
        StartCoroutine(GetSavedDeals());
    }

    private IEnumerator GetDealsInRadius()
    {
        WWW www = new WWW("http://localhost:8080/AIVRA-PHP/getAllDealsInRadius.php");
        yield return www;
        string[] splitString = www.text.Split('#');
        for (int i = 0; i < splitString.Length - 1; i += 4)
        {
            Deal deal = new Deal();
            deal.businessName = splitString[i];
            deal.internalName = splitString[i + 1];
            deal.discountAmount = splitString[i + 2];
            deal.expiry = splitString[i + 3];
            deals.Add(deal);
        }
    }

    private IEnumerator GetSavedDeals()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://localhost:8080/AIVRA-PHP/getCustomerDealsFromEmail.php", form);
        yield return www;
        string[] splitString = www.text.Split('#');
        for (int i = 0; i < splitString.Length - 1; i += 5)
        {
            Deal deal = new Deal();
            deal.email = splitString[i];
            deal.internalName = splitString[i + 1];
            deal.businessName = splitString[i + 2];
            deal.discountAmount = splitString[i + 3];
            deal.expiry = splitString[i + 4];
            yourDeals.Add(deal);
        }
    }
}

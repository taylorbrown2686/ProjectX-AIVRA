using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealsController : MonoBehaviour
{
    public List<Deal> deals = new List<Deal>();
    public List<Deal> yourDeals = new List<Deal>();
    private bool dealExists = false;
    private bool dealExpired = false;
    [SerializeField] private GameObject mapCover;
    [SerializeField] private DealsFilteredByCustomerController customerController;
    [SerializeField] private DealsFilteredByBusinessController businessController;

    void OnEnable()
    {
        StartCoroutine(GetSavedDeals());
    }

    public void RecallAfterAction(bool isCustomer) {
        if (isCustomer) {
            StartCoroutine(GetSavedDeals(customerRecall: true));
        }
        else {
            StartCoroutine(GetSavedDeals(businessRecall: true));
        }
    }

    private IEnumerator GetDealsInRadius(bool customerRecall = false, bool businessRecall = false)
    {
        deals.Clear();
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getAllDealsInRadius.php");
        yield return www;
        string[] splitString = www.text.Split('#');
        for (int i = 0; i < splitString.Length - 1; i += 4)
        {
            if (DateTime.Parse(splitString[i + 3]) < DateTime.Today) {
                dealExpired = true;
            }
            if (dealExpired) {
                dealExpired = false;
                continue;
            }
            foreach (Deal dealio in yourDeals) {
                if (dealio.internalName == splitString[i + 1]) {
                    dealExists = true;
                }
            }
            if (dealExists) {
                dealExists = false;
                continue;
            }
            Deal deal = new Deal();
            deal.businessName = splitString[i];
            deal.internalName = splitString[i + 1];
            deal.discountAmount = splitString[i + 2];
            deal.expiry = splitString[i + 3];
            deals.Add(deal);
        }
        if (customerRecall) {
            customerController.GetListOfFilteredDeals();
        } else if (businessRecall) {
            businessController.GetListOfFilteredDeals(businessController.search.text);
        }
    }

    private IEnumerator GetSavedDeals(bool customerRecall = false, bool businessRecall = false)
    {
        yourDeals.Clear();
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getCustomerDealsFromEmail.php", form);
        yield return www;
        string[] splitString = www.text.Split('#');
        for (int i = 0; i < splitString.Length - 1; i += 5)
        {
            if (DateTime.Parse(splitString[i + 4]) < DateTime.Today) {
                dealExpired = true;
            }
            if (dealExpired) {
                dealExpired = false;
                continue;
            }
            Deal deal = new Deal();
            deal.email = splitString[i];
            deal.internalName = splitString[i + 1];
            deal.businessName = splitString[i + 2];
            deal.discountAmount = splitString[i + 3];
            deal.expiry = splitString[i + 4];
            yourDeals.Add(deal);
        }
        if (customerRecall) {
            StartCoroutine(GetDealsInRadius(customerRecall: true));
        }
        else if (businessRecall) {
            StartCoroutine(GetDealsInRadius(businessRecall: true));
        }
        else {
            StartCoroutine(GetDealsInRadius());
        }
    }

    public void GlobalBack() {
        mapCover.SetActive(false);
    }
}

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
    [SerializeField] private GameObject allDealsContent, yourDealsContent;
    [SerializeField] private GameObject emptyDealPrefab;
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

    private void PopulateAllDeals()
    {
        allDealsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * deals.Count);
        for (int i = 0; i < deals.Count; i++)
        {
            if (i % 2 == 0)
            {
                GameObject newObj = Instantiate(emptyDealPrefab, new Vector3(0,0,0), Quaternion.identity);
                newObj.transform.SetParent(allDealsContent.transform, false);
                newObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                newObj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                newObj.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                newObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, i * -150, 0);
                newObj.GetComponent<DealCouponPopulator>().PopulateCoupon(deals[i]);
            }
            else
            {
                GameObject newObj = Instantiate(emptyDealPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newObj.transform.SetParent(allDealsContent.transform, false);
                newObj.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
                newObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                newObj.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
                newObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (i - 1) * -150, 0);
                newObj.GetComponent<DealCouponPopulator>().PopulateCoupon(deals[i]);
            }
        }
    }

    private void PopulateYourDeals()
    {
        yourDealsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * yourDeals.Count);
        for (int i = 0; i < yourDeals.Count; i++)
        {
            if (i % 2 == 0)
            {
                GameObject newObj = Instantiate(emptyDealPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newObj.transform.SetParent(yourDealsContent.transform, false);
                newObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                newObj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                newObj.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                newObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, i * -150, 0);
                newObj.GetComponent<DealCouponPopulator>().PopulateCoupon(yourDeals[i]);
                newObj.GetComponent<DealCouponPopulator>().isSavedDeal = true;
            }
            else
            {
                GameObject newObj = Instantiate(emptyDealPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newObj.transform.SetParent(yourDealsContent.transform, false);
                newObj.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
                newObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                newObj.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
                newObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (i - 1) * -150, 0);
                newObj.GetComponent<DealCouponPopulator>().PopulateCoupon(yourDeals[i]);
                newObj.GetComponent<DealCouponPopulator>().isSavedDeal = true;
            }
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
        PopulateAllDeals();
        PopulateYourDeals();

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
        StartCoroutine(GetDealsInRadius());
        
    }
}

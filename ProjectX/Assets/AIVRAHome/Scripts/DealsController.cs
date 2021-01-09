using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealsController : MonoBehaviour
{
    public List<Deal> deals = new List<Deal>();
    public List<Deal> yourDeals = new List<Deal>();
    private bool dealExists = false;
    private bool dealExpired = false;
    [SerializeField] private GameObject allDealsContent, yourDealsContent;
    [SerializeField] private GameObject emptyDealPrefab;
    public GameObject inBusinessControlBlock;
    [SerializeField] private Text distanceText;
    [SerializeField] private Slider distanceSlider;
    public InputField searchInput;
    [SerializeField] private Dropdown filterDropdown;
    [SerializeField] private DealsFilteredByCustomerController customerController;
    [SerializeField] private DealsFilteredByBusinessController businessController;

    private static DealsController instance = null;

    public static DealsController Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void OnEnable()
    {
        if (UIController.Instance.filterBusinessDeals)
        {
            CrossSceneVariables.Instance.PopulateBusinessesInRadius(50);
            inBusinessControlBlock.SetActive(true);
            inBusinessControlBlock.GetComponentInChildren<Text>().text = "You are viewing " + CrossSceneVariables.Instance.inBusinessName + "'s deals.\nTo see all deals, go home and select 'Deals'";
            OnSearchEndEdit(CrossSceneVariables.Instance.inBusinessName);
        } else
        {
            inBusinessControlBlock.SetActive(false);
            OnSliderChanged();
        }
    }

    public void RecallAfterAction()
    {
        StartCoroutine(GetSavedDeals(UIController.Instance.filterBusinessDeals));
    }

    private void PopulateAllDeals(List<Deal> deals)
    {
        foreach (Transform child in allDealsContent.transform)
        {
            Destroy(child.gameObject);
        }
        allDealsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * deals.Count);
        for (int i = 0; i < deals.Count; i++)
        {
            if (i % 2 == 0)
            {
                GameObject newObj = Instantiate(emptyDealPrefab, new Vector3(0, 0, 0), Quaternion.identity);
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
        foreach (Transform child in yourDealsContent.transform)
        {
            Destroy(child.gameObject);
        }
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

    private IEnumerator GetDealsInRadius(bool dontPopulateDeals)
    {
        deals.Clear();
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getAllDealsInRadius.php");
        yield return www;
        string[] splitString = www.text.Split('#');
        for (int i = 0; i < splitString.Length - 1; i += 6)
        {
            if (DateTime.Parse(splitString[i + 5]) < DateTime.Today)
            {
                dealExpired = true;
            }
            if (dealExpired)
            {
                dealExpired = false;
                continue;
            }
            foreach (Deal dealio in yourDeals)
            {
                if (dealio.internalName == splitString[i + 1])
                {
                    dealExists = true;
                }
            }
            if (dealExists)
            {
                dealExists = false;
                continue;
            }
            bool dealInRadius = false;
            foreach (string businessName in CrossSceneVariables.Instance.businessesInRadius)
            {
                if (businessName == splitString[i])
                {
                    dealInRadius = true;
                }
            }
            Deal deal = new Deal();
            deal.businessName = splitString[i];
            deal.internalName = splitString[i + 1];
            deal.category = splitString[i + 2];
            deal.tags = splitString[i + 3];
            deal.discountAmount = splitString[i + 4];
            deal.expiry = splitString[i + 5];
            if (dealInRadius)
            {
                deals.Add(deal);
            }
        }
        if (!dontPopulateDeals)
        {
            PopulateAllDeals(deals);
        }
        PopulateYourDeals();

    }

    private IEnumerator GetSavedDeals(bool dontPopulateDeals)
    {
        yourDeals.Clear();
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getCustomerDealsFromEmail.php", form);
        yield return www;
        string[] splitString = www.text.Split('#');
        for (int i = 0; i < splitString.Length - 1; i += 6)
        {
            if (DateTime.Parse(splitString[i + 4]) < DateTime.Today)
            {
                dealExpired = true;
            }
            if (dealExpired)
            {
                dealExpired = false;
                continue;
            }
            if (splitString[i + 5] == "1")
            {
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
        if (dontPopulateDeals)
        {
            StartCoroutine(GetDealsInRadius(true));
        } else
        {
            StartCoroutine(GetDealsInRadius(false));
        }

    }

    public void OnSearchEndEdit(string search = null)
    {
        if (search == "")
        {
            search = searchInput.text.ToLower();
        } else
        {
            search = search.ToLower();
        }
        StartCoroutine(GetSavedDeals(true));
        StartCoroutine(DoSearch(search));
    }

    private IEnumerator DoSearch(string search)
    {
        List<Deal> newList = new List<Deal>();
        while (deals.Count == 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        foreach (Deal deal in deals)
        {
            //if search is contained in business name or tags
            if (deal.businessName.ToLower().Contains(search) || deal.tags.ToLower().Contains(search))
            {
                newList.Add(deal);
            }
        }
        PopulateAllDeals(newList);
    }

    public void OnFilterChanged()
    {
        List<Deal> newList = new List<Deal>();
        foreach (Deal deal in deals)
        {
            if (filterDropdown.options[filterDropdown.value].text == "All")
            {
                newList.Add(deal);
            }
            if (filterDropdown.options[filterDropdown.value].text == deal.category)
            {
                newList.Add(deal);
            }
        }
        PopulateAllDeals(newList);
    }

    public void OnSliderChanged()
    {
        if (distanceSlider.value < 1)
        {
            distanceText.text = (distanceSlider.value * 5280f).ToString("F0") + " Feet";
        }
        else if (distanceSlider.value == 50)
        {
            distanceText.text = "No Limit";
        }
        else
        {
            distanceText.text = distanceSlider.value.ToString("F0") + " Miles";
        }
        CrossSceneVariables.Instance.PopulateBusinessesInRadius(distanceSlider.value);
        StartCoroutine(GetSavedDeals(true));
    }
}

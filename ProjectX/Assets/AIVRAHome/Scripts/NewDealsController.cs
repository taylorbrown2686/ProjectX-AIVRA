using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewDealsController : MonoBehaviour
{
    public List<Deal> deals = new List<Deal>();
    public List<Deal> yourDeals = new List<Deal>();
    public List<Deal> yourUsableDeals = new List<Deal>();
    [SerializeField] private GameObject allDealsContent, yourDealsContent;
    [SerializeField] private GameObject emptyDealPrefab;
    public GameObject inBusinessControlBlock;
    [SerializeField] private Text distanceText;
    [SerializeField] private Slider distanceSlider;
    public InputField searchInput;
    [SerializeField] private Dropdown filterDropdown; 
    public GameObject businessDealOverlay, customerDealOverlay, businessSavingCover, customerRemovingCover;
    public DealCouponPopulator businessCoupon, customerCoupon;

    private static NewDealsController instance = null;

    public static NewDealsController Instance { get => instance; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OnEnable()
    {
        CrossSceneVariables.Instance.PopulateBusinessesInRadius(distanceSlider.value);
        businessSavingCover.SetActive(false);
        customerRemovingCover.SetActive(false);
    }

    public IEnumerator GetSavedDeals() //CALL THIS FIRST EVERY RECALL!
    {
        yourDeals.Clear();
        yourUsableDeals.Clear();
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getCustomerDealsFromEmail.php", form);
        yield return www;
        string[] splitString = www.text.Split('#');
        print(splitString.Length);
        for (int i = 0; i < splitString.Length - 1; i += 7)
        {
            bool dealUnusable = false;
            if (DateTime.Parse(splitString[i + 4]) < DateTime.Today)
            {
                dealUnusable = true;
            }
            if (splitString[i + 5] == "1") //if deal has been used already...
            {
                dealUnusable = true;
            }
            Deal deal = new Deal();
            deal.email = splitString[i];
            deal.internalName = splitString[i + 1];
            deal.businessName = splitString[i + 2];
            deal.discountAmount = splitString[i + 3];
            deal.expiry = splitString[i + 4];
            deal.isReward = splitString[i + 6];
            yourDeals.Add(deal);
            if (!dealUnusable)
            {
                yourUsableDeals.Add(deal);
            }
        }
        print(yourDeals.Count);
        StartCoroutine(GetDeals());
    }

    private IEnumerator GetDeals()
    {
        deals.Clear();
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getAllDealsInRadius.php");
        yield return www;
        Debug.Log(www.text);
        string[] splitString = www.text.Split('#');
        for (int i = 0; i < splitString.Length - 1; i += 6)
        {
            bool dealExpired = false;
            bool dealAlreadyAdded = false;
            bool dealInRadius = false;
            bool dealMismatchBusinessFilter = false;
            bool dealMismatchFilter = false;
            bool dealMismatchSearch = false;
            if (DateTime.Parse(splitString[i + 5]) < DateTime.Today)
            {
                dealExpired = true;
            }
            foreach (Deal deal in yourDeals)
            {
                if (deal.internalName == splitString[i + 1])
                {
                    dealAlreadyAdded = true;
                }
            }
            foreach (string businessName in CrossSceneVariables.Instance.businessesInRadius)
            {
                if (businessName == splitString[i])
                {
                    dealInRadius = true;
                }
            }
            if (UIController.Instance.filterBusinessDeals)
            {
                inBusinessControlBlock.SetActive(true);
                if (CrossSceneVariables.Instance.inBusinessName != splitString[i])
                {
                    dealMismatchBusinessFilter = true;
                }
            } else
            {
                inBusinessControlBlock.SetActive(false);
            }
            if (filterDropdown.options[filterDropdown.value].text != "All")
            {
                if (filterDropdown.options[filterDropdown.value].text != splitString[i + 2])
                {
                    dealMismatchFilter = true;
                }
            }
            if (searchInput.text != "")
            {
                dealMismatchSearch = true;
                if (splitString[i].ToLower().Contains(searchInput.text.ToLower()) || 
                    splitString[i + 3].ToLower().Contains(searchInput.text.ToLower()))
                {
                    dealMismatchSearch = false;
                }
            }
            if (!dealExpired /*&& dealInRadius*/ && !dealAlreadyAdded && !dealMismatchBusinessFilter && !dealMismatchFilter && !dealMismatchSearch)
            {
                Deal deal = new Deal();
                deal.businessName = splitString[i];
                deal.internalName = splitString[i + 1];
                deal.category = splitString[i + 2];
                deal.tags = splitString[i + 3];
                deal.discountAmount = splitString[i + 4];
                deal.expiry = splitString[i + 5];
                deals.Add(deal);
            }
            Debug.Log(dealExpired + " " + dealAlreadyAdded + " "  + " " + dealMismatchBusinessFilter + " " + dealMismatchFilter + " " + dealMismatchSearch );
            PopulateAllDeals();
            PopulateYourDeals();
        }
    }

    private void PopulateAllDeals()
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
        yourDealsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * yourUsableDeals.Count);
        for (int i = 0; i < yourUsableDeals.Count; i++)
        {
            if (i % 2 == 0)
            {
                GameObject newObj = Instantiate(emptyDealPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newObj.transform.SetParent(yourDealsContent.transform, false);
                newObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                newObj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                newObj.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                newObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, i * -150, 0);
                newObj.GetComponent<DealCouponPopulator>().PopulateCoupon(yourUsableDeals[i]);
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
                newObj.GetComponent<DealCouponPopulator>().PopulateCoupon(yourUsableDeals[i]);
                newObj.GetComponent<DealCouponPopulator>().isSavedDeal = true;
            }
        }
    }

    public void OnEndSearch()
    {
        StartCoroutine(GetSavedDeals());
    }

    public void OnDropdownChanged()
    {
        StartCoroutine(GetSavedDeals());
    }

    public void OnSliderDistanceChanged()
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
    }
}

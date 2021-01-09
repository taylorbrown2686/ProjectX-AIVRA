using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DealCouponPopulator : MonoBehaviour
{
    private Deal thisDeal;
    [SerializeField] private Text issuer, amount, expiry;
    [SerializeField] private RawImage qrImage;
    private string dealInternalName;
    [SerializeField] private GameObject rewardCover;
    [SerializeField] private GameObject useDealScreen;
    [SerializeField] private DealsController dealController;
    public bool isSavedDeal;

    private void Start()
    {
        dealController = FindObjectOfType(typeof(DealsController)) as DealsController;
    }

    public void PopulateCoupon(Deal deal)
    {
        thisDeal = deal;
        dealInternalName = deal.internalName;
        issuer.text = deal.businessName;
        amount.text = deal.discountAmount;
        expiry.text = deal.expiry;
        qrImage.texture = deal.GenerateQR();
        if (deal.isReward == "1")
        {
            rewardCover.SetActive(true);
        } else
        {
            rewardCover.SetActive(false);
        }
    }

    public void Overlay()
    {
        if (isSavedDeal)
        {
            if (NewDealsController.Instance.customerDealOverlay.activeSelf)
            {
                NewDealsController.Instance.customerDealOverlay.SetActive(false);
            }
            else
            {
                NewDealsController.Instance.customerDealOverlay.SetActive(true);
                NewDealsController.Instance.customerCoupon.PopulateCoupon(thisDeal);
            }
        }
        else
        {
            if (NewDealsController.Instance.businessDealOverlay.activeSelf)
            {
                NewDealsController.Instance.businessDealOverlay.SetActive(false);
            }
            else
            {
                NewDealsController.Instance.businessDealOverlay.SetActive(true);
                NewDealsController.Instance.businessCoupon.PopulateCoupon(thisDeal);
            }
        }
    }

    public void CloseOverlay()
    {

    }

    public void OnClickSaveDeal()
    {
        StartCoroutine(SaveDeal());
    }

    public IEnumerator SaveDeal()
    {
        NewDealsController.Instance.businessSavingCover.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("dealInternalName", dealInternalName);
        form.AddField("dealIssuer", issuer.text);
        form.AddField("dealDiscount", amount.text);
        form.AddField("dealExpiry", expiry.text);
        form.AddField("isReward", 0);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/saveDealToCustomerAccount.php", form);
        yield return www;
        NewDealsController.Instance.OnEnable();
        Overlay();
    }

    public void UseDeal()
    {
        GameObject newScreen = Instantiate(useDealScreen, Vector3.zero, Quaternion.identity);
        newScreen.transform.SetParent(GameObject.Find("Deals").transform, false);
        newScreen.GetComponent<UseDealController>().Populate(dealInternalName, issuer.text, amount.text, expiry.text, rewardCover.activeSelf);
    }

    public void UseDealOnSpinOnClick()
    {
        if (thisDeal.businessName != CrossSceneVariables.Instance.inBusinessName)
        {
            print("this is not the proper location to claim that...");
            return;
        }
        else
        {
            StartCoroutine(UseDealOnSpin());
        }
    }
    private IEnumerator UseDealOnSpin()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("dealInternalName", dealInternalName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/updateDealUsedFromSavedCustomerDeals.php", form);
        yield return www;
        SceneManager.LoadScene("SpinToWin");
    }

    public void RemoveDealOnClick() {
        StartCoroutine(RemoveDeal());
    }

    private IEnumerator RemoveDeal() {
        NewDealsController.Instance.customerRemovingCover.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("dealInternalName", dealInternalName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/removeDealFromSavedCustomerDeals.php", form);
        yield return www;
        NewDealsController.Instance.OnEnable();
        Overlay();
    }
}

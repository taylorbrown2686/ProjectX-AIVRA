using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealCouponPopulator : MonoBehaviour
{
    [SerializeField] private Text issuer, amount, expiry;
    [SerializeField] private RawImage qrImage;
    private string dealInternalName;
    private bool overlayIsOn = false;
    [SerializeField] private GameObject businessOverlay, customerOverlay;
    [SerializeField] private GameObject useDealScreen;

    public bool isSavedDeal;

    public void PopulateCoupon(Deal deal)
    {
        dealInternalName = deal.internalName;
        issuer.text = deal.businessName;
        amount.text = deal.discountAmount;
        expiry.text = deal.expiry;
        qrImage.texture = deal.GenerateQR();
    }

    public void Overlay()
    {
        if (isSavedDeal)
        {
            overlayIsOn = !overlayIsOn;
            customerOverlay.SetActive(overlayIsOn);
            businessOverlay.SetActive(false);
        }
        else
        {
            overlayIsOn = !overlayIsOn;
            businessOverlay.SetActive(overlayIsOn);
            customerOverlay.SetActive(false);
        }
    }

    public void OnClickSaveDeal()
    {
        StartCoroutine(SaveDeal());
    }

    public IEnumerator SaveDeal()
    {
        Overlay();
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("dealInternalName", dealInternalName);
        form.AddField("dealIssuer", issuer.text);
        form.AddField("dealDiscount", amount.text);
        form.AddField("dealExpiry", expiry.text);
        WWW www = new WWW("http://localhost:8080/AIVRA-PHP/saveDealToCustomerAccount.php", form);
        yield return www;
        Debug.Log(www.text);
    }

    public void UseDeal()
    {
        GameObject newScreen = Instantiate(useDealScreen, Vector3.zero, Quaternion.identity);
        newScreen.transform.SetParent(GameObject.Find("Deals").transform, false);
        newScreen.GetComponent<UseDealController>().Populate(dealInternalName, issuer.text, amount.text, expiry.text);
    }
}

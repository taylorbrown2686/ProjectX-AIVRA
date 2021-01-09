using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;

public class ScanDealController : MonoBehaviour
{

    [SerializeField] private GameObject scanningDealScreen;
    [SerializeField] private Text errorText;

    unsafe void Update()
    {
        var cameraSubsystem = Camera.main.gameObject.GetComponent<ARCameraManager>();
        if (cameraSubsystem.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            using (image)
            {

                var conversionParams = new XRCpuImage.ConversionParams(image, TextureFormat.RGBA32, XRCpuImage.Transformation.MirrorY);

                var dataSize = image.GetConvertedDataSize(conversionParams);
                var bytesPerPixel = 4;

                var pixels = new Color32[dataSize / bytesPerPixel];
                fixed (void* ptr = pixels)
                {
                    image.Convert(conversionParams, new IntPtr(ptr), dataSize);
                }
                IBarcodeReader barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(pixels, image.width, image.height);
                if (result != null && FindObjectsOfType(typeof(ScanningDealController)).Length == 0)
                {
                    string[] splitString = result.Text.Split('&');
                    if (splitString[2] != BusinessController.Instance.businessName)
                    {
                        errorText.text = "You cannot scan " + splitString[2] + "'s deal.";
                        return;
                    }
                    else if (DateTime.Parse(splitString[4]) < DateTime.Today)
                    {
                        errorText.text = "This deal expired on " + splitString[4];
                        return;
                    }
                    StartCoroutine(CheckDealClaimed(splitString[0], splitString[1], splitString[2], splitString[3], splitString[4], Convert.ToBoolean(splitString[5])));
                    result = null;
                }
            }
        }
    }

    private IEnumerator CheckDealClaimed(string email, string internalNameM, string locationM, string amountM, string expiryM, bool isReward)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("internalname", internalNameM);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/checkIfDealIsUsed.php", form);
        yield return www;
        Debug.Log(www.text);
        if (www.text == "0")
        {
            GameObject newObj = Instantiate(scanningDealScreen, Vector3.zero, Quaternion.identity);
            newObj.transform.SetParent(GameObject.Find("Business").transform, false);
            newObj.GetComponent<ScanningDealController>().Populate(internalNameM, locationM, amountM, expiryM, isReward);
            newObj.GetComponent<ScanningDealController>().email = email;
        }
        else if (www.text == "1")
        {
            errorText.text = "This deal has been claimed by this person already.";
            yield break;
        }

    }

    public void BackToBusinessMain()
    {
        BusinessController.Instance.optionSelectScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

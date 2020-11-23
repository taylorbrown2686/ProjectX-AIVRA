using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;

public class ScanDealController : MonoBehaviour
{

    [SerializeField] private GameObject scanningDealScreen;

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
                Debug.Log(FindObjectsOfType(typeof(ScanningDealController)));
                if (result != null && FindObjectsOfType(typeof(ScanningDealController)).Length == 0)
                {
                    string[] splitString = result.Text.Split('&');
                    GameObject newObj = Instantiate(scanningDealScreen, Vector3.zero, Quaternion.identity);
                    newObj.transform.SetParent(GameObject.Find("Business").transform, false);
                    newObj.GetComponent<ScanningDealController>().Populate(splitString[1], splitString[2], splitString[3], splitString[4]);
                    newObj.GetComponent<ScanningDealController>().email = splitString[0];
                    result = null;
                }
            }
        }
    }
}

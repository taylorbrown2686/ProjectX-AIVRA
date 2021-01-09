using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TakePictureController : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToEnable;
    [SerializeField] private Canvas canvas;
    private XRCpuImage currentImage;
    private Color32[] currentImagePixels;
    public string currentBusinessName;

    private unsafe void Update()
    {
        var cameraSubsystem = Camera.main.gameObject.GetComponent<ARCameraManager>();
        if (cameraSubsystem.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            using (image)
            {

                var conversionParams = new XRCpuImage.ConversionParams(image, TextureFormat.RGBA32);

                var dataSize = image.GetConvertedDataSize(conversionParams);
                var bytesPerPixel = 4;

                var pixels = new Color32[dataSize / bytesPerPixel];
                fixed (void* ptr = pixels)
                {
                    image.Convert(conversionParams, new IntPtr(ptr), dataSize);
                    currentImage = image;
                    currentImagePixels = pixels;
                }
            }
        }
    }
    public void BackToGallery()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }

    public void TakePictureOnClick()
    {
        StartCoroutine(TakePicture());
    }

    private IEnumerator TakePicture()
    {
        canvas.enabled = false;
        yield return new WaitForSeconds(1f);
        var tex = new Texture2D(currentImage.width, currentImage.height);
        tex.SetPixels32(currentImagePixels, 0);
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        WWWForm form = new WWWForm();
        form.AddBinaryData("businessimage", bytes, "0");
        form.AddField("businessname", currentBusinessName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/uploadBusinessImage.php", form);
        yield return www;
        canvas.enabled = true;
        BackToGallery();
    }
}

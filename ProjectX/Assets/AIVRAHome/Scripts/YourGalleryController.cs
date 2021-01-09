using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourGalleryController : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToDisable;
    [SerializeField] private GameObject takePicScreen, locationInfoScreen;
    public string currentBusinessName;
    public int totalImages;

    private void OnEnable()
    {
        Debug.Log(currentBusinessName);
        locationInfoScreen.SetActive(false);
        StartCoroutine(GetImagesFromDB());
    }
    public void TakeNewPicture()
    {
        if (totalImages == 7)
        {
            //errorText.text = "You can only have 7 images max. Delete some images to continue.";
            return;
        }
        takePicScreen.SetActive(true);
        takePicScreen.GetComponent<TakePictureController>().currentBusinessName = currentBusinessName;
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }

    private IEnumerator GetImagesFromDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("businessName", currentBusinessName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getBusinessImages.php", form);
        yield return www;
        string[] splitString = www.text.Split(new string[] { "STRING_SPLIT" }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string str in splitString)
        {
            //convert to texture
            //instantiate obj (rawimage)
            //assign texture
            //perserve aspect???
            //move element down
        }
    }

    public void BackToLocInfo()
    {
        locationInfoScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

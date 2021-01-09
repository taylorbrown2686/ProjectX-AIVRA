using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIVRASays : MonoBehaviour
{

    [SerializeField] private GameObject businessButton, inLocationContainer, notInLocationContainer;
    [SerializeField] private Text businessButtonText, helloText, youAreAtText;
    [SerializeField] private GameObject transitionImage1, transitionImage2;
    [SerializeField] private RectTransform spinToWinTransform;
    private bool transitionInProgress = false;
    public IEnumerator EnteringLocation(string businessName)
    {
        if (transitionInProgress)
        {
            yield break;
        }
        transitionInProgress = true;
        while (transitionImage1.transform.rotation.eulerAngles.y < 180)
        {
            transitionImage1.transform.Rotate(new Vector3(0, -5, 0));
            transitionImage2.transform.Rotate(new Vector3(0, -5, 0));
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        inLocationContainer.SetActive(true);
        businessButton.SetActive(true);
        businessButtonText.text = businessName;
        youAreAtText.text = businessName;
        while (transitionImage1.transform.rotation.eulerAngles.y > 1)
        {
            transitionImage1.transform.Rotate(new Vector3(0, 5, 0));
            transitionImage2.transform.Rotate(new Vector3(0, 5, 0));
            yield return new WaitForSeconds(0.01f);
        }
        transitionInProgress = false;
        StartCoroutine(CheckForSpinToWin(businessName));
    }

    public IEnumerator LeavingLocation()
    {
        if (transitionInProgress)
        {
            yield break;
        }
        transitionInProgress = true;
        while (transitionImage1.transform.rotation.eulerAngles.y < 180)
        {
            transitionImage1.transform.Rotate(new Vector3(0, -5, 0));
            transitionImage2.transform.Rotate(new Vector3(0, -5, 0));
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        inLocationContainer.SetActive(false);
        businessButton.SetActive(false);
        businessButtonText.text = "Home";
        youAreAtText.text = "";
        while (transitionImage1.transform.rotation.eulerAngles.y > 1)
        {
            transitionImage1.transform.Rotate(new Vector3(0, 5, 0));
            transitionImage2.transform.Rotate(new Vector3(0, 5, 0));
            yield return new WaitForSeconds(0.01f);
        }
        transitionInProgress = false;
    }

    private IEnumerator OpenMenu()
    {
        if (transitionInProgress)
        {
            yield break;
        }
        transitionInProgress = true;
        while (transitionImage1.transform.rotation.eulerAngles.y < 180)
        {
            transitionImage1.transform.Rotate(new Vector3(0, -5, 0));
            transitionImage2.transform.Rotate(new Vector3(0, -5, 0));
            yield return new WaitForSeconds(0.01f);
        }
        inLocationContainer.SetActive(true);
        while (transitionImage1.transform.rotation.eulerAngles.y > 1)
        {
            transitionImage1.transform.Rotate(new Vector3(0, 5, 0));
            transitionImage2.transform.Rotate(new Vector3(0, 5, 0));
            yield return new WaitForSeconds(0.01f);
        }
        transitionInProgress = false;
    }

    private IEnumerator CloseMenu()
    {
        if (transitionInProgress)
        {
            yield break;
        }
        transitionInProgress = true;
        while (transitionImage1.transform.rotation.eulerAngles.y < 180)
        {
            transitionImage1.transform.Rotate(new Vector3(0, -5, 0));
            transitionImage2.transform.Rotate(new Vector3(0, -5, 0));
            yield return new WaitForSeconds(0.01f);
        }
        inLocationContainer.SetActive(false);
        while (transitionImage1.transform.rotation.eulerAngles.y > 1)
        {
            transitionImage1.transform.Rotate(new Vector3(0, 5, 0));
            transitionImage2.transform.Rotate(new Vector3(0, 5, 0));
            yield return new WaitForSeconds(0.01f);
        }
        transitionInProgress = false;
    }

    public void OpenMenuOnClick()
    {
        StartCoroutine(OpenMenu());
    }

    public void CloseMenuOnClick()
    {
        StartCoroutine(CloseMenu());
    }

    private IEnumerator CheckForSpinToWin(string businessName)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("businessName", businessName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getTimestampOfSpin.php", form);
        yield return www;
        DateTime date;
        if (www.text != "0")
        {
            DateTime.TryParse(www.text, out date);
            date = date.AddDays(1);
            if (date > DateTime.Now)
            {
                yield break;
            }
        }

        WWWForm form2 = new WWWForm();
        form2.AddField("businessName", businessName);
        WWW www2 = new WWW("http://65.52.195.169/AIVRA-PHP/checkIfSpinEnabledByBusinessName.php", form2);
        yield return www2;
        if (www2.text == "1")
        {
            for (int i = 0; i < 100; i++)
            {
                spinToWinTransform.localPosition += new Vector3(6, 0, 0);
                spinToWinTransform.Rotate(0,0,-0.9f);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}

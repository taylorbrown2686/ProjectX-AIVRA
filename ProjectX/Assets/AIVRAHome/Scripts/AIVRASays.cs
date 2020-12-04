using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIVRASays : MonoBehaviour
{

    [SerializeField] private GameObject businessButton, inLocationContainer, notInLocationContainer;
    [SerializeField] private Text businessButtonText, helloText, youAreAtText;
    [SerializeField] private GameObject transitionImage1, transitionImage2;
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
        youAreAtText.text = "You are at: " + businessName;
        while (transitionImage1.transform.rotation.eulerAngles.y > 1)
        {
            transitionImage1.transform.Rotate(new Vector3(0, 5, 0));
            transitionImage2.transform.Rotate(new Vector3(0, 5, 0));
            yield return new WaitForSeconds(0.01f);
        }
        transitionInProgress = false;
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
}

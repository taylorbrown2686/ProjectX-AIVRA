using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageBusinessController : MonoBehaviour
{
    private int index = 2;
    [SerializeField] private Text activePage;
    [SerializeField] private GameObject[] businessPages;

    void OnEnable()
    {
        EnablePage();
    }

    public void LeftArrow()
    {
        index -= 1;
        if (index < 0)
        {
            index = businessPages.Length - 1;
        }
        EnablePage();
    }

    public void RightArrow()
    {
        index += 1;
        if (index > businessPages.Length - 1)
        {
            index = 0;
        }
        EnablePage();
    }

    private void EnablePage()
    {
        DisableAllPages();
        businessPages[index].SetActive(true);
        ChangeActivePageText();
    }

    private void DisableAllPages()
    {
        foreach (GameObject obj in businessPages)
        {
            obj.SetActive(false);
        }
    }

    private void ChangeActivePageText()
    {
        switch (index)
        {
            case 0:
                activePage.text = "Add Feature";
                break;
            case 1:
                activePage.text = "Create Event";
                break;
            case 2:
                activePage.text = "Location Info";
                break;
            case 3:
                activePage.text = "View Events";
                break;
            case 4:
                activePage.text = "Metrics";
                break;
        }
    }
}

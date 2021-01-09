using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageBusinessController : MonoBehaviour
{
    [SerializeField] private Text activePage;
    [SerializeField] private GameObject[] businessPages;

    void OnEnable()
    {
        EnablePage(2);
    }

    public void EnablePage(int index)
    {
        DisableAllPages();
        businessPages[index].SetActive(true);
    }

    private void DisableAllPages()
    {
        foreach (GameObject obj in businessPages)
        {
            obj.SetActive(false);
        }
    }

    public void Back()
    {
        BusinessController.Instance.optionSelectScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

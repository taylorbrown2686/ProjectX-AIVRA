using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIVRAMainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject businessButton;
    [SerializeField] private Text helloText;

    void Start()
    {
        StartCoroutine(CheckBusinessStatus());
        StartCoroutine(SetHello());
    }

    private IEnumerator CheckBusinessStatus()
    {
        while (CrossSceneVariables.Instance.isBusiness == null)
        {
            yield return new WaitForSeconds(1f);
        }
        if (CrossSceneVariables.Instance.isBusiness == false)
        {
            //not a business
            businessButton.SetActive(false);
        }
        else
        {
            //is business
            businessButton.SetActive(true);
        }
    }

    private IEnumerator SetHello()
    {
        while (CrossSceneVariables.Instance.name == "")
        {
            yield return new WaitForSeconds(1f);
        }
        helloText.text = CrossSceneVariables.Instance.name;
    }
}

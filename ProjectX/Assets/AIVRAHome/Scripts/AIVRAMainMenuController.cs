using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVRAMainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject businessButton;

    IEnumerator Start()
    {
        while (CrossSceneVariables.Instance.isBusiness == null)
        {
            yield return new WaitForSeconds(1f);
        }
        if (CrossSceneVariables.Instance.isBusiness == false)
        {
            //not a business
            businessButton.SetActive(true);
        } else
        {
            //is business
            businessButton.SetActive(false);
        }
    }
}

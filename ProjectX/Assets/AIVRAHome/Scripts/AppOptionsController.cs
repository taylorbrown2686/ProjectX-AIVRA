using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppOptionsController : MonoBehaviour
{
    [SerializeField] private GameObject appOptions;
    [SerializeField] private GameObject[] notificationIndicators;

    private void OnEnable()
    {
        if (CrossSceneVariables.Instance.newNotifications)
        {
            foreach (GameObject obj in notificationIndicators)
            {
                obj.SetActive(true);
            }
        } else
        {
            foreach (GameObject obj in notificationIndicators)
            {
                obj.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        appOptions.SetActive(false);
    }
    public void AppOptionsToggle()
    {
        appOptions.SetActive(!appOptions.activeSelf);
    }
}

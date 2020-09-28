using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class ARPlacementAndPlaneDetectionController : MonoBehaviour
{
    ARPlaneManager m_ARPlaneManager;
    ARPlacementManager m_ARPlacementManager;

    public GameObject placeButton;
    public GameObject adjustButton;
    public GameObject scaleSlider;
    public GameObject startGameButton;



    private void Awake()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
        m_ARPlacementManager = GetComponent<ARPlacementManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        placeButton.SetActive(true);
        scaleSlider.SetActive(true);

        adjustButton.SetActive(false);
        startGameButton.SetActive(false);
        

        UIManager.Instance.roomWelcomeText.text = "Move phone to detect planes and place the ground!";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableARPlacementAndPlaneDetection()
    {
        m_ARPlaneManager.enabled = false;
        m_ARPlacementManager.enabled = false;
        SetAllPlanesActiveOrDeactive(false);

        scaleSlider.SetActive(false);
        startGameButton.SetActive(true);
        placeButton.SetActive(false);
        adjustButton.SetActive(true);
      

        UIManager.Instance.roomWelcomeText.text = "Welcome to Room";


    }

    public void EnableARPlacementAndPlaneDetection()
    {
        m_ARPlaneManager.enabled = true;
        m_ARPlacementManager.enabled = true;
        SetAllPlanesActiveOrDeactive(true);
        scaleSlider.SetActive(true);
        startGameButton.SetActive(true);
        placeButton.SetActive(true);
        adjustButton.SetActive(false);


        UIManager.Instance.roomWelcomeText.text = "Move phone to detect planes and place the ground!";  
    }




    private void SetAllPlanesActiveOrDeactive(bool value)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}

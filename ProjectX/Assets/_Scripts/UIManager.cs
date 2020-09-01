using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    
    [Header("Pre-Join Panel")]
    public GameObject PreJoinPanel;
    public Text StatusText;

    [Header("Room Panel")]
    public GameObject RoomPanel;
    public Text RoomWelcomeText;
    public Text ShareText;
    
    [Header("Loading")]
    public GameObject LoadingPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

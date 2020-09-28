using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    public Text playerName;
    [Header("Name Panel")]
    public InputField nameInputField;
    public GameObject namePanel;
        
    [Header("Pre-Join Panel")]
    public GameObject preJoinPanel;
    public Text statusText;

    [Header("Room Panel")]
    public GameObject roomPanel;
    public Text roomWelcomeText;
    public Text shareText;
    
    [Header("Loading")]
    public GameObject loadingPanel;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Name"))
        {
            namePanel.SetActive(false);
            preJoinPanel.SetActive(true);
            playerName.text = PlayerPrefs.GetString("Name");
        }
        else
        {
            namePanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveName() {

        string name = nameInputField.text.Trim(); //trim() removes all leading and trailing white spaces from string and after that saving it in a string

        if (!string.IsNullOrEmpty(name))
        {
            PlayerPrefs.SetString("Name", name);
            namePanel.SetActive(false);
            preJoinPanel.SetActive(true);
            playerName.text = name;
        }

    }
}

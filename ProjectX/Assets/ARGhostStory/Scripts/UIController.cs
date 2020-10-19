using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public GameObject namePanel;
    public InputField nameInputField;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Name"))
        {
            namePanel.SetActive(true);
        }
        else {
            namePanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nameInputCallback() {
        string name = nameInputField.text.Trim();
        if (!string.IsNullOrEmpty(name)){
            PlayerPrefs.SetString("Name", name);
            namePanel.SetActive(false);
        }
    }
}

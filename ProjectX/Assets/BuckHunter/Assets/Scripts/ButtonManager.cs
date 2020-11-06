using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public GameObject minnesotaButton, wisconsinButton, michiganButton, iowaButton, illinoisButton;

    public Sprite minnesotaSprite, wisconsinSprite, michiganSprite, iowaSprite, illinoisSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getStateButton(string stateName)
    {


        switch (stateName)
        {
            case "wisconsin":
                return wisconsinButton;

            case "minnesota":
                return minnesotaButton;

            case "michigan":
                return michiganButton;

            case "iowa":
                return iowaButton;

            case "illinois":
                return illinoisButton;

            default:
                return null;

        }

    }

    public void StateCompleted(string stateName, int score)
    {
        GameObject button;
        switch (stateName)
        {
            case "wisconsin":
                button = wisconsinButton;
                button.GetComponent<Image>().sprite = wisconsinSprite;
                button.GetComponentInChildren<Text>().text = "\n\n\nScore: " + score;
                button.GetComponentInChildren<Text>().color = Color.red;
                button.GetComponent<Button>().interactable = false;
                break;

            case "minnesota":
                button = minnesotaButton;
                button.GetComponent<Image>().sprite = minnesotaSprite;
                button.GetComponentInChildren<Text>().text = "\n\n\nScore: " + score;
                button.GetComponentInChildren<Text>().color = Color.red;
                button.GetComponent<Button>().interactable = false;
                break;

            case "michigan":
                button = michiganButton;
                button.GetComponent<Image>().sprite = michiganSprite;
                button.GetComponentInChildren<Text>().text = "\n\n\nScore: " + score;
                button.GetComponentInChildren<Text>().color = Color.red;
                button.GetComponent<Button>().interactable = false;
                break;

            case "iowa":
                button = iowaButton;
                button.GetComponent<Image>().sprite = iowaSprite;
                button.GetComponentInChildren<Text>().text = "\n\n\nScore: " + score;
                button.GetComponentInChildren<Text>().color = Color.red;
                button.GetComponent<Button>().interactable = false;
                break;

            case "illinois":
                button = illinoisButton;
                button.GetComponent<Image>().sprite = illinoisSprite;
                button.GetComponentInChildren<Text>().text = "\n\n\nScore: " + score;
                button.GetComponentInChildren<Text>().color = Color.red;
                button.GetComponent<Button>().interactable = false;
                break;

        }
    }
}

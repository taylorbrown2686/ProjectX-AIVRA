using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkDices : MonoBehaviour
{
    public GameObject[] dice;
    public Sprite[] selectedDice;
    public Sprite[] unselectedDice;
    // Start is called before the first frame update

    public void ShowDices()
    {
        foreach (GameObject d in dice)
        {
            d.SetActive(true);
        }
    }

    public void HideDices()
    {
        foreach (GameObject d in dice)
        {
            d.SetActive(false);
        }
    }


    public void UpdateDices(int[] value,bool[] selected)
    {

        for(int i=0; i<dice.Length; i++)
        {

            if(selected[i] == true)
                dice[i].GetComponent<Image>().sprite = selectedDice[value[i]];
            else
                dice[i].GetComponent<Image>().sprite = unselectedDice[value[i]];

        }

    }

}

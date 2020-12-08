using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceSelectionManager : MonoBehaviour
{
    public GameObject[] dice;
    public Sprite[] selectedDice;
    public Sprite[] unselectedDice;
    public int[] values;
    public bool[] isSelected;
    public bool[] isFrozen;
    // Start is called before the first frame update
    void Start()
    {
        isSelected = new bool[5];
        isFrozen = new bool[5];
        values = new int[5];
        //print(isSelected[0]);
    }

    public void SelectDice(int index)
    {
        if (isFrozen[index] == true)
            return;
        if(isSelected[index] == false) {
            dice[index].GetComponent<Image>().sprite = selectedDice[values[index]];
            isSelected[index] = true;
        }
        else
        {
            dice[index].GetComponent<Image>().sprite = unselectedDice[values[index]];
            isSelected[index] = false;
        }

        DiceGameManager.Instance.dc.CallSendDices(values, isSelected);

       /* foreach (bool x in isSelected)
            print(x);*/
    }

    public void ValuesChanged()
    {
        for (int i = 0; i < dice.Length; i++)
        {
            if (isSelected[i] == false)
            {
                dice[i].GetComponent<Image>().sprite = unselectedDice[values[i]];
            }
            else
            {
                dice[i].GetComponent<Image>().sprite = selectedDice[values[i]];
            }
        }
        DiceGameManager.Instance.dc.CallSendDices(values, isSelected);
    }

    public void Refresh()
    {
        isSelected = new bool[5];
        isFrozen = new bool[5];
        values = new int[5];
    }

    public void ShowDices()
    {
        foreach(GameObject d in dice)
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

    public void Stream()
    {
        DiceGameManager.Instance.dc.CallSendDices(values, isSelected);
    }

}

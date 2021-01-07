using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulButtonManager : MonoBehaviour
{
    public Text manaCost;
    public Image active;
    Color defaultColor;
    public int cost;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = manaCost.color;
    }

    public void Check(int currentMana)
    {
        if (currentMana < cost)
        {
            manaCost.color = Color.red;
            active.enabled = false;
        }
        else
        {
            manaCost.color = defaultColor;
            active.enabled = true;
        }
    }

    public void SetCost(int cost)
    {
        this.cost = cost;
        manaCost.text = cost + "";
    }
}

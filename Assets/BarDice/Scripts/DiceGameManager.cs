using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceGameManager : MonoBehaviour
{

    public Slider SliderX;
    public Slider SliderZ;
    public Toggle AutoPlacement;
    public Button StartButton;
    public DiceSelectionManager dsm;
    public NetworkDices nd;
    public DiceCommunication dc;
    public Button endTurn;
    public bool myTurn { get; internal set; }


    private static DiceGameManager _instance;
    private DiceGameManager()
    {


    }

    public static DiceGameManager Instance
    {
        get { return _instance; }
    }


    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
            dsm.HideDices();
            nd.gameObject.SetActive(false);
        }
    }
  
}

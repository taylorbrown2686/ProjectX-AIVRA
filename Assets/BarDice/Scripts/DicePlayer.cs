using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public int score;
    public bool isOut;
    public string nickname;
    public Text nicknameText;
    public Text scoreText;
    public string scoreInText;
    public GameObject turnImage;
    void Start()
    {
        isOut = false;
        score = 0;
    }

    
}

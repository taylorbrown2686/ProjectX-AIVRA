using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScale : MonoBehaviour
{
    // Start is called before the first frame update

    Slider sliderX;
    Slider sliderZ;
    GameObject gameZone;
    float x, y, z;
    void Start()
    {
        gameZone = GameObject.FindGameObjectWithTag("GameZone");
        sliderX = DiceGameManager.Instance.SliderX;
        sliderZ = DiceGameManager.Instance.SliderZ;
        sliderX.onValueChanged.AddListener(delegate { ChangeX(); });
        sliderZ.onValueChanged.AddListener(delegate { ChangeZ(); });
    }

    // Update is called once per frame
    void Update()
    {
        sliderX.value = gameZone.transform.localScale.x;
        sliderZ.value = gameZone.transform.localScale.z;
    }

    void ChangeX()
    {
        x = sliderX.value;
        z = gameZone.transform.localScale.z;
        y = (x + z) / 2;
        gameZone.transform.localScale = new Vector3(x,y,z);
    }

    void ChangeZ()
    {
        x = gameZone.transform.localScale.x;
        z = sliderZ.value;
        y = (x + z) / 2;
        gameZone.transform.localScale = new Vector3(x, y, z);
    }
}

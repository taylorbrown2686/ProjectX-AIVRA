using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamesController : MonoBehaviour
{
    [SerializeField] protected Image[] gameImages; //The ones shown in the wheel
    private int selectedGameIndex = 0;
    [SerializeField] private AIVRASays aivraSays;
    [SerializeField] private MarkerCreator markerCreator;

    
}

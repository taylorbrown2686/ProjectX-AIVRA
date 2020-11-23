using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneVariables : MonoBehaviour
{
    public string email;

    private static CrossSceneVariables instance = null;

    public static CrossSceneVariables Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

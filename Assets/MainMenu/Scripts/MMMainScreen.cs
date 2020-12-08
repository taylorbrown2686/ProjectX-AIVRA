using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMMainScreen : MonoBehaviour
{
    public void Login() {
      MMUIController.Instance.ChangeScreen(1); //Login
    }

    public void Signup() {
      MMUIController.Instance.ChangeScreen(4); //Signup
    }
}

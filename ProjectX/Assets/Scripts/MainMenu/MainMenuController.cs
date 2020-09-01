using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public int pageIndex = 0; //this represents the current page index, each page has OnEnable
    public GameObject mainScreen, loginScreen, signupScreen; //list of screens we switch between

    public void SwitchPage(int index) {
      switch (index) { //change page based on index passed into method
        case 0:
          DisableAllPages(); //start by turning everything off
          mainScreen.SetActive(true); //then enable the page matching the index
        break;
        case 1:
          DisableAllPages();
          loginScreen.SetActive(true);
        break;
        case 2:
          DisableAllPages();
          signupScreen.SetActive(true);
        break;
      }
    }

    private void DisableAllPages() { //method to turn off all pages
      mainScreen.SetActive(false);
      loginScreen.SetActive(false);
      signupScreen.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class MMVerifyingScreen : MonoBehaviour
{
    [SerializeField] private Text verifyingText;

    IEnumerator Start()
    {
        verifyingText.text = "Verifying your information";
        for (int i = 0; i < 3; i++)
        {
            verifyingText.text += ".";
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1.5f);
        verifyingText.text += "\n" + "Saving data in database";
        MMUIController.Instance.StartCoroutine(MMUIController.Instance.SendDataToDatabase());

        for (int i = 0; i < 3; i++)
        {
            verifyingText.text += ".";
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2f);
        verifyingText.text += "\n" + "Your account was created successfully!";
        yield return new WaitForSeconds(2f);
        MMUIController.Instance.ChangeScreen(1); //Login
    }

    private IEnumerator Failed()
    {
        verifyingText.text += "\n" + "Your email did not return a valid address, sending you back to the signup screen...";
        yield return new WaitForSeconds(3f);
        MMUIController.Instance.ChangeScreen(4);
    }
}

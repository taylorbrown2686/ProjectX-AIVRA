using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MMConfirmAccountScreen : MonoBehaviour
{
    [SerializeField] private InputField code;
    [SerializeField] private Text errorText;

    void OnEnable()
    {
        StartCoroutine(ResendCode(false));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MMUIController.Instance.ChangeScreen(1); //Login
        }
    }

    public void Continue()
    {
        StartCoroutine(VerifyCodeIsCorrect());
        //SceneManager.LoadScene("AIVRAHome");
    }

    private IEnumerator VerifyCodeIsCorrect()
    {
        WWWForm form = new WWWForm();
        form.AddField("emailorusername", MMUIController.Instance.usernameOrEmail);
        form.AddField("code", code.text);
        WWW www = new WWW("http://localhost:8080/AIVRA-PHP/updateVerifiedStatus.php", form);
        yield return www;
        if (www.text != "0")
        {
            errorText.text = "Your code was incorrect. If this keeps happening, try resending the code.";
        }
        else
        {
            errorText.text = "Your account was verified! Loading AIVRA...";
            EditPlayerPrefsToStayLoggedIn();
            SceneManager.LoadScene("AIVRAHome");
        }
    }

    public void ResendCodeOnClick()
    {
        StartCoroutine(ResendCode(true));
    }

    private IEnumerator ResendCode(bool resending)
    {
        Email email = new Email();
        int randomCode = Random.Range(100000, 999999);
        email.SendConfirmationEmail(CrossSceneVariables.Instance.email, randomCode);
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("code", randomCode);
        WWW www = new WWW("http://localhost:8080/AIVRA-PHP/updateVerificationCode.php", form);
        yield return www;
        if (resending)
        {
            errorText.text = "Code resent!";
        }
    }

    private void EditPlayerPrefsToStayLoggedIn()
    {
        if (MMUIController.Instance.stayLoggedIn)
        {
            PlayerPrefs.SetInt("stayloggedin", 1);
            PlayerPrefs.SetString("email", CrossSceneVariables.Instance.email);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("stayloggedin", 0);
            PlayerPrefs.SetString("email", CrossSceneVariables.Instance.email);
            PlayerPrefs.Save();
        }
    }
}

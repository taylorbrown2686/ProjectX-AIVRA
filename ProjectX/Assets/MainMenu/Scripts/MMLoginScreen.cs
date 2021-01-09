using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MMLoginScreen : MonoBehaviour
{
    [SerializeField] private InputField username, password;
    [SerializeField] private Text errorText;
    private bool userIsVerified = false;
    [SerializeField] private CrossSceneVariables csv;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MMUIController.Instance.ChangeScreen(0); //Main
        }
    }

    public void Login()
    {
        StartCoroutine(TryLogin());
    }

    public void ForgotPassword()
    {
        if (username.text.Contains("@") && username.text.Contains("."))
        {
            Email email = new Email();
            int randomCode = Random.Range(100000, 999999);
            StartCoroutine(UploadForgotPassCode(randomCode));
            email.SendResetPasswordEmail(username.text, randomCode);
            MMUIController.Instance.email = username.text;
            MMUIController.Instance.ChangeScreen(3); //Forgot Pass
        }
        else
        {
            errorText.text = "Please enter a valid email address to send a confirmation email to.";
        }
    }

    private IEnumerator TryLogin()
    {
        WWWForm form = new WWWForm();
        form.AddField("emailorusername", username.text);
        form.AddField("password", password.text);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/checkUsernamePasswordMatch.php", form);
        yield return www;
        if (www.text == "00")
        {
            WWWForm form2 = new WWWForm();
            form2.AddField("emailorusername", username.text);
            WWW www2 = new WWW("http://65.52.195.169/AIVRA-PHP/checkVerifiedStatus.php", form2);
            yield return www2;
            if (www2.text == "1")
            {
                if (!username.text.Contains("@"))
                {
                    StartCoroutine(GetEmailFromUsername(true));
                } else
                {
                    csv.email = username.text;
                    EditPlayerPrefs();
                    SceneManager.LoadScene("AIVRAHome");
                }
            }
            else
            {
                if (!username.text.Contains("@"))
                {
                    StartCoroutine(GetEmailFromUsername(false));
                } else
                {
                    csv.email = username.text;
                    MMUIController.Instance.email = username.text;
                    MMUIController.Instance.usernameOrEmail = username.text;
                    MMUIController.Instance.ChangeScreen(2); //Verify Account
                }
            }
        }
        else
        {
            errorText.text = "Your username or password was incorrect.";
        }
    }

    private IEnumerator GetEmailFromUsername(bool verified)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getEmailFromUsername.php", form);
        yield return www;
        
        csv.email = www.text;
        if (!verified)
        {
            MMUIController.Instance.usernameOrEmail = username.text;
            MMUIController.Instance.email = username.text;
            MMUIController.Instance.ChangeScreen(2); //Verify Account
        } else
        {
            EditPlayerPrefs();
            SceneManager.LoadScene("AIVRAHome");
        }
    }

    private IEnumerator UploadForgotPassCode(int randomCode)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", username.text);
        form.AddField("code", randomCode);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/updateForgotPassCode.php", form);
        yield return www;
    }

    public void StayLoggedIn()
    {
        MMUIController.Instance.stayLoggedIn = !MMUIController.Instance.stayLoggedIn;
    }

    private void EditPlayerPrefs()
    {
        if (MMUIController.Instance.stayLoggedIn)
        {
            PlayerPrefs.SetInt("stayloggedin", 1);
            PlayerPrefs.SetString("email", csv.email);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("stayloggedin", 0);
            PlayerPrefs.SetString("email", csv.email);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("showwarning"))
        {
            PlayerPrefs.SetInt("showwarning", 1);
            PlayerPrefs.Save();
        }
    }
}

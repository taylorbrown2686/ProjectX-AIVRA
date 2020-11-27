using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MMForgotPassScreen : MonoBehaviour
{
    [SerializeField] private InputField password, confPassword, code;
    [SerializeField] private Text errorText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MMUIController.Instance.ChangeScreen(1); //Login
        }
    }

    public void Continue()
    {
        StartCoroutine(VerifyFields());
    }

    private IEnumerator VerifyFields()
    {
        if (password.text != confPassword.text)
        {
            errorText.text = "Your passwords did not match";
            yield break;
        }
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");
        if (!hasNumber.IsMatch(password.text) || !hasUpperChar.IsMatch(password.text) || !hasMinimum8Chars.IsMatch(password.text))
        {
            errorText.text = "Your password must have a capital, symbol, number, and be 8+ characters.";
            yield break;
        } else
        {
            WWWForm form = new WWWForm();
            form.AddField("email", MMUIController.Instance.email);
            form.AddField("code", code.text);
            form.AddField("password", password.text);
            WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/checkForgotPassCode.php", form);
            yield return www;
            if (www.text == "0")
            {
                errorText.text = "Your code was valid! Resetting your password...";
                WWWForm form2 = new WWWForm();
                form2.AddField("password", password.text);
                form2.AddField("email", MMUIController.Instance.email);
                WWW www2 = new WWW("http://65.52.195.169/AIVRA-PHP/updateUserPassword.php", form);
                yield return www2;
                yield return new WaitForSeconds(2f);
                MMUIController.Instance.ChangeScreen(1);
            } else
            {
                errorText.text = "Your code was incorrect. If this continues, try going back and resending the code.";
            }
        }
    }
}

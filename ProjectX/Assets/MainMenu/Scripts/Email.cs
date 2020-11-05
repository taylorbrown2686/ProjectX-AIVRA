using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Mime;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Email
{
    //THIS SCRIPT HANDLES EMAILING USERS AND SCANNED USERS
    public void SendConfirmationEmail(string email, int verificationCode) {
      MailMessage message = new MailMessage();
      message.From = new MailAddress("AIVRAMailBot@gmail.com");
      message.To.Add(email);
      message.Subject = "AIVRA Account Verification";
      message.Body = "Hello, and welcome to AIVRA!\n\nPlease use the following code when prompted on login in the AIVRA app.\n\n" + verificationCode + "\n\nWe hope you enjoy all AIVRA has to offer!\n\nSincerely,\nYour Friends at AIVRA\n\nearthmedia.info, 715-797-2787";

      SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
      smtpServer.Port = 587;
      smtpServer.Credentials = new NetworkCredential("AIVRAMailBot@gmail.com", "Accu0423") as ICredentialsByHost;
      smtpServer.EnableSsl = true;
      ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors) {
          return true;
      };
      smtpServer.Send(message);
    }

    public void SendResetPasswordEmail(string email, int verificationCode) {
      MailMessage message = new MailMessage();
      message.From = new MailAddress("AIVRAMailBot@gmail.com");
      message.To.Add(email);
      message.Subject = "AIVRA Password Reset";
      message.Body = "Hello, and welcome to AIVRA!\n\nPlease return to the app, and use this code when resetting your password.\n\n" + verificationCode + "\n\nMake sure you pick a secure, easy to remember password. A length of 8 and minor complexity is required. We hope you enjoy all AIVRA has to offer!\n\nSincerely,\nYour Friends at AIVRA\n\nearthmedia.info, 715-797-2787";

      SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
      smtpServer.Port = 587;
      smtpServer.Credentials = new NetworkCredential("AIVRAMailBot@gmail.com", "Accu0423") as ICredentialsByHost;
      smtpServer.EnableSsl = true;
      ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors) {
          return true;
      };
      smtpServer.Send(message);
    }
}

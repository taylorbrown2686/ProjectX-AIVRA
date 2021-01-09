using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;

public class UseDealController : MonoBehaviour
{
    [SerializeField] private Text issuer, amount, expiry;
    [SerializeField] private RawImage qr;
    public string internalName;
    private bool reward;

    public void Populate(string internalNameM, string issuerM, string amountM, string expiryM, bool isReward)
    {
        internalName = internalNameM;
        issuer.text = issuerM;
        amount.text = amountM;
        expiry.text = expiryM;
        reward = isReward;
        qr.texture = GenerateQR();
    }

    public void Done()
    {
        Destroy(this.gameObject);
    }

    public Texture2D GenerateQR()
    {
        QrCodeEncodingOptions options = new QrCodeEncodingOptions
        {
            DisableECI = true,
            CharacterSet = "UTF-8",
            Width = 200,
            Height = 200
        };
        var writer = new BarcodeWriter();
        writer.Format = BarcodeFormat.QR_CODE;
        writer.Options = options;

        Color32[] pixels = writer.Write(CrossSceneVariables.Instance.email + "&" + internalName + "&" + issuer.text + "&" + amount.text + "&" + expiry.text + "&" + reward);
        Texture2D tex = new Texture2D(200, 200);
        tex.SetPixels32(pixels);
        tex.Apply();
        return tex;
    }
}

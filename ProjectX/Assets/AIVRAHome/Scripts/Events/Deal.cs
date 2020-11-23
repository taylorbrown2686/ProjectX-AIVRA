using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;

public class Deal
{
    public string email, businessName, internalName, externalName, category, tags, audience, discountType, discountAmount, expiry, amountDistributed, priceOfDiscountedItem;

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

        Color32[] pixels = writer.Write(businessName + "&" + discountAmount + "&" + expiry);
        Texture2D tex = new Texture2D(200, 200);
        tex.SetPixels32(pixels);
        tex.Apply();
        return tex;
    }

    //public Texture2D GenerateQRForUse()
    //{

    //}
}

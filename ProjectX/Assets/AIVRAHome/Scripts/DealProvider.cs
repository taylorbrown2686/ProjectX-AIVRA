using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;

public class DealProvider : MonoBehaviour
{
    /*protected string dealAmount, dealIssuer, dealExpiry; //These will be DB values when we have the DB
    [SerializeField] protected GameObject youveWon;

    void Start() {
      dealAmount = "20% off your order (one-time)";
      dealIssuer = "Azara's Smoke'n'Vape";
      dealExpiry = "12/31/2020";
    }

    protected IEnumerator OnWin() {
      var wonObj = Instantiate(youveWon, Vector3.zero, Quaternion.identity);
      wonObj.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
      wonObj.GetComponent<DealCouponPopulator>().PopulateCoupon(dealIssuer, dealAmount, dealExpiry, GenerateQR());
      wonObj.transform.localScale = new Vector3(0, 0, 0);
      while (wonObj.transform.localScale.x < 1) {
        wonObj.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        yield return new WaitForSeconds(0.025f);
      }
      yield return new WaitForSeconds(3f);
      while (wonObj.transform.localScale.x > 0) {
        wonObj.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        yield return new WaitForSeconds(0.025f);
      }
    }

    private Texture2D GenerateQR() {
      QrCodeEncodingOptions options = new QrCodeEncodingOptions {
        DisableECI = true,
        CharacterSet = "UTF-8",
        Width = 200,
        Height = 200
      };
      var writer = new BarcodeWriter();
      writer.Format = BarcodeFormat.QR_CODE;
      writer.Options = options;

      Color32[] pixels = writer.Write(dealIssuer + "&" + dealAmount + "&" + dealExpiry);
      Texture2D tex = new Texture2D(200, 200);
      tex.SetPixels32(pixels);
      tex.Apply();
      return tex;
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Attached to NearbyEventBox
public class AIVRASays : MonoBehaviour
{

    private RectTransform transform;
    private Text aivraText;

    void Start()
    {
        transform = this.GetComponent<RectTransform>();
        aivraText = this.GetComponentInChildren<Text>();
    }

    public IEnumerator Say(string message)
    {
        aivraText.text = message;
        while (transform.anchoredPosition.y > 0)
        {
            transform.anchoredPosition -= new Vector2(0, 10f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(7f);
        StartCoroutine(Unsay());
    }

    public IEnumerator Unsay()
    {
        while (transform.anchoredPosition.y < 600)
        {
            transform.anchoredPosition += new Vector2(0, 10f);
            yield return new WaitForSeconds(0.01f);
        }
        aivraText.text = "";
    }

    public void Menu()
    {

    }

    public void Deals()
    {
        Unsay();
        UIController.Instance.ChangePage("Deals");
    }

    public void Games()
    {
        Unsay();
        UIController.Instance.ChangePage("Games");
    }

}

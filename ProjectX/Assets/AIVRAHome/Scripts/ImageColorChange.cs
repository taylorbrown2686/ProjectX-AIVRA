using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorChange : MonoBehaviour
{
    private bool canChange = true;

    void Update()
    {
        if (canChange) {
            canChange = false;
            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor() {
        this.GetComponent<Image>().color = new Color(Random.Range(0,1f), Random.Range(0,1f), Random.Range(0,1f), 1f);
        yield return new WaitForSeconds(1f);
        canChange = true;
    }
}

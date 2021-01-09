using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarBlockController : MonoBehaviour
{
    [SerializeField] private GameObject loadingText;
    public void Yes()
    {
        loadingText.SetActive(true);
        SceneManager.LoadScene("AvatarSelection");
    }

    public void No()
    {
        Destroy(this.gameObject);
    }
}

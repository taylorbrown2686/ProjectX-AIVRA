using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarManager : MonoBehaviour
{
    private static AvatarManager _instance;
    
    public GameObject[] avatar;
    public GameObject[] prefab;
    public GameObject plane;
    float scale;
    public Shader depthMask;
    int avatarID=0, bagID=0, headID=0, weaponID=0, skinID=0;
    bool avatarselecting;
    public AvatarLocator[] al;

    private float initialDistance;
    private Vector3 initialScale;
    [SerializeField] private Transform placeAvatar;
    [SerializeField] private RenderTexture avatarRT;


    private AvatarManager()
    {


    }
    public static AvatarManager Instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        plane.transform.parent.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;

            Renderer planeRenderer = plane.GetComponent<Renderer>();
            planeRenderer.material.shader = depthMask;

            scale = Mathf.Sqrt(plane.transform.parent.transform.localScale.x * plane.transform.parent.transform.localScale.z) / 2;
            avatar = new GameObject[prefab.Length];
            al = new AvatarLocator[prefab.Length];
            for (int i = 0; i < prefab.Length; i++) { 

                avatar[i] = InstantiateObjectOnThePlane(prefab[i]);
                avatar[i].transform.LookAt(new Vector3(Camera.main.transform.position.x, avatar[i].transform.position.y,Camera.main.transform.position.z));
                al[i] = avatar[i].GetComponent<AvatarLocator>();
            }


        }
        initialScale = new Vector3(1,1,1);
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled
               || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return;
            }

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                for (int i = 0; i < avatar.Length; i++)
                {
                    initialScale = avatar[avatarID].transform.localScale;
                }
            }
            else
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                if (Mathf.Approximately(initialDistance, 0)) return;
                var factor = currentDistance / initialDistance;
                for (int i = 0; i < avatar.Length; i++)
                {
                    avatar[i].transform.localScale = initialScale * factor;
                }
            }
        }
    }

    public void SelectAvatar(int number)
    {
        avatarselecting = true;
        avatarID = number;
        for (int i = 0; i < avatar.Length; i++)
            if (i == number)
                avatar[i].SetActive(true);
            else
                avatar[i].SetActive(false);


        ChangeBag();
        ChangeHead();
        ChangeWeapon();
        ChangeSkin();
        avatarselecting = false;
    }

    public void RandomizeAvatar()
    {
        avatarselecting = true;
        SelectAvatar(Random.Range(0, 3));
        bagID = Random.Range(0, 14);
        headID = Random.Range(0, 14);
        weaponID = Random.Range(0, 10);
        skinID = Random.Range(0, 15);
        ChangeBag();
        ChangeHead();
        ChangeWeapon();
        ChangeSkin();
        avatarselecting = false;
    }

    public void ChangeBag()
    {
        GameObject locator = al[avatarID].bagLocator;
        if(avatarselecting == false)
            bagID++;
        if (locator.transform.childCount == bagID)
            bagID = 0;
        for (int i = 0; i < locator.transform.childCount; i++)
            if (i == bagID)
                locator.transform.GetChild(i).gameObject.SetActive(true);
            else
                locator.transform.GetChild(i).gameObject.SetActive(false);

    }

    public void ChangeHead()
    {
        GameObject locator = al[avatarID].headLocator;
        if (avatarselecting == false)
            headID++;
        if (locator.transform.childCount == headID)
            headID = 0;
        for (int i = 0; i < locator.transform.childCount; i++)
            if (i == headID)
                locator.transform.GetChild(i).gameObject.SetActive(true);
            else
                locator.transform.GetChild(i).gameObject.SetActive(false);

    }

    public void ChangeWeapon()
    {
        GameObject locator = al[avatarID].weaponLocator;
        if (avatarselecting == false)
            weaponID++;
        if (locator.transform.childCount == weaponID)
            weaponID = 0;
        for (int i = 0; i < locator.transform.childCount; i++)
            if (i == weaponID)
                locator.transform.GetChild(i).gameObject.SetActive(true);
            else
                locator.transform.GetChild(i).gameObject.SetActive(false);

    }

    public void ChangeSkin()
    {
        if (avatarselecting == false)
            skinID++;
        if (al[avatarID].material.Length == skinID)
            skinID = 0;
        al[avatarID].renderer.material = al[avatarID].material[skinID];
    }

    GameObject InstantiateObjectOnThePlane(GameObject gameObject)
    {
        GameObject childObject = Instantiate(gameObject, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localScale *= (scale*10);
        return childObject;

    }

    public void Done()
    {
        avatar[avatarID].transform.SetParent(placeAvatar);
        avatar[avatarID].transform.localPosition = new Vector3(0, 0, 0);
        avatar[avatarID].transform.localScale = new Vector3(1, 1, 1);
        avatar[avatarID].transform.rotation = Quaternion.identity;
        Destroy(avatar[avatarID].GetComponent<Animator>());
        StartCoroutine(SaveRenderTextureToPNG());
    }

    private IEnumerator SaveRenderTextureToPNG()
    {
        yield return new WaitForSeconds(0.5f);
        var tex = new Texture2D(avatarRT.width, avatarRT.height);
        RenderTexture.active = avatarRT;
        tex.ReadPixels(new Rect(0, 0, avatarRT.width, avatarRT.height), 0, 0);
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        WWWForm form = new WWWForm();
        form.AddBinaryData("avatarimage", bytes, CrossSceneVariables.Instance.username);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/uploadAvatarImage.php", form);
        yield return www;
        StartCoroutine(SaveAvatarToDB());
    }

    private IEnumerator SaveAvatarToDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("avatarid", avatarID);
        form.AddField("accessoryid", bagID);
        form.AddField("headid", headID);
        form.AddField("weaponid", weaponID);
        form.AddField("skinid", skinID);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/uploadAvatarIDs.php", form);
        yield return www;
        Debug.Log(www.text);
        SceneManager.LoadScene("AIVRAHome");
    }

}

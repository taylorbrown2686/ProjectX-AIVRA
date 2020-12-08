using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    private AvatarManager()
    {


    }
    public static AvatarManager Instance
    {
        get { return _instance; }
    }

    private void Start()
    {
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
            print(prefab.Length);
            avatar = new GameObject[prefab.Length];
            al = new AvatarLocator[prefab.Length];
            for (int i = 0; i < prefab.Length; i++) { 

                avatar[i] = InstantiateObjectOnThePlane(prefab[i]);
                avatar[i].transform.LookAt(new Vector3(Camera.main.transform.position.x, avatar[i].transform.position.y,Camera.main.transform.position.z));
                al[i] = avatar[i].GetComponent<AvatarLocator>();
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

}

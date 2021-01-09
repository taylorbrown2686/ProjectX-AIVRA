using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YourProfileController : MonoBehaviour
{
    [SerializeField] private Text usernameText, friendUsernameText;
    [SerializeField] private RawImage avatarImg, friendAvatarImg;
    [SerializeField] private InputField search;
    [SerializeField] private GameObject friendsBox, friendsContent, viewingFriendContainer;
    private string currentViewedUserEmail;

    private void Start()
    {
        usernameText.text = CrossSceneVariables.Instance.username;
        StartCoroutine(GetAvatarImage());
        StartCoroutine(GetFriendsList());
    }

    private IEnumerator GetAvatarImage()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameText.text);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getAvatarImageFromUsername.php", form);
        yield return www;
        Texture2D image = new Texture2D(1, 1);
        image.LoadImage(www.bytes);
        avatarImg.texture = image;
    }

    private IEnumerator GetFriendsList()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", search.text);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/pullFriends.php", form);
        yield return www;
        //pull bffs first (query1)
        //pull all others second (query2)
    }

    public void OnEndEdit()
    {
        foreach (Transform child in friendsContent.transform)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine(SearchFriends());
    }

    public IEnumerator SearchFriends()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", search.text);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/searchAccountByUsername.php", form);
        yield return www;
        string[] splitString = www.text.Split(new string[] { "STRING_SPLIT" }, System.StringSplitOptions.RemoveEmptyEntries);
        friendsContent.GetComponent<RectTransform>().sizeDelta = new Vector2((splitString.Length / 2) * 400, 0);
        for (int i = 0; i < splitString.Length; i += 3)
        {
            GameObject newObj = Instantiate(friendsBox, Vector3.zero, Quaternion.identity);
            newObj.transform.SetParent(friendsContent.transform);
            newObj.transform.localScale = new Vector3(1, 1, 1);
            newObj.transform.localPosition = new Vector3((i / 2) * 400, 0, 0);
            currentViewedUserEmail = splitString[i];
            newObj.GetComponentInChildren<Text>().text = splitString[i + 1];
            Texture2D image = new Texture2D(1, 1);
            image.LoadImage(System.Convert.FromBase64String(splitString[i + 2]));
            newObj.GetComponentInChildren<RawImage>().texture = image;
            newObj.GetComponent<Button>().onClick.AddListener(delegate { 
                ShowFriendsOnClick(newObj.GetComponentInChildren<Text>().text, image); });
        }
    }

    public void ShowFriendsOnClick(string username, Texture2D avatar)
    {
        StartCoroutine(ShowFriendsTab(username, avatar));
    }

    private IEnumerator ShowFriendsTab(string username, Texture2D avatar)
    {
        viewingFriendContainer.SetActive(true);
        friendUsernameText.text = username;
        friendAvatarImg.texture = avatar;
        yield return null;
    }

    public void AddFriendOnClick()
    {
        StartCoroutine(AddFriend());
    }

    private IEnumerator AddFriend()
    {
        WWWForm form = new WWWForm();
        form.AddField("sender", CrossSceneVariables.Instance.email);
        form.AddField("receiver", currentViewedUserEmail);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/sendFriendRequest.php", form);
        yield return www;
        Notifier.Instance.StartCoroutine(Notifier.Instance.SendNotification(
            CrossSceneVariables.Instance.email, currentViewedUserEmail, "FriendReq", usernameText.text + " would like to be your friend."));
    }

    public void BlockUserOnClick()
    {
        StartCoroutine(BlockUser());
    }

    private IEnumerator BlockUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("blocker", CrossSceneVariables.Instance.email);
        form.AddField("blockeduser", currentViewedUserEmail);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/blockUser.php", form);
        yield return www;
    }

    public void BackToYourProfile()
    {
        viewingFriendContainer.SetActive(false);
    }
}

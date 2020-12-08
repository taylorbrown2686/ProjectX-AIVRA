using ExitGames.Client.Photon;
using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    // Start is called before the first frame update

    ChatClient chatClient;
    string userID = "user";

 

    void Start()
    {
        chatClient = new ChatClient(this);
        // Set your favourite region. "EU", "US", and "ASIA" are currently supported.
        chatClient.ChatRegion = "US";
        chatClient.Connect("808855c8-16fe-4ddb-8fbc-9edbaab74cbe", "whatever", new AuthenticationValues(userID));
       
        print("sdfdsfsdsdfsd");
    }

    private void Update()
    {
        chatClient.Service();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
        print("state changed");
    }

    public void OnConnected()
    {
        print("Chat works");
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
}

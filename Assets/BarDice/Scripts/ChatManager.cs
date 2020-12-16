using ExitGames.Client.Photon;
using Photon.Chat;
using System;
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
        print(state);
    }

    public void OnConnected()
    {
        print("Chat works");
        chatClient.Subscribe(new string[] { "public" });
        print("sdfdsfsdsdfsd");
        chatClient.SendPrivateMessage("user", "hii");
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        print(senders.Length);
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}{1}={2}, ", msgs, senders[i], messages[i]);
        }
        Console.WriteLine("OnGetMessages: {0} ({1}) > {2}", channelName, senders.Length, msgs);
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        print(message);
        print(channelName);
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatClient.PublishMessage("public", "hiii");
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        print(user);
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
}

using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChatManager : MonoBehaviour, IChatClientListener
{
    // Start is called before the first frame update

    ChatClient chatClient;
    string userID = "userxx";
    ChatChannel cc;
    bool connected = false;
    void Start()
    {
        


    }

    public void Connect(string userID)
    {
        print(userID + " is trying to connect");
        chatClient = new ChatClient(this)
        {
            // Set your favourite region. "EU", "US", and "ASIA" are currently supported.
            ChatRegion = "US"
        };
        chatClient.Connect("808855c8-16fe-4ddb-8fbc-9edbaab74cbe", "whatever", new AuthenticationValues(userID));
    }

    private void Update()
    {
        if (this.chatClient != null)
            chatClient.Service();
        if (connected == true)
        {
       //     print(cc.Subscribers.Count);
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
        print("state: " + state);
    }

    public void OnConnected()
    {
        chatClient.SetOnlineStatus(1);
        print("Chat works");
        
        chatClient.Subscribe("public", creationOptions: new ChannelCreationOptions { PublishSubscribers = true });
        print("sdfdsfsdsdfsd");

        chatClient.SendPrivateMessage("user", "hii");
        
        print("connected " + connected);

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
        string message_ = (string)message;
        if (message_.Substring(0, 1) == "/")
        {
            if (message_.Substring(1, 7) == "invite") //if gets "/invite roomname"
            {
                PhotonNetwork.JoinRoom(message_.Substring(8, message_.Length));
            }

            if (message_.Substring(1, 5) == "kick")
            {
                PhotonNetwork.LeaveRoom();
            }

            if (message_.Substring(1, 4) == "add")
            {
                throw new System.NotImplementedException();
            }

            if (message_.Substring(1, 5) == "mute")
            {
                throw new System.NotImplementedException();
            }

            if (message_.Substring(1, 7) == "remove")
            {
                throw new System.NotImplementedException();
            }
        }
        else
            print(message);
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Console.WriteLine("Status change for: {0} to: {1}", user, status);
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatClient.PublishMessage("public", "hiii");

        print(this.chatClient.PrivateChannels.Count);

        print(this.chatClient.PublicChannels.Count);

        print(this.chatClient.TryGetChannel("public", out cc));

        foreach (string user in cc.Subscribers)
            print(user);

        connected = true;
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        print("user list: " + user);
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        print("left user list: " + user);
    }
}

/*
 * const int 	Offline = 0
 	(0) Offline. More...
 
const int 	Invisible = 1
 	(1) Be invisible to everyone. Sends no message. More...
 
const int 	Online = 2
 	(2) Online and available. More...
 
const int 	Away = 3
 	(3) Online but not available. More...
 
const int 	DND = 4
 	(4) Do not disturb. More...
 
const int 	LFG = 5
 	(5) Looking For Game/Group. Could be used when you want to be invited or do matchmaking. More...
 
const int 	Playing =Us 6
 	(6) Could be used when in a room, playing

    */
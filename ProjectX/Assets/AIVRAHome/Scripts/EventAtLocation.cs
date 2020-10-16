using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAtLocation
{
    [SerializeField] private string eventType;
    [SerializeField] private string eventTitle;
    [SerializeField] private string eventDesc;
    [SerializeField] private string eventSubDesc;

    public string EventType {get => eventType; set => eventType = value;}
    public string EventTitle {get => eventTitle; set => eventTitle = value;}
    public string EventDesc {get => eventDesc; set => eventDesc = value;}
    public string EventSubDesc {get => eventSubDesc; set => eventSubDesc = value;}

    public EventAtLocation(string type, string title, string desc, string subDesc) {
      eventType = type;
      eventTitle = title;
      eventDesc = desc;
      eventSubDesc = subDesc;
    }
}

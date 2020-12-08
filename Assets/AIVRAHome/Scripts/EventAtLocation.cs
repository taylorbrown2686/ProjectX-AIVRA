using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAtLocation : MonoBehaviour
{
    [SerializeField] private string eventType;
    [SerializeField] private string eventTitle;
    [SerializeField] private string eventDesc;
    [SerializeField] private string eventSubDesc;
    [SerializeField] private float eventRadius; //km

    public string EventType {get => eventType; set => eventType = value;}
    public string EventTitle {get => eventTitle; set => eventTitle = value;}
    public string EventDesc {get => eventDesc; set => eventDesc = value;}
    public string EventSubDesc {get => eventSubDesc; set => eventSubDesc = value;}
    public float EventRadius {get => eventRadius; set => eventRadius = value;}

    public EventAtLocation(string type, string title, string desc, string subDesc, float radius) {
      eventType = type;
      eventTitle = title;
      eventDesc = desc;
      eventSubDesc = subDesc;
      eventRadius = radius;
    }
}

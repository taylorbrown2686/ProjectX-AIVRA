using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;

public class MarkerData : MonoBehaviour
{
    //Variables
    public int UID;
    private float latitude, longitude;
    private string address;
    public string businessName; //CHANGE TO PRIVATE LATER WITH DATABASE
    [SerializeField] private string eventType;
    [SerializeField] private string eventTitle;
    [SerializeField] private string eventDesc;
    [SerializeField] private string eventSubDesc;
    [SerializeField] private EventAtLocation hostedEvent;
    public Texture2D locationImage; //CHANGE TO PRIVATE LATER WITH DATABASE

    //Properties
    public float Latitude {get => latitude;}
    public float Longitude {get => longitude;}
    public string Address {get => address; set => address = value;}
    public EventAtLocation HostedEvent {get => hostedEvent;}
    public Texture2D LocationImage {get => locationImage; set => locationImage = value;}

    void Awake() {
      UID = UnityEngine.Random.Range(10000000, 99999999);
      hostedEvent = new EventAtLocation(eventType, eventTitle, eventDesc, eventSubDesc);
    }

    IEnumerator Start() {
      yield return new WaitForSeconds(0.1f); //Gives the PlaceAtLocation class time to initialize
      latitude = Convert.ToSingle(this.GetComponent<PlaceAtLocation>().Location.Latitude);
      longitude = Convert.ToSingle(this.GetComponent<PlaceAtLocation>().Location.Longitude);
      EventCoordinates.Instance.coordsWithUID.Add(new Vector2(latitude, longitude), UID);
      GetLocationFromCoordinate glfc = this.gameObject.AddComponent<GetLocationFromCoordinate>();
      glfc.StartCoroutine(glfc.GetLocationName(new Vector2(latitude, longitude)));
    }
}

/*         INFINITY CODE         */
/*   https://infinity-code.com   */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ARLocation;
/// <summary>
/// Base class for marker manager components
/// </summary>
[Serializable]
[DisallowMultipleComponent]
[AddComponentMenu("")]
public class OnlineMapsMarkerManager : OnlineMapsMarkerManagerBase<OnlineMapsMarkerManager, OnlineMapsMarker>
{
    //TAYLOR: Added to get the boolean related to map transform
    public AdjustMap mapPlane;
    private ARLocationProvider provider;
    //TAYLOR: Added so I can change the marker texture for different markers
    public MarkerImages markerImages;
    //TAYLOR: Added so I can control how often markers are updated
    private bool canUpdateEnemies = true, canUpdatePlayer = true;

    /// <summary>
    /// Texture to be used if marker texture is not specified.
    /// </summary>
    public Texture2D defaultTexture;

    /// <summary>
    /// Align for new markers
    /// </summary>
    public OnlineMapsAlign defaultAlign = OnlineMapsAlign.Bottom;

    /// <summary>
    /// Specifies whether to create a 2D marker by pressing M under the cursor.
    /// </summary>
    public bool allowAddMarkerByM = true;

    /// <summary>
    /// Create a new marker
    /// </summary>
    /// <param name="location">Location of the marker (X - longitude, Y - latitude)</param>
    /// <param name="label">Tooltip</param>
    /// <returns>Instance of the marker</returns>
    public static OnlineMapsMarker CreateItem(Vector2 location, string label)
    {
        if (instance != null) return instance.Create(location.x, location.y, null, label);
        return null;
    }

    /// <summary>
    /// Create a new marker
    /// </summary>
    /// <param name="location">Location of the marker (X - longitude, Y - latitude)</param>
    /// <param name="texture">Texture of the marker</param>
    /// <param name="label">Tooltip</param>
    /// <returns>Instance of the marker</returns>
    public static OnlineMapsMarker CreateItem(Vector2 location, Texture2D texture = null, string label = "")
    {
        if (instance != null) return instance.Create(location.x, location.y, texture, label);
        return null;
    }

    /// <summary>
    /// Create a new marker
    /// </summary>
    /// <param name="longitude">Longitude</param>
    /// <param name="latitude">Latitude</param>
    /// <param name="label">Tooltip</param>
    /// <returns>Instance of the marker</returns>
    public static OnlineMapsMarker CreateItem(double longitude, double latitude, string label)
    {
        if (instance != null) return instance.Create(longitude, latitude, null, label);
        return null;
    }

    /// <summary>
    /// Create a new marker
    /// </summary>
    /// <param name="longitude">Longitude</param>
    /// <param name="latitude">Latitude</param>
    /// <param name="texture">Texture of the marker</param>
    /// <param name="label">Tooltip</param>
    /// <returns>Instance of the marker</returns>
    public static OnlineMapsMarker CreateItem(double longitude, double latitude, Texture2D texture = null, string label = "")
    {
        if (instance != null) return instance.Create(longitude, latitude, texture, label);
        return null;
    }

    /// <summary>
    /// Create a new marker
    /// </summary>
    /// <param name="location">Location of the marker (X - longitude, Y - latitude)</param>
    /// <param name="texture">Texture of the marker</param>
    /// <param name="label">Tooltip</param>
    /// <returns>Instance of the marker</returns>
    public OnlineMapsMarker Create(Vector2 location, Texture2D texture = null, string label = "")
    {
        if (instance != null) return Create(location.x, location.y, texture, label);
        return null;
    }

    /// <summary>
    /// Create a new marker
    /// </summary>
    /// <param name="longitude">Longitude</param>
    /// <param name="latitude">Latitude</param>
    /// <param name="texture">Texture of the marker</param>
    /// <param name="label">Tooltip</param>
    /// <returns>Instance of the marker</returns>
    public OnlineMapsMarker Create(double longitude, double latitude, Texture2D texture = null, string label = "")
    {
        if (texture == null) texture = defaultTexture;
        OnlineMapsMarker marker = _CreateItem(longitude, latitude);
        marker.manager = this;
        marker.texture = texture;
        marker.label = label;
        marker.align = defaultAlign;
        marker.scale = defaultScale;
        marker.Init();
        Redraw();
        return marker;
    }

    public override OnlineMapsSavableItem[] GetSavableItems()
    {
        if (savableItems != null) return savableItems;

        savableItems = new []
        {
            new OnlineMapsSavableItem("markers", "2D Markers", SaveSettings)
            {
                priority = 90,
                loadCallback = LoadSettings
            }
        };

        return savableItems;
    }

    /// <summary>
    /// Load items and component settings from JSON
    /// </summary>
    /// <param name="json">JSON item</param>
    public void LoadSettings(OnlineMapsJSONItem json)
    {
        OnlineMapsJSONItem jitems = json["items"];
        RemoveAll();
        foreach (OnlineMapsJSONItem jitem in jitems)
        {
            OnlineMapsMarker marker = new OnlineMapsMarker();

            double mx = jitem.ChildValue<double>("longitude");
            double my = jitem.ChildValue<double>("latitude");

            marker.SetPosition(mx, my);

            marker.range = jitem.ChildValue<OnlineMapsRange>("range");
            marker.label = jitem.ChildValue<string>("label");
            marker.texture = OnlineMapsUtils.GetObject(jitem.ChildValue<int>("texture")) as Texture2D;
            marker.align = (OnlineMapsAlign)jitem.ChildValue<int>("align");
            marker.rotation = jitem.ChildValue<float>("rotation");
            marker.enabled = jitem.ChildValue<bool>("enabled");
            Add(marker);
        }

        OnlineMapsJSONItem jsettings = json["settings"];
        defaultTexture = OnlineMapsUtils.GetObject(jsettings.ChildValue<int>("defaultTexture")) as Texture2D;
        defaultAlign = (OnlineMapsAlign)jsettings.ChildValue<int>("defaultAlign");
        defaultScale = jsettings.ChildValue<float>("defaultScale");
        allowAddMarkerByM = jsettings.ChildValue<bool>("allowAddMarkerByM");
    }

    protected override OnlineMapsJSONItem SaveSettings()
    {
        OnlineMapsJSONItem jitem = base.SaveSettings();
        jitem["settings"].AppendObject(new
        {
            defaultTexture = defaultTexture != null? defaultTexture.GetInstanceID(): -1,
            defaultAlign = (int)defaultAlign,
            defaultScale,
            allowAddMarkerByM
        });
        return jitem;
    }

    protected override void Start()
    {
        base.Start();
        mapPlane = GameObject.Find("MapPlane").GetComponent<AdjustMap>();
        provider = GameObject.Find("ARLocationRoot").GetComponent<ARLocationProvider>();
        markerImages = this.gameObject.GetComponent<MarkerImages>();

        foreach (OnlineMapsMarker marker in items)
        {
            marker.manager = this;
            marker.Init();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (allowAddMarkerByM && Input.GetKeyUp(KeyCode.M))
        {
            double lng, lat;
            if (map.control.GetCoords(out lng, out lat)) Create(lng, lat);
        }

        /*if (Input.touchCount == 1 && mapPlane.mapCanMove && CheckForCollisionWithMap() == "MapPlane") {
          var touch = Input.GetTouch(0);
          RemoveMarkerByLabel("Portal");
          double lat, lng;
          if (map.control.GetCoords(out lng, out lat)) Create(new Vector2((float)lng, (float)lat), markerImages.markerTextures[3], "Portal");
        }*/

        if (canUpdatePlayer) {
          RemoveMarkerByLabel("Player");
          StartCoroutine(UpdateMapPositionOfPlayer());
        }
        if (canUpdateEnemies) {
          RemoveMarkerByLabel("Enemy");
          StartCoroutine(UpdateMapPositionOfEnemies());
        }
    }

    private IEnumerator UpdateMapPositionOfEnemies() {
      canUpdateEnemies = false;
      foreach (GameObject location in GameObject.FindGameObjectsWithTag("Enemy")) {
        PlaceAtLocation coords = location.GetComponent<PlaceAtLocation>();
        Create(new Vector2((float)coords.Location.Longitude, (float)coords.Location.Latitude), markerImages.markerTextures[2], "Enemy");
      }
      yield return new WaitForSeconds(5f);
      canUpdateEnemies = true;
    }

    private IEnumerator UpdateMapPositionOfPlayer() {
      canUpdatePlayer = false;
      Create(new Vector2(Input.location.lastData.longitude, Input.location.lastData.latitude), markerImages.markerTextures[0], "Player");
      yield return new WaitForSeconds(1f);
      canUpdatePlayer = true;
    }

    private void RemoveMarkerByLabel(string labelToRemove) {
      List<OnlineMapsMarker> tempList = new List<OnlineMapsMarker>();
      tempList = this.items;
      foreach (OnlineMapsMarker marker in tempList) {
        if (marker.label == labelToRemove) {
          this.Remove(marker);
        }
      }
    }

    private string CheckForCollisionWithMap() {
      if (Input.touchCount == 1) {
         Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
         RaycastHit hit;
         if(Physics.Raycast(ray, out hit))
         {
             if (hit.collider != null) {

                 GameObject touchedObject = hit.transform.gameObject;

                 return hit.transform.gameObject.name;
             }
         }
         return "";
       }
       return "";
    }
}

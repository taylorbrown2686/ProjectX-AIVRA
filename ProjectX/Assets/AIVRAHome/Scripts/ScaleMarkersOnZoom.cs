using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMarkersOnZoom : MonoBehaviour
{
      public int defaultZoom = 18;
      [SerializeField] private OnlineMapsMarkerManager markerManager;
      //[SerializeField] private MarkerCreator markerCreator;

      private void Start()
      {
          OnlineMaps.instance.OnChangeZoom += OnChangeZoom;
          OnChangeZoom();
      }

      private void OnChangeZoom()
      {
        OnlineMaps map = this.GetComponent<OnlineMaps>();
        foreach (OnlineMapsMarker marker in markerManager.items)
        {
            if (map.zoom < defaultZoom)
            {
                marker.scale = 1;
            }
            else
            {
                marker.scale = marker.originalRadius / (.0008f * Mathf.Pow(2, 22 - map.zoom));
            }
        }
        
      }
}

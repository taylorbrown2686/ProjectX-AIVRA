using System;
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
                marker.scale = 2;
            }
            else
            {
                marker.scale = Convert.ToSingle((1.40625f * Math.Pow(map.zoom, 2)) - (51.5625f * map.zoom) + 473.75f);
            }
        }
        
      }
}

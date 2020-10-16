using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMarkersOnZoom : MonoBehaviour
{
      public int defaultZoom = 18;
      [SerializeField] private OnlineMapsMarkerManager markerManager;

      private void Start()
      {
          OnlineMaps.instance.OnChangeZoom += OnChangeZoom;
          OnChangeZoom();
      }

      private void OnChangeZoom()
      {
          foreach (OnlineMapsMarker marker in markerManager.items)
          {
            float originalScale = 1 << defaultZoom;
            float currentScale = 1 << OnlineMaps.instance.zoom;
            if (originalScale / currentScale < 0.5f) {
              marker.scale = currentScale / originalScale;
            } else {
              marker.scale = 1f;
            }
          }
      }
}

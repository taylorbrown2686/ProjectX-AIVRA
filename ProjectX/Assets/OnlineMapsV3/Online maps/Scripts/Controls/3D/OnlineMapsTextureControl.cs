﻿/*         INFINITY CODE         */
/*   https://infinity-code.com   */
using UnityEngine;
using System.Collections;
/// <summary>
/// Class control the map for the Texture.
/// </summary>
[System.Serializable]
[AddComponentMenu("Infinity Code/Online Maps/Controls/Texture")]
[RequireComponent(typeof(MeshRenderer))]
public class OnlineMapsTextureControl : OnlineMapsControlBase3D
{
    /// <summary>
    /// Singleton instance of OnlineMapsTextureControl control.
    /// </summary>
    public new static OnlineMapsTextureControl instance
    {
        get { return _instance as OnlineMapsTextureControl; }
    }

    public override bool GetCoords(Vector2 position, out double lng, out double lat)
    {
        lng = lat = 0;
        double tx, ty;
        if (!GetTile(position, out tx, out ty)) return false;

        map.projection.TileToCoordinates(tx, ty, map.zoom, out lng, out lat);
        return true;
    }

    public override bool GetTile(Vector2 position, out double tx, out double ty)
    {
        RaycastHit hit;

        tx = ty = 0;
        if (!cl.Raycast(activeCamera.ScreenPointToRay(position), out hit, OnlineMapsUtils.maxRaycastDistance)) return false;

        if (rendererInstance == null || rendererInstance.sharedMaterial == null || rendererInstance.sharedMaterial.mainTexture == null) return false;

        Vector2 r = hit.textureCoord;

        float zoomCoof = map.zoomCoof;
        r.x = (r.x - 0.5f) * zoomCoof;
        r.y = (r.y - 0.5f) * zoomCoof;

        int countX = map.width / OnlineMapsUtils.tileSize;
        int countY = map.height / OnlineMapsUtils.tileSize;

        map.GetTilePosition(out tx, out ty);

        tx += countX * r.x;
        ty -= countY * r.y;
        return true;
    }

    public override void SetTexture(Texture2D texture)
    {
        base.SetTexture(texture);
        GetComponent<Renderer>().sharedMaterial.mainTexture = texture;

        StartCoroutine(SendToServerDelayed()); //Delayed so the GPS sensor can catch up
    }

    private IEnumerator SendToServerDelayed() {
      if (map.latitude == 0 || map.longitude == 0) {
        yield return new WaitForSeconds(5f);
      }
      //TAYLOR: Custom behavior for sending texture to server
      Debug.Log("TEXTURE: " + map.latitude.ToString() + ", " + map.longitude.ToString());
      SendSatImageToServer send = this.gameObject.GetComponent<SendSatImageToServer>(); //Script is attached to same object
      send.StartCoroutine(send.SendImageToServer());
      //TAYLOR: End of changes
    }
}

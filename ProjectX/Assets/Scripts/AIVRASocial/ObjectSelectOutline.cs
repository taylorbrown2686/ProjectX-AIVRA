using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script outlines an object on the Object Wheel for selection and spawning
public class ObjectSelectOutline : MonoBehaviour
{
    public Shader outlineShader, defaultShader;
    private GameObject objectToOutline;
    public ObjectSelectWheel wheel;

    void OnTriggerEnter(Collider col) { //When an object enters the collider
      if (col.tag == "Placeable") { //If it has placeable tag
        objectToOutline = col.transform.gameObject; //Get the material on the renderer
        ApplyOutline();
      }
    }

    void OnTriggerExit(Collider col) {
      if (col.tag == "Placeable") {
        RemoveOutline();
      }
    }

    private void ApplyOutline() {
      Material mat = objectToOutline.GetComponent<Renderer>().material;
      mat.shader = outlineShader; //Set the shader to 'Outline' and set some properties for it
      mat.SetColor("_OutlineColor", Color.cyan);
      mat.SetFloat("_Outline", 0.0001f);
      wheel.SetObjectToSpawnIndex(objectToOutline.name); //Call the method in ObjectSelectWheel to set the object index
    }

    private void RemoveOutline() {
      Material mat = objectToOutline.GetComponent<Renderer>().material;
      mat.shader = defaultShader; //Reset the shader and object
      objectToOutline = null;
    }
}

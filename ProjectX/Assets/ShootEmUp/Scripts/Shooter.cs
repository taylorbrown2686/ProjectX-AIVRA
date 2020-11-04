using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    private GameObject spawnedShootable;
    public GameObject SpawnedShootable {get => spawnedShootable;}
    [SerializeField] private GameObject[] shootableObjects;


    public void PrepareFire() {

        if (PhotonNetwork.isMasterClient) {
            spawnedShootable = PhotonNetwork.Instantiate(GetRandomShootableObject().name,
              this.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity, 0);
            //    spawnedShootable.transform.SetParent(this.transform);
          //  if (PhotonNetwork.isMasterClient) {
            
               spawnedShootable.GetComponent<ShootableObject>().canAttack = false;
           
          //  }
            spawnedShootable.GetComponent<ShootableObject>().Fire();
        }
    }

    //This method returns an object in shootableObjects, with weights to modify frequency of certain objects
    private GameObject GetRandomShootableObject() {
      float value = Random.Range(0.0f, 100.0f); //Depending on this value, we will choose an object to shoot
      if (0 <= value && value <= 27.5f) { //27.5% chance for 1pt
        return shootableObjects[0];
      } else if (value <= 47.5f) { //20% chance for 5pt
        return shootableObjects[1];
      } else if (value <= 65f) { //17.5% chance for 7pt
        return shootableObjects[2];
      } else if (value <= 80f) { //15% chance for 9pt
        return shootableObjects[3];
      } else if (value <= 90f) { //10% chance for 1pt
        return shootableObjects[4];
      } else if (value <= 92.5f) { //2.5% chance for Bomb
        return shootableObjects[5];
      } else if (value <= 95f) { //5% chance for 2x Points
        return shootableObjects[6];
      } else if (value <= 97.5f) { //2.5% chance for No Ammo
        return shootableObjects[7];
      } else if (value <= 100f) { //2.5% chance for Add Health
        return shootableObjects[8];
      }
      return null; //Not reachable, but need for error
    }

}

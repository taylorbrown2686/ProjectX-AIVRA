using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    private GameObject spawnedShootable;
    public GameObject SpawnedShootable {get => spawnedShootable;}
    [SerializeField] private GameObject[] shootableObjects;

    public void PrepareFire() {
      spawnedShootable = Instantiate(shootableObjects[Random.Range(0, shootableObjects.Length)], this.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity, this.transform);
      spawnedShootable.GetComponent<ShootableObject>().Fire();
    }

}

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class MoveUp : MonoBehaviourPunCallbacks
{

    public GameObject monster;
    float t;
    Vector3 startPosition;
    Vector3 target;
    float timeToReachTarget;
    public GameObject cube;
    public GameObject plane;
    GameObject gameZone;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        cube.transform.parent = null;
        plane.transform.parent = null;
        gameZone =  GameObject.FindGameObjectWithTag("GameZone");
        if(gameZone != null) {
         //   plane.transform.parent = cube.transform.parent = transform.parent = 
                monster.transform.parent = gameZone.transform;
        }

        SetDestination(new Vector3(monster.transform.position.x, monster.transform.position.y, monster.transform.position.z), 10f);
        startPosition = monster.transform.position - new Vector3(0, 2f, 0);
        monster.transform.position = startPosition;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (monster.GetComponent<PhotonView>().IsMine == true)
        {
            monster.transform.LookAt(new Vector3(Camera.main.transform.position.x, monster.transform.position.y, Camera.main.transform.position.z));
            t += Time.deltaTime / timeToReachTarget;
            monster.transform.position = Vector3.Lerp(startPosition, target, t);
        
            if (monster.transform.position == target) {
                photonView.RPC("DestroyObject", RpcTarget.All);
            }
        }

        if (gameZone != null)
        {
            plane.transform.parent = cube.transform.parent = transform.parent = gameZone.transform;
        }

    }

    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        timeToReachTarget = time;
        target = destination;
    }


    [PunRPC]
    void DestroyObject()
    {
        StartCoroutine(ExecuteAfterTime(3));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SoulGameManager.Instance.ShowMonsterAttackButton();
        Destroy(cube.gameObject);
        Destroy(plane.gameObject);
        Destroy(this.gameObject);
        monster.GetComponent<SoulMonster>().StartToMove();
    }

}

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeDice : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject die, diceCup, diceSpawnPoint;
    PhotonView photonview;
    private bool isShaking = false;
    private bool readyToCollect = false;
    //private List<GameObject> activeDice = new List<GameObject>();
    private GameObject[] activeDice;
 //   private Vector3 originalPosition, originalRotation;
    private Text resultText;
    public bool playersTurn = false;
    DiceCommunication DiceNetwork;
    float scale = 0.08f;
    GameObject gameZone;
    bool isPlaced = false;
    Vector3 pointalongplane;
    GameObject plane;
    float x,y,z;
    GameObject fakeCup;
    GameObject placeCub;
    Toggle AutoPlacement;
    DiceSelectionManager dsm;
    public int turnCounter = 0;
    bool isLastTurn = false;
    public int maxTurns;
    public GameObject avatar;
    //   public List<GameObject> ActiveDice {get => activeDice;}

    void Start() {
        isLastTurn = true;
        maxTurns = 3;
        gameZone = GameObject.FindGameObjectWithTag("GameZone");
        
        photonview = GetComponent<PhotonView>();

        activeDice = new GameObject[5];

        if (gameZone.transform.localScale.x < gameZone.transform.localScale.z)
            scale = Mathf.Sqrt(gameZone.transform.localScale.x * gameZone.transform.localScale.x);
        else
            scale = Mathf.Sqrt(gameZone.transform.localScale.z * gameZone.transform.localScale.z);

        AutoPlacement = DiceGameManager.Instance.AutoPlacement;

        AutoPlacement.gameObject.SetActive(true);

        die.transform.localScale = this.gameObject.transform.localScale = scale * new Vector3(2,2,2) / 2;

        die.GetComponent<Rigidbody>().mass = scale * 1000;

    //    die.transform.localScale = scale * new Vector3(2, 2, 2) / 2;

        plane = gameZone.transform.GetChild(0).gameObject;
        transform.SetParent(plane.transform);
        y = 0;
        dsm = DiceGameManager.Instance.dsm;
        if (photonview.IsMine == true) {

           // avatar = PhotonNetwork.Instantiate(avatar.name, Vector3.zero, Quaternion.identity);

           // avatar.transform.SetParent(plane.transform);

            fakeCup = GameObject.FindGameObjectWithTag("fakeCup");
            placeCub = Resources.FindObjectsOfTypeAll<Unique>()[0].gameObject;
            placeCub.SetActive(true);
            placeCub.GetComponent<Button>().onClick.AddListener(PlaceCub);
            
            resultText = GameObject.FindGameObjectWithTag("ResultText").GetComponent<Text>();
            
            DiceNetwork = GameObject.FindGameObjectWithTag("communication").GetComponent<DiceCommunication>();
            DiceNetwork.shakeDice = this;
            
            
        }
    }

    private void Update()
    {
       
        if (isPlaced == true) {



        }
        else if (photonview.IsMine == true) {
            if (AutoPlacement.isOn == true)
            {
                fakeCup.transform.position = Vector3.MoveTowards(fakeCup.transform.position, Camera.main.transform.position, 1 * Time.deltaTime);
                x = Mathf.Clamp(fakeCup.transform.localPosition.x, -4.3f, 4.3f);
                z = Mathf.Clamp(fakeCup.transform.localPosition.z, -4.3f, 4.3f);
                transform.localPosition = new Vector3(x, y, z);
            }else
                if (Input.GetKeyDown("space") || Input.GetButton("Fire1"))
                {
                    FollowCursor();
                    x = Mathf.Clamp(fakeCup.transform.localPosition.x, -4.3f, 4.3f);
                    z = Mathf.Clamp(fakeCup.transform.localPosition.z, -4.3f, 4.3f);
                    transform.localPosition = new Vector3(x, y, z);


                }
            
            
        }

    }

    public void PlaceCub()
    {
        
        placeCub.SetActive(false);
        AutoPlacement.gameObject.SetActive(false);
        isPlaced = true;
        Button shakeButton = GameObject.FindGameObjectWithTag("ShakeDiceButton").GetComponent<Button>();
        shakeButton.onClick.AddListener(OnclickShake);
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true) {
            Button start = DiceGameManager.Instance.StartButton;
            start.onClick.AddListener(StartGame);
            start.gameObject.SetActive(true);
            
        }
    }

    public void UpCups()
    {
        y = gameZone.transform.GetChild(2).transform.localPosition.y;
        transform.localPosition = new Vector3(x, y, z);
    }

    public void StartGame()
    {
        Destroy(DiceGameManager.Instance.StartButton.gameObject);
        GameObject.FindGameObjectWithTag("communication").GetComponent<DiceCommunication>().StartGame();
    }

    void FollowCursor()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            pointalongplane = ray.origin + (ray.direction * distance);
        }

        fakeCup.transform.position = pointalongplane;
    }

    public void OnclickShake() {
      if (playersTurn == false)
        return;

      if (!isShaking) {
        isShaking = true;
        StartCoroutine(Shake());
        
        } else {
        if (readyToCollect) {
            if (isLastTurn == true)
                DiceGameManager.Instance.endTurn.gameObject.SetActive(false);
            StartCoroutine(Pickup());
                //if(gametype == 1) !!!!!!!!!!!!!!!!!!!!!!!!!! add this
                

                if (isLastTurn == false) {
                    DiceNetwork.TurnPlayed();
                    dsm.Refresh();
                    dsm.HideDices();
                }

                else
                if (turnCounter == maxTurns) {
                    dsm.Refresh();
                    DiceNetwork.TurnPlayed();
                    turnCounter = 0;
                    dsm.HideDices();
                }
            }
      }
    }

    private IEnumerator Shake() {
        
      for (int i = 0; i < 5; i++) {
            if (dsm.isSelected[i] == true)
                continue;
        GameObject newDie = PhotonNetwork.Instantiate(die.name, diceSpawnPoint.transform.position, RandomRotation());

        activeDice[i] = newDie;
        newDie.name = "Dice " + i;
        yield return new WaitForSeconds(0.25f);
      }
      yield return new WaitForSeconds(0.5f);
      for (int i = 0; i < 90; i++) {
        diceCup.transform.Rotate(new Vector3(0, 0, 2));
        yield return new WaitForEndOfFrame();
      }
      int counter = 0;
      foreach (GameObject die in activeDice) {
        while (die.GetComponent<Die>().GetComponent<Rigidbody>().velocity != Vector3.zero && die.GetComponent<Die>().GetComponent<Rigidbody>().angularVelocity != Vector3.zero) {
          yield return new WaitForSeconds(0.1f);
        }
        die.GetComponent<Die>().ReadDie();
        dsm.values[counter] = die.GetComponent<Die>().Value-1;
        counter++;
        dsm.ValuesChanged();
      }
        dsm.ShowDices();
        RollResult result = new RollResult();
        string resultstr = result.GetResult(new List<GameObject>(activeDice));
        resultText.text = "You got " + resultstr; // result
        if (isLastTurn == true)
            DiceGameManager.Instance.endTurn.gameObject.SetActive(true);
        DiceNetwork.SendMyScore(resultstr, result.score);
        readyToCollect = true;
    }

    public void RemotePickup()
    {
        playersTurn = false;
        turnCounter = maxTurns;
        StartCoroutine(Pickup());
        
    }

    private IEnumerator Pickup() {
       // dsm.Refresh();
        resultText.text = "";
        turnCounter++;
        for (int i = 0; i < 5; i++) {
            
            if (dsm.isSelected[i] == true && turnCounter < maxTurns)
            {
                activeDice[i].GetComponent<Rigidbody>().freezeRotation = true;
                dsm.isFrozen[i] = true;
                continue;
            }
                
         
            for (int k = 0; k < 25; k++) {
                activeDice[i].transform.position = Vector3.Lerp(activeDice[i].transform.position, diceSpawnPoint.transform.position, 0.1f);
          yield return new WaitForEndOfFrame();
        }
            PhotonNetwork.Destroy(activeDice[i]);
      }
        for (int i = 0; i < 90; i++)
        {
            //  diceCup.transform.Translate(Vector3.down * 0.25f);
            diceCup.transform.Rotate(new Vector3(0, 0, -2));
            yield return new WaitForEndOfFrame();
        }
      //  this.transform.rotation = Quaternion.Euler(originalRotation);
      //activeDice.Clear();
      isShaking = false;
      readyToCollect = false;
        
    }

    private Quaternion RandomRotation() {
      return Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}

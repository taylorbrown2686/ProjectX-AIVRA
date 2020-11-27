using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    private int score = 0;
    private int previousScore = 0;

    public Canvas StateSelectionCanvas;


    State[] state;
    State selectedState;

    DeerStats[] smallDeer;
    DeerStats[] mediumDeer;
    DeerStats[] largeDeer;
    public GameObject plane;
    public GameObject deer;
    public GameObject doe;
    public GameObject pig;
    public GameObject duck;
    public GameObject squirel;
    public DeerShoot[] deerShoot;
    int deerShootCounter = -1;
    //   public GameObject dog;
    public GameObject food;
    public GameObject exitPoint;
    private GameObject[] exit;
    public GameObject tree;
    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;

    public GameObject grass1;
    public GameObject grass2;

    public GameObject pound;
    public GameObject farmArea;
    public Shader depthMask;
    GameObject[] animal;
    public Gun gun;
    List<GameObject> treeList;
    float scale = 1;
    List<GameObject> foodList;
    int roundIndex = 0;
    int huntCounter = 0;
    int deerCounter = 0;
    int escapedDeerCounter = 0;
    bool running;
    int totalNumOfDeers = 3;
    public Text levelText;
    public Text RoundEndText;
    public Text completionText;
    public Text totalScoreText;
    int stateCounter = 0;

    //public Text countDown;
    bool bonus = true;
    Round[] round;
    bool doeKilled = false;
    private AudioSource audioSource;
    [SerializeField] private Sprite[] countdownSprites;
    [SerializeField] private Sprite gameOverSprite;
    [SerializeField] private Image imageToDisplay;

    int[] deerScores;
    int[] deerPoints;
    int[] deerWeights;

    public ButtonManager buttonManager;
    public RoundEndManager rem;


    private static GameManager _instance;
    Text text;
    GameObject ui;
    private GameManager()
    {


    }

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            Renderer planeRenderer = plane.GetComponent<Renderer>();
            planeRenderer.material.shader = depthMask;
            gun = GameObject.Find("Gun(Clone)").GetComponentInChildren<Gun>();
            scale = Mathf.Sqrt(plane.transform.parent.transform.localScale.x* plane.transform.parent.transform.localScale.z)/2;
            gun.SetScale(scale);
            _instance = this;
            plane.GetComponent<NavMeshSurface>().BuildNavMesh();

            audioSource = this.gameObject.GetComponent<AudioSource>();
            imageToDisplay = GameObject.Find("CenterImage").GetComponent<Image>();
            imageToDisplay.gameObject.SetActive(false);

            smallDeer = new DeerStats[5];
            mediumDeer = new DeerStats[5];
            largeDeer = new DeerStats[5];

            smallDeer[0] = new DeerStats(160, 6, 380);
            smallDeer[1] = new DeerStats(165, 6, 400);
            smallDeer[2] = new DeerStats(170, 6, 420);
            smallDeer[3] = new DeerStats(172, 6, 425);
            smallDeer[4] = new DeerStats(175, 6, 430);

            mediumDeer[0] = new DeerStats(179, 10, 458);
            mediumDeer[1] = new DeerStats(181, 8, 462);
            mediumDeer[2] = new DeerStats(182, 6, 468);
            mediumDeer[3] = new DeerStats(185, 8, 480);
            mediumDeer[4] = new DeerStats(189, 8, 499);

            largeDeer[0] = new DeerStats(220, 10, 620);
            largeDeer[1] = new DeerStats(236, 12, 682);
            largeDeer[2] = new DeerStats(292, 14, 807);
            largeDeer[3] = new DeerStats(325, 12, 932);
            largeDeer[4] = new DeerStats(366, 14, 1095);

            state = new State[5];

            state[0] = new State("wisconsin", tree1, null);
            state[1] = new State("michigan", tree1, null);
            state[2] = new State("minnesota", tree3, null);
            state[3] = new State("iowa", tree3, null);
            state[4] = new State("illinois", tree2, null);

            //    buttonManager.StateCompleted("michigan",1556);
            
            StateSelectionCanvas.gameObject.SetActive(true);

        }
    }



    public void StartRound()
    {
        foreach (DeerShoot ds in deerShoot){
            ds.DeActivateAll();
        }
        running = false;
        CreateExitPoints();
        GenerateTrees();
        GenerateGrass();
        DeactivateTreeRenderers();
        
        ActivateTreeRenderers();
  //      Debug.Log(round[roundIndex].total);
        animal = new GameObject[round[roundIndex].total];
        for (int i = 0; i < round[roundIndex].numberOfDeers; i++) {

            animal[i] = InstantiateDeer();
            animal[i].GetComponent<Sheep>().SetScore(deerScores[i]);
            animal[i].GetComponent<Sheep>().SetAntlerPoint(deerPoints[i]);
            animal[i].GetComponent<Sheep>().SetWeight(deerWeights[i]);
            
        }
        foreach (DeerShoot ds in deerShoot)
            ds.gameObject.SetActive(false);
        for (int i = 0; i < round[roundIndex].numberOfDoes; i++)
            animal[round[roundIndex].numberOfDeers + i] = InstantiateDoe();
        for (int i = 0; i < round[roundIndex].numberOfPigs; i++)
            animal[round[roundIndex].numberOfDoes + round[roundIndex].numberOfDeers + i] = InstantiatePig();
        //        InstantiateDog();
        for (int i = 0; i < round[roundIndex].numberOfSquirrels; i++)
            InstantiateSquirrel();
        for (int i = 0; i < round[roundIndex].numberOfDucks; i++)
            InstantiateDuck();
    }

    public void MakeAllDeersRun()
    {
        if (running == true)
            return;
        for (int i = 0; i < animal.Length; i++) {
            if (animal[i] == null)
                return;
            animal[i].GetComponent<Sheep>().RunAway();
        }
        running = true;
    }

    void EndRound()
    {
        audioSource.Play();
        foreach (Transform child in plane.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void CreateExitPoints()
    {
        GameObject childObject = Instantiate(exitPoint, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localPosition = new Vector3(4.5f, childObject.transform.localPosition.y, Random.Range(-2f, 2f));
        exit[0] = childObject;

        childObject = Instantiate(exitPoint, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localPosition = new Vector3(-4.5f, childObject.transform.localPosition.y, Random.Range(-2f, 2f));
        exit[1] = childObject;

        childObject = Instantiate(exitPoint, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localPosition = new Vector3(Random.Range(-2f, 2f), childObject.transform.localPosition.y, 4.5f);
        exit[2] = childObject;

        childObject = Instantiate(exitPoint, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localPosition = new Vector3(Random.Range(-2f, 2f), childObject.transform.localPosition.y, -4.5f);
        exit[3] = childObject;
    }

    public void DeerControl(bool endRound)
    {

        huntCounter++;
        
        if (endRound== true || huntCounter >= round[roundIndex].total) {
            Debug.Log(roundIndex + " " + huntCounter);
            gun.CanShoot(false);
            EndRound();

            

            treeList = new List<GameObject>();
            foodList = new List<GameObject>();
            ui = GameObject.FindGameObjectWithTag("text");
            exit = new GameObject[4];
            roundIndex++;
            if(roundIndex == 3 && (bonus == false)) { 
                roundIndex++;
                Debug.Log(bonus);
            }
            else if(roundIndex == 3)
                StartCoroutine(BonusRoundTimer());

            levelText.text = "Round " + (roundIndex+1) + " Loading";
            if (doeKilled == true) {
                levelText.text += "\nYou killed a doe!";
                bonus = false;
            }
            else
            {
                if(deerCounter == 0)
                    levelText.text += "\nThey all got away!";
                else
                    levelText.text += "\nYou got " + deerCounter +" deers";
            }
            
            if (roundIndex == 4)
            {
                levelText.text = "Total score: " + score;
                imageToDisplay.gameObject.SetActive(true);
                imageToDisplay.sprite = gameOverSprite;
                levelText.gameObject.SetActive(true);
                selectedState.score = score - previousScore;
                buttonManager.StateCompleted(selectedState.name, selectedState.score);


                Debug.Log(roundIndex + "!!!!!!!!!!!!!!!!!");
                roundIndex = 0;
                huntCounter = 0;
                deerCounter = 0;
                escapedDeerCounter = 0;
                bonus = true;
                Debug.Log("state :" + stateCounter);
                if (stateCounter == 5) { 
                    completionText.text = "MIDWEST COMPLETED";

                    totalScoreText.text = "Total Score: " + score;
                }else
                    totalScoreText.text = "Choose your next spot\nTotal Score: " + score;

                StateSelectionCanvas.gameObject.SetActive(true);
            }
            else {

                Debug.Log(roundIndex);
                doeKilled = false;
                levelText.gameObject.SetActive(true);
                RoundEndText.text = levelText.text;

                rem.gameObject.SetActive(true);
                rem.Transfer();

                StartCoroutine(RoundBreak());
                
            }

        }

    }

    IEnumerator BonusRoundTimer()
    {


        yield return new WaitForSeconds(25);
        DeerControl(true);

    }


    IEnumerator RoundBreak()
    {

        
        yield return new WaitForSeconds(5);
        rem.Restart();
        rem.gameObject.SetActive(false);
        
        StartCoroutine(StartNewRound());
        
    }

    public void AddScore(int value)
    {
        score += value;
        text.text = "" + score;
    }
    void InstantiateDuck()
    {
        GameObject childObject = Instantiate(duck, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localScale *= scale;
    }

    void InstantiateSquirrel()
    {
        GameObject childObject = Instantiate(squirel, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.GetComponent<Squirrel>().SetScale(scale);
        childObject.transform.localScale *= scale;
        childObject.transform.localPosition = new Vector3(Random.Range(-3f, 3f), childObject.transform.localPosition.y, Random.Range(-3f, 3f));
        if (roundIndex == 3)
            childObject.transform.localScale *= 2;
        childObject.GetComponent<Squirrel>().SetAgent();
    }


    GameObject InstantiateDeer()
    {
        GameObject childObject = Instantiate(deer, plane.transform.position , Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localPosition = new Vector3(Random.Range(-3f, 3f), childObject.transform.localPosition.y, Random.Range(-3f, 3f));
        childObject.GetComponent<Sheep>().SetExit(exit);
        childObject.GetComponent<Sheep>().SetFood(foodList.ToArray());
        childObject.transform.localScale *= scale;
        childObject.GetComponent<Sheep>().SetScale( scale);
        childObject.GetComponent<Sheep>().SetAgent();
        return childObject;

    }

    GameObject InstantiatePig()
    {
        GameObject childObject = Instantiate(pig, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localPosition = new Vector3(Random.Range(-3f, 3f), childObject.transform.localPosition.y, Random.Range(-3f, 3f));
        childObject.GetComponent<Sheep>().SetExit(exit);
        childObject.GetComponent<Sheep>().SetAgent();
        childObject.GetComponent<Sheep>().SetFood(foodList.ToArray());
        childObject.transform.localScale *= scale;
        childObject.GetComponent<Sheep>().SetScale(scale);
        return childObject;

    }

    GameObject InstantiateDoe()
    {
        GameObject childObject = Instantiate(doe, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localPosition = new Vector3(Random.Range(-3f, 3f), childObject.transform.localPosition.y, Random.Range(-3f, 3f));
        childObject.GetComponent<Sheep>().SetExit(exit);
        childObject.GetComponent<Sheep>().SetAgent();
        childObject.GetComponent<Sheep>().SetFood(foodList.ToArray());
        childObject.transform.localScale *= scale;
        childObject.GetComponent<Sheep>().SetScale(scale);
        return childObject;

    }

   /* void InstantiateDog()
    {
        GameObject childObject = Instantiate(dog, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localPosition = new Vector3(Random.Range(-3f, 3f), childObject.transform.localPosition.y, Random.Range(-3f, 3f));
        childObject.GetComponent<Dog>().SetExit(exit);
        childObject.transform.localScale *= scale;
    }*/

    void GenerateTrees()
    {
        GameObject childObject = null;
        int foodLocation;
        if (selectedState.name == "minnesota")
        {
            childObject = Instantiate(farmArea, plane.transform.position, Quaternion.identity) as GameObject;
            childObject.transform.localScale *= scale*2;
            childObject.transform.parent = plane.transform;
        }
        for (int i = 0; i < 5; i++) {
            foodLocation = Random.Range(0, 5);
            for (int j = 0; j < 5; j++) {
                if (j == foodLocation)
                {
                    childObject = Instantiate(food, plane.transform.position, Quaternion.identity) as GameObject;
                    foodList.Add(childObject);
                }
                else
                {
                    if (selectedState.name == "michigan") {
                        int random = Random.Range(0, 6);
                        if (random < 2)
                            continue;
                        else if(random == 2)
                            childObject = Instantiate(pound, plane.transform.position, Quaternion.identity) as GameObject;
                        else
                            childObject = Instantiate(tree, plane.transform.position, Quaternion.identity) as GameObject;
                    }else
                    if (selectedState.name == "minnesota" || selectedState.name == "iowa")
                    {
                        if (i == 0 || i == 4 || j == 0 || j == 4)
                        {
                            int random = Random.Range(0, 3);
                            if (random < 2)
                                continue;
                            else if (random == 2)
                                childObject = Instantiate(tree3, plane.transform.position, Quaternion.identity) as GameObject;
                        }
                        else
                            continue;
                    }
                    else
                        childObject = Instantiate(tree, plane.transform.position, Quaternion.identity) as GameObject;


                }
                childObject.transform.localScale *= scale;
                childObject.transform.parent = plane.transform;
                childObject.transform.localPosition = new Vector3(-4 + i *2 + Random.Range(-0.3f,0.3f), childObject.transform.localPosition.y, -4 + j * 2 + Random.Range(-0.3f, 0.3f));
                treeList.Add(childObject);
            }
        }
    }

    void GenerateGrass()
    {
        GameObject childObject = null;
        for (float i=-5;i<5;i = i+0.5f)
            for (float j = -5; j < 5; j = j+ 0.5f)
            {
                if (selectedState.name == "iowa")
                {
                    int random = Random.Range(0, 10);
                    if (random < 7)
                        childObject = Instantiate(grass1, plane.transform.position, Quaternion.identity) as GameObject;
                    else
                        childObject = Instantiate(grass2, plane.transform.position, Quaternion.identity) as GameObject;

                }
                else
                    continue;
                childObject.transform.localScale *= scale;
                childObject.transform.parent = plane.transform;
                childObject.transform.localPosition = new Vector3(i+0.3f, childObject.transform.localPosition.y, j + 0.5f);
            }
    }

    void DeactivateTreeRenderers()
    {
        foreach (GameObject tree in treeList)
        {
            tree.GetComponent<MeshRenderer>().enabled = false;
        }

    }
    void ActivateTreeRenderers()
    {
        foreach (GameObject tree in treeList)
        {
            tree.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    IEnumerator StartNewRound()
    {

        imageToDisplay.gameObject.SetActive(true);
        imageToDisplay.sprite = countdownSprites[0];
        yield return new WaitForSeconds(1);
        imageToDisplay.sprite = countdownSprites[1];
        yield return new WaitForSeconds(1);
        imageToDisplay.sprite = countdownSprites[2];
        yield return new WaitForSeconds(1);
        imageToDisplay.gameObject.SetActive(false);
        StartRound();
        gun.CanShoot(true);
        levelText.gameObject.SetActive(false);
        deerCounter=0;
        deerShootCounter = -1;
        huntCounter = 0;
        deerCounter = 0;

    }

    public void KilledDeer() {
        deerCounter++;

        if (deerCounter + escapedDeerCounter == totalNumOfDeers) {
            DeerControl(true);

        }
    }

    public void DeerEscaped()
    {
        escapedDeerCounter++;

        if (deerCounter + escapedDeerCounter == totalNumOfDeers)
        {
            Debug.Log(deerCounter);
            DeerControl(true);
            escapedDeerCounter = 0;

        }

    }

    public void DoeKilled()
    {
        doeKilled = true;
    }

    public DeerShoot GetDeerShoot(int back, int neck, int head, int score, int weight, int antlerPoint)
    {
     
        
        

        deerShootCounter++;

        rem.text[deerShootCounter].text = antlerPoint + " point\n" +
                                          weight +  " lbs\n" +
                                           "shots: "+ (back + neck + head) +"\n" +
                                           "score: " + (score + head*50) ;
        deerShoot[deerShootCounter].gameObject.SetActive(true);
        for (int i = 0; i < back; i++) { 
            deerShoot[deerShootCounter].ActivatePoint(Random.Range(4, 7));  
        }
        for (int i = 0; i < neck; i++)
            deerShoot[deerShootCounter].ActivatePoint(Random.Range(1, 4));

        for (int i = 0; i < head; i++)
            deerShoot[deerShootCounter].ActivatePoint(0);

        return deerShoot[deerShootCounter];
    }

    public void ChooseState(string stateName)
    {
        stateCounter++;
        previousScore = score;


        selectedState = state[0];

        foreach (State st in state)
            if(st.name == stateName)
            {
                selectedState = st;
                break;
            }

        
        tree = selectedState.tree;


        StateSelectionCanvas.gameObject.SetActive(false);
        treeList = new List<GameObject>();
        foodList = new List<GameObject>();
        ui = GameObject.FindGameObjectWithTag("text");
        text = ui.GetComponent<Text>();
        exit = new GameObject[4];
        levelText.text = "Round 1 Loading";
        levelText.gameObject.SetActive(true);

        round = new Round[4];

        switch (stateName)
        {
            
            case "wisconsin":
                round[0] = new Round(3, 3, 0, 3, 0);// number of deers,does,pigs,squirrels,ducks
                round[1] = new Round(3, 4, 0, 3, 0);
                round[2] = new Round(3, 5, 0, 3, 0);
                round[3] = new Round(0, 0, 0, 12, 0);
                break;
            case "michigan":
                round[0] = new Round(3, 3, 0, 3, 0);// number of deers,does,pigs,squirrels,ducks
                round[1] = new Round(3, 4, 0, 3, 0);
                round[2] = new Round(3, 5, 0, 3, 0);
                round[3] = new Round(0, 0, 0, 12, 0);
                break;
            case "minnesota":
                round[0] = new Round(3, 3, 0, 0, 0);// number of deers,does,pigs,squirrels,ducks
                round[1] = new Round(3, 4, 0, 0, 0);
                round[2] = new Round(3, 5, 0, 0, 0);
                round[3] = new Round(0, 0, 0, 0, 12);
                break;
            case "iowa":
                round[0] = new Round(3, 3, 0, 0, 0);// number of deers,does,pigs,squirrels,ducks
                round[1] = new Round(3, 4, 0, 0, 0);
                round[2] = new Round(3, 5, 0, 0, 0);
                round[3] = new Round(0, 0, 10, 0, 0);
                break;
            case "illinois":
                round[0] = new Round(3, 3, 0, 0, 0);// number of deers,does,pigs,squirrels,ducks
                round[1] = new Round(3, 4, 0, 0, 0);
                round[2] = new Round(3, 5, 0, 0, 0);
                round[3] = new Round(0, 0, 10, 0, 0);
                break;
        }

        

        int smallDeerIndex, mediumDeerIndex, largeDeerIndex;

        do
        {
            smallDeerIndex = Random.Range(0, 5);
        }while (smallDeer[smallDeerIndex].available == false);

        do
        {
            mediumDeerIndex = Random.Range(0, 5);
        } while (mediumDeer[mediumDeerIndex].available == false);

        do
        {
            largeDeerIndex = Random.Range(0, 5);
        } while (largeDeer[largeDeerIndex].available == false);


        deerScores = new int[3];
        deerPoints = new int[3];
        deerWeights = new int[3];

        smallDeer[smallDeerIndex].available = false;
        deerScores[0] = smallDeer[smallDeerIndex].point;
        deerPoints[0] = smallDeer[smallDeerIndex].antlerPoint;
        deerWeights[0] = smallDeer[smallDeerIndex].weight;

        Debug.Log(deerWeights[0]);

        mediumDeer[mediumDeerIndex].available = false;
        deerScores[1] = mediumDeer[mediumDeerIndex].point;
        deerPoints[1] = mediumDeer[mediumDeerIndex].antlerPoint;
        deerWeights[1] = mediumDeer[mediumDeerIndex].weight;

        largeDeer[largeDeerIndex].available = false;
        deerScores[2] = largeDeer[largeDeerIndex].point;
        deerPoints[2] = largeDeer[largeDeerIndex].antlerPoint;
        deerWeights[2] = largeDeer[largeDeerIndex].weight;

        StartCoroutine(StartNewRound());
    }


}


struct State
{
    public string name;
    public GameObject tree;
    public GameObject ground;
    public bool cleared;
    public int score;

    public State(string name,GameObject tree, GameObject ground)
    {
        this.name = name;
        this.tree = tree;
        this.ground = ground;
        cleared = false;
        score = 0;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

}

struct DeerStats
{
    public int weight;
    public int antlerPoint;
    public int point;
    public bool available;

    public DeerStats(int weight, int antlerPoint, int point)
    {
        this.weight = weight;
        this.antlerPoint = antlerPoint;
        this.point = point;
        available = true;
    }

}



struct Round
{
    public int numberOfDeers;
    public int numberOfDoes;
    public int numberOfPigs;
    public int numberOfSquirrels;
    public int numberOfDucks;
    public int total;

    public Round(int numberOfDeers, int numberOfDoes, int numberOfPigs, int numberOfSquirrels, int numberOfDucks)
    {
        this.numberOfDeers = numberOfDeers;
        this.numberOfDoes = numberOfDoes;
        this.numberOfPigs = numberOfPigs;
        this.numberOfSquirrels = numberOfSquirrels;
        this.numberOfDucks = numberOfDucks;
        total = numberOfDeers + numberOfDoes + numberOfPigs;
        if (total == 0)
            total = 111;
    }
}

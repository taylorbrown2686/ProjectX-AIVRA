using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private int score = 0;

    public GameObject plane;
    public GameObject deer;
    public GameObject doe;
    public GameObject pig;
    public GameObject duck;
    public GameObject dog;
    public GameObject food;
    public GameObject exitPoint;
    private GameObject[] exit;
    public GameObject tree;
    GameObject[] animal;
    public Gun gun;
    List<GameObject> treeList;
    float scale = 1;
    List<GameObject> foodList;
    int roundIndex = 0;
    int deerCounter = 0;
    bool running;
    public Text levelText;
    public Text countDown;
    bool bonus = true;
    Round[] round;


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
            gun = GameObject.Find("Gun(Clone)").GetComponentInChildren<Gun>();
            scale = Mathf.Sqrt(plane.transform.localScale.x* plane.transform.localScale.z)/2;
            gun.SetScale(scale);
            _instance = this;
            treeList = new List<GameObject>();
            foodList = new List<GameObject>();
            ui = GameObject.FindGameObjectWithTag("text");
            text = ui.GetComponent<Text>();
            exit = new GameObject[4];

            levelText.text = "Round 1 Loading";
            levelText.gameObject.SetActive(true);


            round = new Round[4];// deers,does,pigs

            round[0] = new Round(3, 3, 0);
            round[1] = new Round(3, 4, 0);
            round[2] = new Round(3, 5, 0);
            round[3] = new Round(0, 0, 6);

            StartCoroutine(StartNewRound());



        }
    }



    public void StartRound()
    {
        running = false;
        CreateExitPoints();
        GenerateTrees();
        plane.GetComponent<NavMeshSurface>().BuildNavMesh();
        ActivateTreeRenderers();
        Debug.Log(round[roundIndex].total);
        animal = new GameObject[round[roundIndex].total];
        for (int i = 0; i < round[roundIndex].numberOfDeers; i++)
            animal[i] = InstantiateDeer();
        for (int i = 0; i < round[roundIndex].numberOfDoes; i++)
            animal[round[roundIndex].numberOfDeers + i] = InstantiateDoe();
        for (int i = 0; i < round[roundIndex].numberOfPigs; i++)
            animal[round[roundIndex].numberOfDoes + round[roundIndex].numberOfDeers + i] = InstantiatePig();
        InstantiateDog();
        for (int i = 0; i < 10; i++)
            InstantiateDuck();
    }

    public void MakeAllDeersRun()
    {
        if (running == true)
            return;
        for (int i = 0; i < animal.Length; i++)
            animal[i].GetComponent<Sheep>().RunAway();
        running = true;
    }

    void EndRound()
    {
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

        deerCounter++;
        Debug.Log(deerCounter);
        if (endRound== true || deerCounter >= round[roundIndex].total) {

            EndRound();
            treeList = new List<GameObject>();
            foodList = new List<GameObject>();
            ui = GameObject.FindGameObjectWithTag("text");
            exit = new GameObject[4];
            roundIndex++;
            if(bonus == false && score <1)
                roundIndex++;
            levelText.text = "Round " + (roundIndex+1) + " Loading";
            if (endRound == true) {
                levelText.text += "\nYou killed a doe";
                bonus = false;
            }

            if(roundIndex == 4)
            {
                levelText.text = "Total score: " + score;
                Debug.Log("game over");
                levelText.gameObject.SetActive(true);
            }
            else {

            levelText.gameObject.SetActive(true);
            StartCoroutine(StartNewRound());
            deerCounter = 0;
            }

        }

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


    GameObject InstantiateDeer()
    {
        GameObject childObject = Instantiate(deer, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.transform.localPosition = new Vector3(Random.Range(-3f, 3f), childObject.transform.localPosition.y, Random.Range(-3f, 3f));
        childObject.GetComponent<Sheep>().SetExit(exit);
        childObject.GetComponent<Sheep>().SetAgent();
        childObject.GetComponent<Sheep>().SetFood(foodList.ToArray());
        childObject.transform.localScale *= scale;
        childObject.GetComponent<Sheep>().SetScale( scale);
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

    void InstantiateDog()
    {
        GameObject childObject = Instantiate(dog, plane.transform.position, Quaternion.identity) as GameObject;
        childObject.transform.parent = plane.transform;
        childObject.GetComponent<Dog>().SetExit(exit);
        childObject.transform.localScale *= scale;
    }

    void GenerateTrees()
    {
        GameObject childObject;
        int foodLocation;
        for (int i = 0; i < 5; i++) {
            foodLocation = Random.Range(0, 5);
            for (int j = 0; j < 5; j++) {
                if (j == foodLocation) {
                    childObject = Instantiate(food, plane.transform.position, Quaternion.identity) as GameObject;
                    foodList.Add(childObject);
                }
                else
                    childObject = Instantiate(tree, plane.transform.position, Quaternion.identity) as GameObject;
                childObject.transform.localScale *= scale;
                childObject.transform.parent = plane.transform;
                childObject.transform.localPosition = new Vector3(-4 + i *2 + Random.Range(-0.3f,0.3f), childObject.transform.localPosition.y, -4 + j * 2 + Random.Range(-0.3f, 0.3f));
                treeList.Add(childObject);
            }
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

        countDown.gameObject.SetActive(true);
        countDown.text = "3";
        yield return new WaitForSeconds(1);
        countDown.text = "2";
        yield return new WaitForSeconds(1);
        countDown.text = "1";
        yield return new WaitForSeconds(1);
        countDown.gameObject.SetActive(false);
        StartRound();
        levelText.gameObject.SetActive(false);
        deerCounter=0;

    }



}

struct Round
{
    public int numberOfDeers;
    public int numberOfDoes;
    public int numberOfPigs;
    public int total;

    public Round(int numberOfDeers, int numberOfDoes, int numberOfPigs)
    {
        this.numberOfDeers = numberOfDeers;
        this.numberOfDoes = numberOfDoes;
        this.numberOfPigs = numberOfPigs;
        total = numberOfDeers + numberOfDoes + numberOfPigs;
    }
}

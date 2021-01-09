using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour
{
    [SerializeField] private GameObject[] reels;
    private bool hasSpun = true;
    private const float ODDS_OF_SINGLE = (float)1 / 216;
    private const float ODDS_OF_DOUBLE = (float)1 / 1728;
    private List<int> numbersToAvoid = new List<int>();
    [SerializeField] private float[] oddsTable;//needs size 8!!!
    [SerializeField] private string[] rewardsTable;
    [SerializeField] private GameObject barPivot, bar, coin;
    [SerializeField] private GameObject[] coinSpawnPoints;
    [SerializeField] private AudioClip slotStart, win, bigWin;
    [SerializeField] private ParticleSystem confetti, barParticles;
    [SerializeField] private TextMesh winText, spinText;

    private void Start()
    {
        oddsTable = new float[8];
        rewardsTable = new string[8];
        barParticles.Stop();
        spinText.text = "";
        StartCoroutine(GetOddsAndRewards());
    }

    private void Update()
    {
        /*if (!hasSpun)
        {
            hasSpun = true;
            StartCoroutine(StartReelsWithDelay());
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Bar")
                {
                    if (!hasSpun)
                    {
                        hasSpun = true;
                        StartCoroutine(StartReelsWithDelay());
                    }
                }
            }
        }
        /*if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began && !isSpinning)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                isSpinning = true;
                StartCoroutine(StartReelsWithDelay());
            }
        }*/
    }

    private IEnumerator StartReelsWithDelay()
    {
        barParticles.Stop();
        spinText.text = "";
        foreach (GameObject obj in reels)
        {
            obj.transform.localRotation = Quaternion.identity;
        }
        StartCoroutine(AnimateBar());
        string whatToRoll = GetRandomOutcome();
        StartCoroutine(SpinReels(0, whatToRoll));
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpinReels(1, whatToRoll));
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpinReels(2, whatToRoll));
    }

    private IEnumerator SpinReels(int reel, string whatToRoll)
    {
        for (int i = 0; i < 72; i++)
        {
            reels[reel].transform.Rotate(new Vector3(30, 0, 0), Space.Self);
            yield return new WaitForSeconds(0.01f);
        }
        switch (whatToRoll)
        {
            case "bar":
                for (int i = 0; i < 12; i++)
                {
                    reels[reel].transform.Rotate(new Vector3(30, 0, 0), Space.Self);
                    yield return new WaitForSeconds(0.01f);
                }
                break;
            case "star":
                for (int i = 0; i < 1; i++)
                {
                    reels[reel].transform.Rotate(new Vector3(30, 0, 0), Space.Self);
                    yield return new WaitForSeconds(0.01f);
                }
                break;
            case "seven":
                for (int i = 0; i < 2; i++)
                {
                    reels[reel].transform.Rotate(new Vector3(30, 0, 0), Space.Self);
                    yield return new WaitForSeconds(0.01f);
                }
                break;
            case "orange":
                for (int i = 0; i < 3; i++)
                {
                    reels[reel].transform.Rotate(new Vector3(30, 0, 0), Space.Self);
                    yield return new WaitForSeconds(0.01f);
                }
                break;
            case "grape":
                for (int i = 0; i < 4; i++)
                {
                    reels[reel].transform.Rotate(new Vector3(30, 0, 0), Space.Self);
                    yield return new WaitForSeconds(0.01f);
                }
                break;
            case "bell":
                for (int i = 0; i < 9; i++)
                {
                    reels[reel].transform.Rotate(new Vector3(30, 0, 0), Space.Self);
                    yield return new WaitForSeconds(0.01f);
                }
                break;
            case "cherry":
                for (int i = 0; i < 10; i++)
                {
                    reels[reel].transform.Rotate(new Vector3(30, 0, 0), Space.Self);
                    yield return new WaitForSeconds(0.01f);
                }
                break;
            case "diamond":
                for (int i = 0; i < 11; i++)
                {
                    reels[reel].transform.Rotate(new Vector3(30, 0, 0), Space.Self);
                    yield return new WaitForSeconds(0.01f);
                }
                break;
            case "random":
                int currentRollIndex = UnityEngine.Random.Range(0, 11);
                if (reel == 2)
                {
                    while (numbersToAvoid.Contains(currentRollIndex))
                    {
                        currentRollIndex = UnityEngine.Random.Range(0, 11);
                    }
                }
                for (int i = 0; i < currentRollIndex; i++)
                {
                    reels[reel].transform.Rotate(new Vector3(30, 0, 0), Space.Self);
                    yield return new WaitForSeconds(0.01f);
                }
                if (reel == 1 && GetSymbolFromQuaternion(reels[0].transform.localRotation) == GetSymbolFromQuaternion(reels[1].transform.localRotation))
                {
                    numbersToAvoid.Clear();
                    switch (currentRollIndex)
                    {
                        case 0:
                            numbersToAvoid.Add(0);
                            numbersToAvoid.Add(8);
                            break;
                        case 1:
                            numbersToAvoid.Add(1);
                            numbersToAvoid.Add(5);
                            break;
                        case 2:
                            numbersToAvoid.Add(2);
                            numbersToAvoid.Add(6);
                            break;
                        case 3:
                            numbersToAvoid.Add(3);
                            numbersToAvoid.Add(7);
                            break;
                        case 4:
                            numbersToAvoid.Add(4);
                            break;
                        case 5:
                            numbersToAvoid.Add(1);
                            numbersToAvoid.Add(5);
                            break;
                        case 6:
                            numbersToAvoid.Add(2);
                            numbersToAvoid.Add(6);
                            break;
                        case 7:
                            numbersToAvoid.Add(3);
                            numbersToAvoid.Add(7);
                            break;
                        case 8:
                            numbersToAvoid.Add(0);
                            numbersToAvoid.Add(8);
                            break;
                        case 9:
                            numbersToAvoid.Add(9);
                            break;
                        case 10:
                            numbersToAvoid.Add(10);
                            break;
                        case 11:
                            numbersToAvoid.Add(11);
                            break;
                    }
                }
                break;
        }
        reels[reel].GetComponent<AudioSource>().Play();
        if (reel == 2)
        {
            CheckForWin();
        }
    }

    private void CheckForWin()
    {
        string reelOne, reelTwo, reelThree;
        reelOne = GetSymbolFromQuaternion(reels[0].transform.localRotation);
        reelTwo = GetSymbolFromQuaternion(reels[1].transform.localRotation);
        reelThree = GetSymbolFromQuaternion(reels[2].transform.localRotation);
        if (reelOne == reelTwo && reelTwo == reelThree)
        {
            if (reelOne == "diamond")
            {
                bar.GetComponent<AudioSource>().clip = bigWin;
                bar.GetComponent<AudioSource>().Play();
                StartCoroutine(ShootCoins(50));
                confetti.Play();
            }
            else
            {
                bar.GetComponent<AudioSource>().clip = win;
                bar.GetComponent<AudioSource>().Play();
                StartCoroutine(ShootCoins(25));
                confetti.Play();
            }
            StartCoroutine(UploadWinToDB(reelOne));
            winText.text = "You are a winner!\nClaim your prize in the 'Deals' section!";
            print("WINNER! 3 " + reelOne);
        }
        else
        {
            winText.text = "You were not a winner...\nTry again tomorrow!";
            print("You Lost!");
        }
        StartCoroutine(SaveTimestampToDB());
    }

    private string GetRandomOutcome()
    {
        int seed = Convert.ToInt32(DateTime.Now.Millisecond);
        UnityEngine.Random.InitState(seed);
        float randomReelSeed = UnityEngine.Random.Range(0.00000f, 100.00000f);
        if (randomReelSeed < AddArrayValuesFromIndex(1))
        {
            return "bar";
        }
        else if (randomReelSeed < AddArrayValuesFromIndex(2))
        {
            return "star";
        }
        else if (randomReelSeed < AddArrayValuesFromIndex(3))
        {
            return "seven";
        }
        else if (randomReelSeed < AddArrayValuesFromIndex(4))
        {
            return "orange";
        }
        else if (randomReelSeed < AddArrayValuesFromIndex(5))
        {
            return "grape";
        }
        else if (randomReelSeed < AddArrayValuesFromIndex(6))
        {
            return "bell";
        }
        else if (randomReelSeed < AddArrayValuesFromIndex(7))
        {
            return "cherry";
        }
        else if (randomReelSeed < AddArrayValuesFromIndex(8))
        {
            return "diamond";
        }
        else
        {
            return "random";
        }
    }

    private string GetSymbolFromQuaternion(Quaternion q)
    {
        if (QuaternionComparator(q, new Quaternion(0, 0, 0, -1f)) || QuaternionComparator(q, new Quaternion(0.9f, 0, 0, -0.5f)))
        {
            return "bar";
        }
        else if (QuaternionComparator(q, new Quaternion(0.3f, 0, 0, 1f)) || QuaternionComparator(q, new Quaternion(1f, 0, 0, 0.3f)))
        {
            return "star";
        }
        else if (QuaternionComparator(q, new Quaternion(0.5f, 0, 0, 0.9f)) || QuaternionComparator(q, new Quaternion(1f, 0, 0, 0)))
        {
            return "seven";
        }
        else if (QuaternionComparator(q, new Quaternion(0.7f, 0, 0, 0.7f)) || QuaternionComparator(q, new Quaternion(1f, 0, 0, -0.3f)))
        {
            return "orange";
        }
        else if (QuaternionComparator(q, new Quaternion(0.9f, 0, 0, 0.5f)))
        {
            return "grape";
        }
        else if (QuaternionComparator(q, new Quaternion(0.7f, 0, 0, -0.7f)))
        {
            return "bell";
        }
        else if (QuaternionComparator(q, new Quaternion(0.5f, 0, 0, -0.9f)))
        {
            return "cherry";
        }
        else if (QuaternionComparator(q, new Quaternion(0.3f, 0, 0, -1f)))
        {
            return "diamond";
        }
        return "";
    }

    private string GetRewardFromSymbol(string symbol)
    {
        switch (symbol)
        {
            case "bar":
                return rewardsTable[0];
            case "star":
                return rewardsTable[1];
            case "seven":
                return rewardsTable[2];
            case "orange":
                return rewardsTable[3];
            case "grape":
                return rewardsTable[4];
            case "bell":
                return rewardsTable[5];
            case "cherry":
                return rewardsTable[6];
            case "diamond":
                return rewardsTable[7];
        }
        return ""; //needed for error but unreachable
    }

    private bool QuaternionComparator(Quaternion q1, Quaternion q2)
    {
        if (Mathf.RoundToInt(q1.x * 10) == Mathf.RoundToInt(q2.x * 10) && Mathf.RoundToInt(q1.y * 10) == Mathf.RoundToInt(q2.y * 10) 
            && Mathf.RoundToInt(q1.z * 10) == Mathf.RoundToInt(q2.z * 10) && Mathf.RoundToInt(q1.w * 10) == Mathf.RoundToInt(q2.w * 10))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private float AddArrayValuesFromIndex(int index)
    {
        float returnVal = 0;
        for (int i = 0; i < index; i++)
        {
            returnVal += oddsTable[i];
        }
        return returnVal;
    }

    private IEnumerator GetOddsAndRewards()
    {
        WWWForm form = new WWWForm();
        form.AddField("businessname", CrossSceneVariables.Instance.inBusinessName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/pullOddsAndRewardsByBusiness.php", form);
        yield return www;
        string[] splitString = www.text.Split('#');
        for (int i = 0; i < splitString.Length; i += 2)
        {
            oddsTable[i / 2] = (1 / System.Single.Parse(splitString[i])) * 100;
            rewardsTable[i / 2] = splitString[i + 1];
        }
        WWWForm form2 = new WWWForm();
        form.AddField("businessname", CrossSceneVariables.Instance.inBusinessName);
        WWW www2 = new WWW("http://65.52.195.169/AIVRA-PHP/pullWonSlotRewardsByBusiness.php", form);
        yield return www2;
        string[] splitString2 = www2.text.Split('#');
        for (int i = 0; i < splitString2.Length; i += 2)
        {
            print(splitString2[i + 1] + " " + splitString2[i]);
            if (Convert.ToInt32(splitString2[i+1]) - Convert.ToInt32(splitString2[i]) <= 0)
            {
                oddsTable[i / 2] = 0;
            }
        }
        hasSpun = false;
        barParticles.Play();
        spinText.text = "Touch me to spin!";
    }

    private IEnumerator AnimateBar()
    {
        bar.GetComponent<AudioSource>().clip = slotStart;
        bar.GetComponent<AudioSource>().Play();
        for (int i = 0; i < 30; i++)
        {
            bar.transform.RotateAround(barPivot.transform.position, transform.right, 1);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 30; i++)
        {
            bar.transform.RotateAround(barPivot.transform.position, -transform.right, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator ShootCoins(int howMany)
    {
        List<GameObject> objList = new List<GameObject>();
        for (int i = 0; i < howMany; i++)
        {
            var newObj = Instantiate(coin,
                coinSpawnPoints[UnityEngine.Random.Range(0, coinSpawnPoints.Length - 1)].transform.position, Quaternion.Euler(0,0,90));
            objList.Add(newObj);
            newObj.GetComponent<Rigidbody>().AddForce(transform.forward * UnityEngine.Random.Range(20, 40));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(5f);
        foreach (GameObject obj in objList)
        {
            Destroy(obj);
        }
    }

    private IEnumerator UploadWinToDB(string winningSymbol)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("symbol", winningSymbol);
        form.AddField("businessName", CrossSceneVariables.Instance.inBusinessName);
        form.AddField("amount", GetRewardFromSymbol(winningSymbol));
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/uploadRewardFromSlotWin.php", form);
        yield return www;
        print(www.text);
    }
    private IEnumerator SaveTimestampToDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("businessName", CrossSceneVariables.Instance.inBusinessName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/uploadTimestampOfSpin.php", form);
        yield return www;
        yield return new WaitForSeconds(5f);
        winText.text = "Sending you back to AIVRA...\nPlease wait...";
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("AIVRAHome");
    }
}

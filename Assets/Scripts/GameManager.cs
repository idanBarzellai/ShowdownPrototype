using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] artifactList;
    public Transform[] artifactSpawnPlaces;
    public Transform[] artifactSlotsP1;
    public Transform[] artifactSlotsP2;
    public Transform[] artifactSlotsP3;
    public Transform[] artifactSlotsP4;

    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;

    float lastArtifactDeployTime;
    bool firstArtifactDeploied = false;
    public int artifactDeployPause = 10;
    public int baseHealth = 5;
    GameObject spawnedArtifact;

    public bool gamePlaying = false;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlaying && !firstArtifactDeploied)
        {
            Invoke("DeployArtifact", 1f);
            firstArtifactDeploied = true;
        }
            //Artifact deploy, and destroy unused old
        if (firstArtifactDeploied && gamePlaying && Time.time - lastArtifactDeployTime >= artifactDeployPause) 
        {
            if (spawnedArtifact != null && spawnedArtifact.tag != "ArtifactOnPlayer")
                DestroyArtifact();
            DeployArtifact();
        }
    }

    public void DeployArtifact()
    {
        //chooses artifact from artifact list randomly and deploies it randomly in the given spawnList
        int artifactIndex = Random.Range(0, artifactList.Length);
        int artifactSpawnIndex = Random.Range(0, artifactSpawnPlaces.Length);
        spawnedArtifact = Instantiate(artifactList[artifactIndex], artifactSpawnPlaces[artifactSpawnIndex]);
        lastArtifactDeployTime = Time.time;
        
    }

    private void DestroyArtifact()
    {
        Destroy(spawnedArtifact);
        //effect
    }

    public bool isInventoryFull(Transform[] artifactInventory)
    {
        if (artifactInventory[artifactInventory.Length - 1].tag == "FullSlot")
            return true;
        else
            return false;
    }
    
    public bool isInventoryEmpty(Transform[] artifactInventory)
    {
        if (artifactInventory[0].tag == "EmptySlot")
            return true;
        else
            return false;
    }

    public void GetNextArtifactAndRefresh(Transform[] artifactInventory, Player player)
    {
        GameObject nextArtifact = artifactInventory[0].gameObject;
        GameObject currArtifact;
        for (int i = 1; i < artifactInventory.Length; i++)
        {
            if (artifactInventory[i].tag == "EmptySlot")
                break;
            else
            {
                currArtifact = artifactInventory[i].transform.gameObject;
                currArtifact.GetComponent<Artifact>().Attach(artifactInventory[i-1].transform, player, false);
            }
        }
        nextArtifact.GetComponent<Artifact>().Attach(player.GetComponent<Player>().artifactSlot, player, true);
    }
    public void SaveState()
    {
        string s = "";
        /*
        s += player1.baseHealth.ToString() + "|" + player2.baseHealth.ToString() +
            "|" + player3.baseHealth.ToString() + "|" + player4.baseHealth.ToString() + "|";
        s += player1.health.ToString() + "|" + player2.health.ToString() +
            "|" + player3.health.ToString() + "|" + player4.health.ToString();
        */
        s += baseHealth.ToString() + "|" + artifactDeployPause.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        baseHealth = int.Parse(data[0]);
        artifactDeployPause = int.Parse(data[1]);

        if (gamePlaying)
        {
            Transform artifactSpanPlacesParent = GameObject.FindGameObjectWithTag("ArtifactSpawnPlace").transform;
            artifactSpawnPlaces = new Transform[artifactSpanPlacesParent.childCount];
            for (int i = 0; i < artifactSpanPlacesParent.childCount; i++)
            {
                artifactSpawnPlaces[i] = artifactSpanPlacesParent.transform.GetChild(i);
            }
            Transform artifactSlotParent1 = GameObject.FindGameObjectWithTag("ArtifactSlotP1").transform;
            artifactSlotsP1 = new Transform[artifactSlotParent1.transform.childCount];
            for (int i = 0; i < artifactSlotParent1.transform.childCount; i++)
            {
                artifactSlotsP1[i] = artifactSlotParent1.transform.GetChild(i);
            }
            Transform artifactSlotParent2 = GameObject.FindGameObjectWithTag("ArtifactSlotP2").transform;
            artifactSlotsP2 = new Transform[artifactSlotParent2.transform.childCount];
            for (int i = 0; i < artifactSlotParent2.transform.childCount; i++)
            {
                artifactSlotsP2[i] = artifactSlotParent2.transform.GetChild(i);
            }
        }
        Debug.Log("LoadState");
    }
    
    
}
public enum GameState
{
    MainManu, Game
}

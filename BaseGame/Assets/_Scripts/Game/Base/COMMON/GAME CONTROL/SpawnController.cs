using System.Collections;
using UnityEngine;

public class SpawnController : ScriptableObject
{
    private ArrayList playerTransforms;
    private ArrayList playerGameObjects;


    private Transform tempTrans;
    private GameObject tempGO;

    private GameObject[] playerPrefabList;
    private Vector3[] startPositions;
    private Quaternion[] startRotations;


    // singleton structure based on AngryAnt's fantastic wiki entry
    // over at http://wiki.unity3d.com/index.php/Singleton



    private static SpawnController instance;

    public SpawnController()
    {
        // this function will be called whenever an instance of the
        // SpawnController class is made
        // first, we check that an instance does not already exist
        // (this is a singleton, after all)
        if (instance != null)
        {
            // drop out if instance exists, to avoid generating
            // duplicates       重复
            Debug.LogWarning("Tried to generate more than one instance of singleton SpawnController");
            return;
        }
        // as no instance already exists, we can safely set instance to this one
        instance = this;
    }

    public static SpawnController Instance
    {
        get {
            if (instance == null)
            {
                ScriptableObject.CreateInstance<SpawnController>();     // new SpawnController();
            }
            return instance;
        }
    }

    public void Restart()
    {
        playerTransforms = new ArrayList();
        playerGameObjects = new ArrayList();
    }

    public void SetUpPlayers(GameObject[] playerPrefabs, Vector3[] playerStartPositions, Quaternion[] playerStartRotations, Transform theParentObj, int totalPlayers)
    {
        playerPrefabList = playerPrefabs;
        startPositions = playerStartPositions;
        startRotations = playerStartRotations;
        CreatePlayers(theParentObj, totalPlayers);

    }

    public void CreatePlayers(Transform theParent, int totalPlayers)
    {
        playerTransforms = new ArrayList();
        playerGameObjects = new ArrayList();

        for (int i = 0; i < totalPlayers; i++)
        {
            // spawn a player
            tempTrans = Spawn(playerPrefabList[i], startPositions[i], startRotations[i]);

            if (theParent != null)
            {
                tempTrans.parent = theParent;
                tempTrans.localPosition = startPositions[i];
            }

            playerTransforms.Add(tempTrans);
            playerGameObjects.Add(tempTrans.gameObject);
        }
    }

    public GameObject GetPlayerGO(int indexNum)
    {
        return (GameObject)playerGameObjects[indexNum];
    }

    public Transform GetPlayerTransform(int indexNum)
    {
        return (Transform)playerTransforms[indexNum];
    }

    public Transform Spawn(GameObject anGO, Vector3 aPosition, Quaternion aRotation)
    {
        tempGO = (GameObject)Instantiate(anGO, aPosition, aRotation);
        tempTrans = tempGO.transform;

        return tempTrans;
    }
    public GameObject SpawnGO(GameObject anGO, Vector3 aPosition, Quaternion aRotation)
    {
        tempGO = (GameObject)Instantiate(anGO, aPosition, aRotation);
        tempTrans = tempGO.transform;

        return tempGO;
    }

    public ArrayList GetAllSpawnPlayer()
    {
        return playerTransforms;
    }
}


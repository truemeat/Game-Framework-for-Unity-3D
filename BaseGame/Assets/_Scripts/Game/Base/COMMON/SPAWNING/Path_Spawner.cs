using UnityEngine;

public class Path_Spawner : MonoBehaviour
{
    public Waypoints_Controller waypointController;

    // should we start spawning based on distance from the camera?
    // if distanceBased is false, we will need to call this class from
    // elsewhere, to spawn
    public bool distanceBasedSpawnStart;

    // if we're using distance based spawning, at what distance should we start?
    public float distanceFromCameraToSpawnAt = 35f;

    // if the distanceBasedSpawnStart is false, we can have the path spawner just start spawning automatically
    public bool shouldAutoStartSpawningOnLoad;

    public float timeBetweenSpawns = 1;
    public int totalAmountToSpawn = 10;
    public bool shouldReversePath;

    public GameObject[] spawnObjectPrefabs;

    private int totalSpawnObjects;

    private Transform myTransform;
    private GameObject tempObj;

    private int spawnCounter = 0;
    private int currentObjectNum;
    private Transform cameraTransform;
    private bool spawning;

    public bool shouldSetSpeed;
    public float speedToSet;
    public bool shouldSetSmoothing;
    public float smoothingToSet;

    public bool shoudSetTotateSpeed;
    public float rotateToSet;

    private bool didInit;

    void Start()
    {
        Init();
    }
    void Init()
    {
        myTransform = transform;

        Camera mainCam = Camera.main;

        if (mainCam == null)
            return;

        cameraTransform = mainCam.transform;

        // tell weypoint controller if we want to reverse the path or not
        waypointController.SetReverseMode(shouldReversePath);

        totalSpawnObjects = spawnObjectPrefabs.Length;

        if (shouldAutoStartSpawningOnLoad)
            StartWave(totalAmountToSpawn, timeBetweenSpawns);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(200, 0, distanceFromCameraToSpawnAt));
    }
    public void Update()
    {
        float aDist = Mathf.Abs(myTransform.position.z - cameraTransform.position.z);
        if (distanceBasedSpawnStart && !spawning && aDist < distanceFromCameraToSpawnAt)
        {
            StartWave(totalAmountToSpawn, timeBetweenSpawns);
            spawning = true;
        }
    }
    // ??? timeBetweenSPawns
    public void StartWave(int HowMany, float timeBetweenSpawns)
    {
        spawnCounter = 0;
        totalAmountToSpawn = HowMany;

        //reset
        currentObjectNum = 0;

        CancelInvoke("doSpawn");
        InvokeRepeating("doSpawn", timeBetweenSpawns, timeBetweenSpawns);
    }
    void doSpawn()
    {
        SpawnObject();  
    }
    public void SpawnObject()
    {
        if (spawnCounter >= totalAmountToSpawn)
        {
            // tell your script that the wave is finished here
            CancelInvoke("doSpawn");
            this.enabled = false;
            return;
        }

        // create an object

        tempObj = SpawnController.Instance.SpawnGO(spawnObjectPrefabs[currentObjectNum], myTransform.position, Quaternion.identity);

        // tell object to reverse its pathfinding, if required
        tempObj.SendMessage("SetReversePath", shouldReversePath, SendMessageOptions.DontRequireReceiver);

        // tell spawned object to use this waypoing controller
        tempObj.SendMessage("SetWayController", waypointController, SendMessageOptions.DontRequireReceiver);

        // tell object to use this speed (again with no required receiver just in case)
        if (shouldSetSpeed)
            tempObj.SendMessage("SetRotateSpeed", rotateToSet, SendMessageOptions.DontRequireReceiver);

        // increase the 'how many objects we have spawned' counter
        spawnCounter++;

        // increase the 'shich object to spawn'counter
        currentObjectNum++;

        // check to see if we've reached the end of the spawn objects array
        if (currentObjectNum > totalSpawnObjects - 1)
            currentObjectNum = 0;
    }
}


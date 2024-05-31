using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimedObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectPrefabs; // List of prefabs of the objects to spawn
    public Transform[] spawnLocations; // Array of transform locations to spawn the objects
    public float spawnInterval = 2f; // Interval in seconds between each spawn
    public int maxSpawnedObjects = 10; // Maximum number of spawned objects allowed in the scene
    public bool spawnContinuously = true; // Whether to continuously spawn objects

    private Queue<GameObject> objectQueue = new Queue<GameObject>(); // Queue of objects to spawn
    private int spawnedObjectCount = 0; // Number of spawned objects in the scene
    
    public int ObjectQueueCount => objectQueue.Count; // Property to get the count of objects in the queue
    public int objectsAvailable;

    public static TimedObjectSpawner Instance { get; private set; }
    public bool isSpawnCoroutineActive = false;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
            return;
        }

    }
    void Start()
    {
        // Fill the queue with object prefabs
        foreach (GameObject prefab in objectPrefabs)
        {
            objectQueue.Enqueue(prefab);
        }

        // Start spawning objects
        StartCoroutine(SpawnObjectsRoutine());
    }

    private void Update()
    {
        
        //if (isSpawnCoroutineActive)
        //    return;


        //if(spawnedObjectCount < maxSpawnedObjects)
        //{
        //    StopCoroutine(SpawnObjectsRoutine());
        //    StartCoroutine(SpawnObjectsRoutine());
        //    isSpawnCoroutineActive = true;
        //}

    }

    IEnumerator SpawnObjectsRoutine()
    {
        while (true)
        {
            // Wait for the spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Spawn the next object if there's room
            if (spawnedObjectCount < maxSpawnedObjects && objectQueue.Count > 0)
            {
                GameObject prefab = objectQueue.Dequeue();
                Transform randomSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
                SpawnObject(prefab, randomSpawnLocation);
            }

            // If continuous spawning is disabled and the queue is empty, exit the coroutine
            if (objectQueue.Count == 0)
            {
                
                yield break;
            }
        }
    }

    void SpawnObject(GameObject prefab, Transform spawnLocation)
    {
        if (prefab != null && spawnLocation != null)
        {
            // Instantiate the object at the random spawn location
            GameObject newObject = Instantiate(prefab, spawnLocation.position, spawnLocation.rotation);

            // Increment the count of spawned objects
            spawnedObjectCount++;
            objectsAvailable = ObjectQueueCount;
        }
        else
        {
            Debug.LogError("Object prefab or spawn location is not assigned!");
        }
        Debug.Log("Spawned Object " + spawnedObjectCount);
    }

    public void ReduceSpawnedObject()
    {
        if(spawnedObjectCount > 0)
        {
            spawnedObjectCount--;
            Debug.Log("Despawned " + spawnedObjectCount);
        }
        
    }
}

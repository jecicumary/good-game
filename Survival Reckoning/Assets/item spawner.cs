using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // List of objects to spawn
    public int numberOfObjectsToSpawn = 10; // Number of objects to spawn
    public Transform spawnTransform; // Transform where objects will be spawned

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogError("No objects to spawn. Please assign objects to the list.");
            return;
        }

        if (numberOfObjectsToSpawn <= 0)
        {
            Debug.LogWarning("Number of objects to spawn is less than or equal to 0. No objects will be spawned.");
            return;
        }

        if (spawnTransform == null)
        {
            Debug.LogError("Spawn transform not assigned. Please assign a transform in the Inspector.");
            return;
        }

        // Loop to spawn the specified number of objects
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Randomly select an object from the list
            GameObject objectToInstantiate = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

            // Instantiate the selected object at the specified transform position
            Vector3 spawnPosition = spawnTransform.position;
            Quaternion spawnRotation = spawnTransform.rotation;

            Instantiate(objectToInstantiate, spawnPosition, spawnRotation);
        }
    }
}

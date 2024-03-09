using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingTable;
    public GameObject[] walls;

    void Start()
    {
        DeactivateAllWalls();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && other.GetComponent<Resource>() != null)
        {
            CraftItem(other.gameObject);
        }
    }

    void CraftItem(GameObject item)
    {
        DestroyCraftedResource(item);

        // Enable a random wall from the list
        int randomWallIndex = Random.Range(0, walls.Length);
        walls[randomWallIndex].SetActive(true);
    }

    void DestroyCraftedResource(GameObject item)
    {
        Destroy(item);
    }

    void DeactivateAllWalls()
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(false);
        }
    }
}

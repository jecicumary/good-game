using UnityEngine;
using System.Collections.Generic;

public class ItemGrabber : MonoBehaviour
{
    public Transform itemHolder;  // Public variable to hold the items after grabbing
    public float grabRange = 3f;  // Maximum distance for the raycast
    public int maxItemsInHand = 7;  // Maximum number of items the player can hold
    private List<GameObject> heldItems = new List<GameObject>();  // List to store held items

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryGrabItem();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            UseHeldItems();
        }
    }

    void TryGrabItem()
    {
        // Check if the player has reached the maximum number of held items
        if (heldItems.Count >= maxItemsInHand)
        {
            Debug.Log("Cannot grab more items. Maximum items in hand reached.");
            return;
        }

        // Perform a raycast to check for items in front of the player
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, grabRange))
        {
            if (hit.collider.CompareTag("Item"))
            {
                GrabItem(hit.collider.gameObject);
            }
        }
    }

    void GrabItem(GameObject item)
    {
        // Disable the Rigidbody on pickup
        Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
        {
            itemRigidbody.isKinematic = true;
        }

        // Attach the item to the itemHolder
        item.transform.parent = itemHolder;

        // Center the item within the itemHolder
        item.transform.localPosition = Vector3.zero;

        // Add the item to the held items list
        heldItems.Add(item);

        Debug.Log("Item grabbed: " + item.name);
    }

    void DropItem()
    {
        if (heldItems.Count > 0)
        {
            GameObject lastHeldItem = heldItems[heldItems.Count - 1];

            // Enable the Rigidbody on drop
            Rigidbody itemRigidbody = lastHeldItem.GetComponent<Rigidbody>();
            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = false;
            }

            // Detach the item from the itemHolder
            lastHeldItem.transform.parent = null;

            // Remove the item from the held items list
            heldItems.Remove(lastHeldItem);

            Debug.Log("Item dropped: " + lastHeldItem.name);
        }
    }

    void UseHeldItems()
    {
        List<GameObject> itemsToRemove = new List<GameObject>();

        foreach (GameObject heldItem in heldItems)
        {
            if (heldItem != null)
            {
                // Check if the held item has an 'Item' script attached
                Item itemScript = heldItem.GetComponent<Item>();
                if (itemScript != null)
                {
                    // Call the UseItem method on each held item
                    itemScript.UseItem();
                }
            }
            else
            {
                // If the heldItem is null, mark it for removal from the list
                itemsToRemove.Add(heldItem);
            }
        }

        // Remove destroyed items from the heldItems list
        foreach (GameObject itemToRemove in itemsToRemove)
        {
            heldItems.Remove(itemToRemove);
        }
    }
}

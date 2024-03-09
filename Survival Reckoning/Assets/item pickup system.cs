using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    public Transform attachTransform; // Assign the attach transform in the Unity Editor
    public KeyCode grabKey = KeyCode.E; // Change this key to your desired grab key
    public KeyCode dropKey = KeyCode.Q; // Change this key to your desired drop key
    public KeyCode useKey = KeyCode.U; // Change this key to your desired use key
    public int maxItemsInHand = 10;

    private int itemsInHand = 0;
    private GameObject currentHeldItem;

    private void Update()
    {
        if (Input.GetKeyDown(grabKey))
        {
            TryGrabItem();
        }

        if (Input.GetKeyDown(dropKey))
        {
            DropAllItems();
        }

        if (Input.GetKeyDown(useKey))
        {
            UseHeldItem();
        }
    }

    private void TryGrabItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f)) // Adjust the raycast distance as needed
        {
            Item item = hit.collider.GetComponent<Item>();
            if (item != null && itemsInHand < maxItemsInHand)
            {
                GrabItem(item.gameObject);
            }
        }
    }

    private void GrabItem(GameObject item)
    {
        item.GetComponent<Rigidbody>().isKinematic = true; // Turn off Rigidbody
        item.transform.parent = attachTransform; // Attach to the specified transform
        item.transform.localPosition = Vector3.zero; // Adjust position if needed

        currentHeldItem = item;
        itemsInHand++;
    }

    private void DropAllItems()
    {
        foreach (Transform child in attachTransform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Turn on Rigidbody
            }

            child.parent = null; // Unparent the item
        }

        itemsInHand = 0;
        currentHeldItem = null;
    }

    private void UseHeldItem()
    {
        if (currentHeldItem != null)
        {
            Item itemComponent = currentHeldItem.GetComponent<Item>();
            if (itemComponent != null)
            {
                itemComponent.UseItem();
            }
        }
    }
}

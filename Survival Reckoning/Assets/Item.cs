using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    // UnityEvent for item used
    public UnityEvent onItemUsed;

    // Variables for the amount of hunger, thirst, and health to increase
    public float hungerIncreaseAmount = 10f;
    public float thirstIncreaseAmount = 5f;
    public float healthIncreaseAmount = 20f;

    // Method to simulate using the item
    public void UseItem()
    {
        Debug.Log("Item used: " + gameObject.name);

        // Call the IncreaseHunger, IncreaseThirst, and IncreaseHealth methods from the PlayerHealthSystem script
        PlayerHealthSystem playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
        if (playerHealthSystem != null)
        {
            playerHealthSystem.IncreaseHunger(hungerIncreaseAmount);
            playerHealthSystem.IncreaseThirst(thirstIncreaseAmount);
            playerHealthSystem.IncreaseHealth(healthIncreaseAmount);
        }
        else
        {
            Debug.LogWarning("PlayerHealthSystem not found in the scene.");
        }

        // Invoke the UnityEvent
        onItemUsed.Invoke();

        // Destroy the GameObject after invoking the event
        Destroy(gameObject);
    }
}

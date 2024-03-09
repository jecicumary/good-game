using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public Slider hungerSlider;
    public Slider thirstSlider;
    public Slider healthSlider;

    public float maxHunger = 100f;
    public float maxThirst = 100f;
    public float maxHealth = 100f;

    public float hungerDecreaseRate = 1f;
    public float thirstDecreaseRate = 1f;

    private float currentHunger;
    private float currentThirst;
    private float currentHealth;

    void Start()
    {
        currentHunger = maxHunger;
        currentThirst = maxThirst;
        currentHealth = maxHealth;

        UpdateUI();
    }

    void Update()
    {
        // Decrease hunger and thirst over time
        DecreaseHunger(hungerDecreaseRate * Time.deltaTime);
        DecreaseThirst(thirstDecreaseRate * Time.deltaTime);

        // Check if hunger or thirst is zero and decrease health accordingly
        if (currentHunger <= 0 || currentThirst <= 0)
        {
            DecreaseHealth(Time.deltaTime);
        }

        // Check if health is zero and destroy the player
        if (currentHealth <= 0)
        {
            DestroyPlayer();
        }

        UpdateUI();
    }

    public void DecreaseHunger(float value)
    {
        currentHunger = Mathf.Clamp(currentHunger - value, 0, maxHunger);
    }

    public void DecreaseThirst(float value)
    {
        currentThirst = Mathf.Clamp(currentThirst - value, 0, maxThirst);
    }

    public void DecreaseHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth - value, 0, maxHealth);
    }

    public void IncreaseHunger(float value)
    {
        currentHunger = Mathf.Clamp(currentHunger + value, 0, maxHunger);
        UpdateUI();
    }

    public void IncreaseThirst(float value)
    {
        currentThirst = Mathf.Clamp(currentThirst + value, 0, maxThirst);
        UpdateUI();
    }

    public void IncreaseHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        UpdateUI();
    }

    void DestroyPlayer()
    {
        // You can implement any game over logic here
        Debug.Log("Player destroyed!");
        // For now, let's just deactivate the player object
        gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        // Update UI sliders
        hungerSlider.value = currentHunger / maxHunger;
        thirstSlider.value = currentThirst / maxThirst;
        healthSlider.value = currentHealth / maxHealth;
    }
}

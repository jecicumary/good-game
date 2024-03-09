using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stats : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxWater = 100;
    public int maxFood = 100;

    private float currentHealth;
    private float currentWater;
    private float currentFood;

    public Slider healthSlider;
    public Slider waterSlider;
    public Slider foodSlider;

    public float healthDegradeRate = 1f;
    public float waterDegradeRate = 0.5f;
    public float foodDegradeRate = 0.3f;

    private bool canTakeDamage = true;
    public float damageCooldown = 0.3f; // Adjust the cooldown time as needed
    public float zombieDetectionRadius = 5f; // Adjust the detection radius for zombies

    private void Start()
    {
        currentHealth = maxHealth;
        currentWater = maxWater;
        currentFood = maxFood;

        UpdateSliders();
        InvokeRepeating(nameof(DegradeStats), 1f, 1f);
    }

    private void Update()
    {
        DegradeStats();
        CheckForZombies();
        HandleDamageCooldown();
    }

    private void UpdateSliders()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth / maxHealth;

        if (waterSlider != null)
            waterSlider.value = currentWater / maxWater;

        if (foodSlider != null)
            foodSlider.value = currentFood / maxFood;
    }

    private void DegradeStats()
    {
        DegradeWater();
        DegradeFood();

        if (currentFood <= 0 || currentWater <= 0)
        {
            // Degrade health if food or water is zero
            TakeDamage(healthDegradeRate);
        }

        UpdateSliders();
    }

    private void DegradeWater()
    {
        currentWater = Mathf.Clamp(currentWater - waterDegradeRate * Time.deltaTime, 0, maxWater);
    }

    private void DegradeFood()
    {
        currentFood = Mathf.Clamp(currentFood - foodDegradeRate * Time.deltaTime, 0, maxFood);
    }

    public void AddHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateSliders();
    }

    public void AddWater(float amount)
    {
        currentWater = Mathf.Clamp(currentWater + amount, 0, maxWater);
        UpdateSliders();
    }

    public void AddFood(float amount)
    {
        currentFood = Mathf.Clamp(currentFood + amount, 0, maxFood);
        UpdateSliders();
    }

    public void TakeDamage(float amount)
    {
        if (canTakeDamage)
        {
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

            if (currentHealth <= 0)
            {
                // Destroy the player or trigger game over logic
                Destroy(gameObject);
            }

            StartCoroutine(DamageCooldown());
        }

        UpdateSliders();
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }


    private void CheckForZombies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, zombieDetectionRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Zombie"))
            {
                TakeDamage(10);
            }
        }
    }

    private void HandleDamageCooldown()
    {
        if (!canTakeDamage)
        {
            return; // Damage cooldown is active, exit the method
        }

        StartCoroutine(DamageCooldown());
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Zombie"))
        {
            TakeDamage(10);
        }
    }
}

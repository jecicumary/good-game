using UnityEngine;
using System;

public class ZombieController : MonoBehaviour
{
    public float wanderSpeed = 2f;
    public float chaseSpeed = 5f;
    public float rotationSpeed = 2f;
    public float wanderRange = 10f;
    public float detectionRange = 5f;
    public float chaseTime = 5f;
    public int maxHealth = 100;
    public float destroyDelay = 0.1f; // Delay before destroying the zombie
    public float attackDelay = 1f;    // Delay between attacks
    public int damage = 10;           // Damage dealt to the player

    public event Action OnPlayerTouch; // Event to be triggered when zombie touches player
    public event Action OnAttack;      // Event to be triggered after the attack delay

    private Transform player;
    private float currentHealth;
    private bool isChasing;
    private float timer;
    private float attackTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player is tagged as 'Player' in the Unity editor.");
            return;
        }

        currentHealth = maxHealth; // Set initial health to maxHealth here
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            if (!IsInvoking("DestroyZombie"))
            {
                Invoke("DestroyZombie", destroyDelay);
            }
            return;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Wander();
        }
    }

    void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= chaseTime)
        {
            isChasing = true;
            timer = 0f;
            return;
        }

        transform.Translate(Vector3.forward * wanderSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * UnityEngine.Random.Range(-rotationSpeed, rotationSpeed));

        if (Physics.Raycast(transform.position, transform.forward, detectionRange))
        {
            isChasing = true;
        }
    }

    void ChasePlayer()
    {
        transform.LookAt(player);
        transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.position) > detectionRange)
        {
            isChasing = false;
        }

        // Check if enough time has passed since the last attack
        if (Time.time >= attackTimer)
        {
            Attack();
            attackTimer = Time.time + attackDelay; // Set the next allowed attack time
        }
    }

    void Attack()
    {
        // Implement your attack logic here

        // Trigger the OnAttack event
        OnAttack?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    void DestroyZombie()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Zombie touched the player, invoke the event
            OnPlayerTouch?.Invoke();

            // Delayed damage to the player
            Invoke("DealDamageToPlayer", destroyDelay);
        }
    }

    void DealDamageToPlayer()
    {
        // Get the PlayerHealthSystem and call the DecreaseHealth method
        PlayerHealthSystem playerHealthSystem = player.GetComponent<PlayerHealthSystem>();

        if (playerHealthSystem != null)
        {
            playerHealthSystem.DecreaseHealth(damage);
        }
    }
}

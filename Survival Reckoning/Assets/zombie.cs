using UnityEngine;

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

    private Transform player;
    private float currentHealth;
    private bool isChasing;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        transform.Rotate(Vector3.up * Random.Range(-rotationSpeed, rotationSpeed));

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
}

using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float chaseSpeed = 3f;        // Speed at which the zombie chases the player
    public float wanderSpeed = 1f;       // Speed at which the zombie wanders
    public float chaseRange = 10f;       // Range within which the zombie starts chasing the player
    public float wanderTime = 5f;        // Time the zombie wanders before stopping

    private Transform player;            // Reference to the player's transform
    private Vector3 wanderTarget;        // Random target for zombie to wander towards
    private float timer;                 // Timer for wandering behavior

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = wanderTime;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseRange)
        {
            ChasePlayer();
            timer = wanderTime; // Reset the timer when the player is in range
        }
        else
        {
            Wander();
        }
    }

    void ChasePlayer()
    {
        // Rotate towards the player
        transform.LookAt(player);
        transform.position += transform.forward * chaseSpeed * Time.deltaTime;
    }

    void Wander()
    {
        // If the zombie can't see the player, wander around
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            // Choose a random position within a circle and set it as the wander target
            wanderTarget = Random.insideUnitCircle * 5f;
            wanderTarget.z = wanderTarget.y;
            wanderTarget.y = 0f;
            wanderTarget += transform.position;

            timer = wanderTime;
        }

        // Rotate towards the wander target
        transform.LookAt(wanderTarget);
        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);
    }
}

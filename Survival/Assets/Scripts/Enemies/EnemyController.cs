using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float idleTime = 2f;
    public float walkSpeed = 2f; // Walking speed.
    public float chaseSpeed = 4f; // Chasing speed.
    public float sightDistance = 10f;
    public float triggerDistance = 15f;
    public float attackRange = 5;
    public float enemyDamage = 10f;
    public float attackCooldown = 5f;
    private float timeSinceLastAttack;
    public AudioClip idleSound;
    public AudioClip walkingSound;
    public AudioClip chasingSound;
    public AudioClip attackSound;
    public AudioClip deathSound;

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private float idleTimer = 0f;
    [SerializeField] private Transform player;
    private AudioSource audioSource;

    private enum EnemyState { Idle, Walk, Chase, Attack, Hit }
    private EnemyState currentState = EnemyState.Idle;

    private bool isChasingAnimation = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        SetDestinationToWaypoint();
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        switch (currentState)
        {
            case EnemyState.Idle:
                idleTimer += Time.deltaTime;
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsChasing", false); // Ensure IsChasing is set to false in the idle state.
                //PlaySound(idleSound);

                if (idleTimer >= idleTime)
                {
                    NextWaypoint();
                }

                CheckForPlayerDetection();
                break;

            case EnemyState.Walk:
                idleTimer = 0f;
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsChasing", false); // Set IsChasing to false when walking.
                //PlaySound(walkingSound);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    currentState = EnemyState.Idle;
                }

                CheckForPlayerDetection();
                break;

            case EnemyState.Chase:
                idleTimer = 0f;
                agent.speed = chaseSpeed; // Set the chase speed.
                agent.SetDestination(player.position);
                isChasingAnimation = true; // Set to true in chase state.
                animator.SetBool("IsChasing", true); // Set IsChasing to true in chase state.
                
                //PlaySound(chasingSound);

                // Check if the player is out of sight and go back to the walk state.
                if (Vector3.Distance(transform.position, player.position) > sightDistance)
                {
                    currentState = EnemyState.Walk;
                    agent.speed = walkSpeed; // Restore walking speed.
                }

                if (Vector3.Distance(transform.position, player.position) < attackRange)
                {
                    currentState = EnemyState.Attack;
                }

                if (!CanReachPlayer())
                {
                    // If the enemy can't reach the player, return to patrolling
                    currentState = EnemyState.Walk;
                    SetDestinationToWaypoint();
                }

                break;

            case EnemyState.Attack:
                agent.isStopped = true;
                animator.SetBool("IsAttacking", true);
                //PlaySound(attackSound);

                bool IsPlayerInAttackRange() => Vector3.Distance(transform.position, player.position) < attackRange;
                bool CanAttack() => timeSinceLastAttack >= attackCooldown;

                if (IsPlayerInAttackRange())
                {
                    if (CanAttack())
                    {
                        animator.SetBool("IsBattleIdle", false);

                        timeSinceLastAttack = 0f;

                        animator.SetBool("ToggleAttack", !animator.GetBool("ToggleAttack"));
                    }
                    else
                    {
                        animator.SetBool("IsBattleIdle", true);
                    }
                }
                else
                {
                    // If the player is no longer in attack range, transition back to the chase state
                    currentState = EnemyState.Chase;
                    agent.isStopped = false; // Allow movement again
                    animator.SetBool("IsAttacking", false); // Stop the attack animation
                    animator.SetBool("IsBattleIdle", false);
                }

                break;

            case EnemyState.Hit:
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) // Check if the Hit animation is still playing
                {
                    currentState = EnemyState.Chase;
                }
                break;
        }
    }

    private void CheckForPlayerDetection()
    {
        RaycastHit hit;
        Vector3 playerDirection = player.position - transform.position;

        if (Physics.Raycast(transform.position, playerDirection.normalized, out hit, sightDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                currentState = EnemyState.Chase;
                Debug.Log("Player detected!");
            }
        }
    }

    private bool CanReachPlayer()
    {
        // Update the agent's path to the player and check the path status
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(player.position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    public void ApplyDamage()
    {
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(enemyDamage);
        }
        
    }

    private void PlaySound(AudioClip soundClip)
    {
        if (Vector3.Distance(transform.position, player.position) <= triggerDistance)
        {
            if (!audioSource.isPlaying || audioSource.clip != soundClip)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
            }
        }
    }

    private void NextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        SetDestinationToWaypoint();
    }

    private void SetDestinationToWaypoint()
    {
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentState = EnemyState.Walk;
        agent.speed = walkSpeed; // Set the walking speed.
        animator.enabled = true;
    }

    // Draw a green raycast line at all times and switch to red when the player is detected.
    private void OnDrawGizmos()
    {
        Gizmos.color = currentState == EnemyState.Chase ? Color.red : Color.green;
        Gizmos.DrawLine(transform.position, player.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class BossEnemyMono : MonoBehaviour
{
    //Hiding and showing in Inspector
    [SerializeField]
    private int Health = 100;
    [SerializeField]
    private GameObject player;

    private NavMeshAgent agent;
    private BossEnemy bossStats;

    private Animator anim;
    [SerializeField]
    private float detectionDistance = 6;
    [SerializeField]
    private bool showDebug = true;
    [SerializeField]
    private LayerMask ObstacleMask;

    // Start is called before the first frame update
    void Start()
    {
        bossStats = new BossEnemy();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ObstacleMask = ~ObstacleMask;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distanceFromPlayerSq", bossStats.SquaredDistanceToPlayer(this.gameObject, player));//[Square] Distance between the Player and Enemy
        if (showDebug)
        {
            Debug.Log("Square Distance: " + bossStats.SquaredDistanceToPlayer(this.gameObject, player));

            // Linecast checks if an obstacle is between the enemy and the player
            // Player layer must be set to "Player" for cast to work
            // This condition is to prevent the enemy from detecting player through walls
            if (Physics.Linecast(this.gameObject.transform.position, player.transform.position, ObstacleMask))
            {
                Debug.Log("Linecast hit");
            }
            else
            {
                Debug.Log("Linecast no hit");
            }
        }
        CalculateDetectionRange();
        AttackRangeAnimExecution();
    }
    /// <summary>
    /// Calculates whether the player is within the sight of the enemy
    /// </summary>
    private void CalculateDetectionRange()
    {
        if (anim.GetFloat("distanceFromPlayerSq") <= Mathf.Pow(detectionDistance, 2) && !Physics.Linecast(this.gameObject.transform.position, player.transform.position, ObstacleMask)
            && !anim.GetBool("PlayerDetected"))
        {
            anim.SetBool("PlayerDetected", true);
            if (showDebug) Debug.Log("Enemy Detected!");
        }

    }
    /// <summary>
    /// If player is out of the enemy's attack range or there is an obstacle in the way, the enemy won't attack
    /// </summary>
    private void AttackRangeAnimExecution()
    {
        if (bossStats.SquaredDistanceToPlayer(this.gameObject, player) > (agent.stoppingDistance * agent.stoppingDistance) || Physics.Linecast(this.gameObject.transform.position, player.transform.position, ObstacleMask))
        {
            anim.SetBool("IsAttacking", false);
        }
        else
        {
            anim.SetBool("IsAttacking", true);
        }
    }
    void OnDrawGizmos()
    {
        if (showDebug)
        {
            Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 10, Color.red);
            Debug.DrawLine(this.transform.position, player.transform.position, Color.cyan);
        }
    }
}

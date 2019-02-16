using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemyMono : MonoBehaviour
{
    //Hiding and showing in Inspector
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float detectionDistance = 20;
    [SerializeField]
    private bool showDebug = true;
    [SerializeField, Tooltip("Set to Player layer")]
    private LayerMask ObstacleMask;

    private NavMeshAgent agent;
    private BossEnemy enemyStats;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = new BossEnemy();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ObstacleMask = ~ObstacleMask;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distanceFromPlayerSq", enemyStats.SquaredDistanceToPlayer(this.gameObject, player));//[Square] Distance between the Player and Enemy
        //Debug Utilities if enabled
        if (showDebug)
        {
            Debug.Log("Square Distance: " + enemyStats.SquaredDistanceToPlayer(this.gameObject, player));

            // Linecast checks if an obstacle is between the enemy and the player
            // Player layer must be set to "Player" for cast to work
            //This condition is to prevent the enemy from detecting player through walls
            if (Physics.Linecast(this.transform.position + new Vector3(0, this.GetComponent<Collider>().bounds.center.y / 3, 0), player.transform.position + new Vector3(0, player.GetComponent<Collider>().bounds.center.y / 3, 0), ObstacleMask))
            {
                Debug.Log("Linecast hit");
            }
            else
            {
                Debug.Log("Linecast no hit");
            }
            //Plays the Death Animation for Ai
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //anim.SetTrigger("Die");
            }
        }
        AttackRangeAnimExecution();
    }
    /// <summary>
    /// If player is out of the enemy's attack range or there is an obstacle in the way, the enemy won't attack
    /// </summary>
    private void AttackRangeAnimExecution()
    {
        if (enemyStats.SquaredDistanceToPlayer(this.gameObject, player) > (agent.stoppingDistance * agent.stoppingDistance) ||
            Physics.Linecast(this.transform.position + new Vector3(0, this.GetComponent<Collider>().bounds.center.y / 3, 0), player.transform.position + new Vector3(0, player.GetComponent<Collider>().bounds.center.y / 3, 0), ObstacleMask))
        {
            anim.SetBool("isAttacking", false);
        }
        else
        {
            anim.SetBool("isAttacking", true);
        }
    }
    //Debug Tools to show in editor at all times if enabled
    void OnDrawGizmos()
    {
        if (showDebug)
        {
            Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 10, Color.red);
            Debug.DrawLine(this.transform.position + new Vector3(0, this.GetComponent<Collider>().bounds.center.y / 3, 0), player.transform.position + new Vector3(0, player.GetComponent<Collider>().bounds.center.y / 3, 0), Color.cyan);
        }
    }
    public GameObject Target()
    {
        return player;
    }
}

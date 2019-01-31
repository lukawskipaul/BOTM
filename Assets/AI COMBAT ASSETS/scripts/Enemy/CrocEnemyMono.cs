using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class CrocEnemyMono : MonoBehaviour
{
    //Hiding and showing in Inspector
    [SerializeField]
    private int Health = 10;
    [SerializeField]
    private GameObject player;

    private CrocEnemy enemyStats;
    private Animator anim;
    public bool showDebug = true;

    private NavMeshAgent nma;
    public bool AttackRange;
    
    public LayerMask ObstacleMask;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = new CrocEnemy(Health);
        anim = GetComponent<Animator>();
        nma = GetComponent<NavMeshAgent>();
        ObstacleMask = ~ObstacleMask;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distanceFromPlayerSq",enemyStats.SquaredDistanceToPlayer(this.gameObject, player));//[Square] Distance between the Player and Enemy
        if (showDebug)
        {
            Debug.Log("Square Distance: " + enemyStats.SquaredDistanceToPlayer(this.gameObject, player));
            Debug.Log("Real Distance: " + Vector3.Distance(player.transform.position, this.gameObject.transform.position));
            Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 10, Color.red);
        }

        // Linecast checks if an obstacle is between the enemy and the player
        // Player layer must be set to "Player" for cast to work
        if (Physics.Linecast(this.gameObject.transform.position, player.transform.position, ObstacleMask))
        {
            Debug.Log("Linecast hit");
        }
        else
        {
            Debug.Log("Linecast no hit");
        }

        // If player is out of the enemy's attack range or there is an obstacle in the way, the enemy won't attack
        if (enemyStats.SquaredDistanceToPlayer(this.gameObject, player) > (nma.stoppingDistance * nma.stoppingDistance) || Physics.Linecast(this.gameObject.transform.position, player.transform.position, ObstacleMask))
        {
            anim.SetBool("InAttackRange", false);
        }
        else
        {
            anim.SetBool("InAttackRange", true);
        }
        
    }
    void OnDrawGizmos()
    {
        if (showDebug)
        {
            Debug.DrawLine(this.transform.position, player.transform.position, Color.cyan);
        }
    }

    public GameObject Target()
    {
        return player;
    }
}

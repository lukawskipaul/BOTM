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

	private NavMeshAgent agent;
    private CrocEnemy enemyStats;
	
    private Animator anim;
	public float detectionDistance = 6;
    public bool showDebug = true;

    public LayerMask ObstacleMask;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = new CrocEnemy(Health);
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ObstacleMask = ~ObstacleMask;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distanceFromPlayerSq",enemyStats.SquaredDistanceToPlayer(this.gameObject, player));//[Square] Distance between the Player and Enemy
        if (showDebug)
        {
            Debug.Log("Square Distance: " + enemyStats.SquaredDistanceToPlayer(this.gameObject, player));
            
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
            //Plays the Death Animation for Ai
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("Die");
            }
		}
        AttackRangeAnimExecution(); 
    }
    /// <summary>
    /// 
    /// </summary>
    private void CalculateDetectionRange()
    {
        if (anim.GetFloat("distanceFromPlayerSq") <= Mathf.Pow(detectionDistance, 2))
        {
            anim.SetBool("PlayerDetected", true);
            if (showDebug) Debug.Log("Enemy Detected!");
        }
        else
        {
            anim.SetBool("PlayerDetected", false);
            if (showDebug) Debug.Log("Enemy Lost!");
        }

    }
    /// <summary>
    /// If player is out of the enemy's attack range or there is an obstacle in the way, the enemy won't attack
    /// </summary>
    private void AttackRangeAnimExecution(){
		if (enemyStats.SquaredDistanceToPlayer(this.gameObject, player) > (agent.stoppingDistance * agent.stoppingDistance) || Physics.Linecast(this.gameObject.transform.position, player.transform.position, ObstacleMask))
		{
			anim.SetBool("InAttackRange", false);
		}
		else
		{
			anim.SetBool("InAttackRange", true);
		}
	}
	//Debug Tools to show in editor at all times if enabled
    void OnDrawGizmos()
    {
        if (showDebug)
        {
            Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 10, Color.red);
            Debug.DrawLine(this.transform.position, player.transform.position, Color.cyan);
        }
    }

    public GameObject Target()
    {
        return player;
    }
}

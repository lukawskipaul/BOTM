using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class CrocEnemyMono : MonoBehaviour
{
    //Hiding and showing in Inspector
    [SerializeField, Tooltip("Attack Damage Output")]
    private int attackDamage = 10;
    public int AttackDamage
    {
        get { return attackDamage; }
    }
    [SerializeField]
    private GameObject player;

	private NavMeshAgent agent;
    private CrocEnemy enemyStats;
	
    private Animator anim;
    [SerializeField]
	private float detectionDistance = 20;
    [SerializeField]
    private bool showDebug = false;
    [SerializeField,Tooltip("Set to Player layer")]
    private LayerMask ObstacleMask;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = new CrocEnemy();
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
            //This condition is to prevent the enemy from detecting player through walls
            if (Physics.Linecast(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), ObstacleMask))
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
                anim.SetTrigger("Flinch");
                anim.SetTrigger("Die");
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
        if (anim.GetFloat("distanceFromPlayerSq") <= Mathf.Pow(detectionDistance, 2) && !Physics.Linecast(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), ObstacleMask) 
            && !anim.GetBool("PlayerDetected"))
        {
            anim.SetBool("PlayerDetected",true);
            if (showDebug) Debug.Log("Enemy Detected!");
            AkSoundEngine.PostEvent("Play_CrocAggro1", gameObject);
        }

    }
    /// <summary>
    /// If player is out of the enemy's attack range or there is an obstacle in the way, the enemy won't attack
    /// </summary>
    private void AttackRangeAnimExecution(){
		if (enemyStats.SquaredDistanceToPlayer(this.gameObject, player) > (agent.stoppingDistance * agent.stoppingDistance) || 
            Physics.Linecast(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), ObstacleMask))
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
            Debug.DrawLine(new Vector3(this.transform.position.x,this.transform.position.y + 0.5f,this.transform.position.z), new Vector3(player.transform.position.x,player.transform.position.y + 0.5f,player.transform.position.z), Color.cyan);
        }
    }

    public GameObject Target()
    {
        return player;
    }
}

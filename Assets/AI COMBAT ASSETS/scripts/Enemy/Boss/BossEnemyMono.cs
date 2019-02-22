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
    private float startChargeDistance = 10;
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
            if (Physics.Linecast(new Vector3(this.transform.position.x, 0.4f, this.transform.position.z), new Vector3(player.transform.position.x, 0.4f, player.transform.position.z), ObstacleMask))
            {
                Debug.Log("Linecast hit");
            }
            else
            {
                Debug.Log("Linecast no hit");
            }
        }
        AttackRangeAnimExecution();
    }
    /// <summary>
    /// If player is out of the enemy's attack range or there is an obstacle in the way, the enemy won't attack
    /// </summary>
    private void AttackRangeAnimExecution()
    {
        if (bossStats.SquaredDistanceToPlayer(this.gameObject, player) > (startChargeDistance * startChargeDistance) || Physics.Linecast(new Vector3(this.transform.position.x, 0.4f, this.transform.position.z), new Vector3(player.transform.position.x, 0.4f, player.transform.position.z), ObstacleMask))
        {
            anim.SetBool("isLineOfObstacle", false);
        }
        else
        {
            anim.SetBool("isLineOfObstacle", true);
        }
    }
    void OnDrawGizmos()
    {
        if (showDebug)
        {
            Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 10, Color.red);
            Debug.DrawLine(new Vector3(this.transform.position.x,0.4f,this.transform.position.z), new Vector3(player.transform.position.x, 0.4f, player.transform.position.z), Color.cyan); /*player.transform.position + new Vector3(0,this.player.GetComponent<Collider>().bounds.center.y * 2 / 3, 0)*/
        }
    }
}

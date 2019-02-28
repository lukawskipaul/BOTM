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
    private bool showDebug = true;
    [SerializeField,Tooltip("Layer must be set to 'Player' for cast to work(and Enemy if neccessary)")]
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
            //Debug.Log("Square Distance: " + bossStats.SquaredDistanceToPlayer(this.gameObject, player));

            // Linecast checks if an obstacle is between the enemy and the player
            // Player layer must be set to "Player" for cast to work(and Enemy if neccessary)
            // This condition is to prevent the enemy from detecting player through walls
            if (Physics.Linecast(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), new Vector3(player.transform.position.x, player.transform.position.y / 2, player.transform.position.z), ObstacleMask))
            {
                Debug.Log("Linecast hit");
            }
            else
            {
                Debug.Log("Linecast no hit");
            }
        }
        ObstacleDetection();
    }
    /// <summary>
    /// Detects if obstacles is between Player and Enemy
    /// </summary>
    private void ObstacleDetection()
    {
        if (Physics.Linecast(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), new Vector3(player.transform.position.x, player.transform.position.y / 2, player.transform.position.z), ObstacleMask))
        {
            anim.SetBool("isLineOfObstacle", true);
        }
        else
        {
            anim.SetBool("isLineOfObstacle", false);
        }
    }
    void OnDrawGizmos()
    {
        if (showDebug)
        {
            Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 10, Color.red);
            Debug.DrawLine(new Vector3(this.transform.position.x,this.transform.position.y+0.5f,this.transform.position.z), new Vector3(player.transform.position.x, player.transform.position.y /2, player.transform.position.z), Color.cyan); /*player.transform.position + new Vector3(0,this.player.GetComponent<Collider>().bounds.center.y * 2 / 3, 0)*/
        }
    }
    public GameObject Player
    {
        get { return player; }
    }
}

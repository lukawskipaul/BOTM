using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EvalEnemy : MonoBehaviour {
    public GameObject player,projectile,waypoint;
    public LayerMask sightLayermask;
    [Tooltip("The distance the object can see another object.")]
    public float sightDistance = 6f;
    [Tooltip("The Field Of Vision of the object's sight.(Degrees)")]
    public float fieldOfView = 45f;
    [Tooltip("The Speed of the projectile.")]
    public float bulletSpeed = 1000f;
    

    private NavMeshAgent agent;
    private GameObject currentBullet;
    private Vector3 enemyToPlayer, LastKnownPos;
    private Animator anim;
    
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        enemyToPlayer = player.transform.position - this.transform.position;
        SightDetection();

        anim.SetFloat("distanceFromWayPoint", Vector3.Distance(waypoint.transform.position, this.transform.position));
        anim.SetFloat("distanceFromPlayer", enemyToPlayer.magnitude);

    }
    #region Waypoint Destination Settings
    /// <summary>
    /// Set the Destination for the Enemy to move towards a random location on the stage. (&& Moves it)
    /// </summary>
    public void MoveToNextRandomWayPoint()
    {
        waypoint.transform.position = new Vector3(Random.Range(-24f, 24f), 0.03f, Random.Range(-24f, 24f));
        agent.SetDestination(waypoint.transform.position);
    }
    /// <summary>
    /// Set the Destination for the Enemy to move towards the player location (&& Moves it)
    /// </summary>
    public void MoveWaypointToPlayer()
    {
        waypoint.transform.position = player.transform.position;
        agent.SetDestination(waypoint.transform.position);
    }
    #endregion
    /// <summary>
    /// Fire One projectile at a time continuously
    /// </summary>
    public void ShootBullet()
    {
        if (currentBullet == null && player.activeSelf)
        {
            Debug.Log("Shot Bullet!");
            currentBullet = Instantiate(projectile);
            currentBullet.transform.position = this.transform.position + this.transform.forward + new Vector3(0, 0.3f, 0);
            currentBullet.transform.localEulerAngles = this.transform.localEulerAngles;
            currentBullet.GetComponent<Rigidbody>().AddForce(this.transform.forward * bulletSpeed);
        }
    }
    /// <summary>
    /// Provides the Enemy w/ eye sensors to see target(s)
    /// </summary>
    private void SightDetection()
    {
        RaycastHit hit;
        Debug.DrawLine(this.transform.position,this.transform.position + this.transform.TransformDirection(Vector3.forward) * sightDistance,Color.blue);
        if (Vector3.Angle(this.transform.TransformDirection(Vector3.forward), enemyToPlayer) < fieldOfView)
        {
            if (Physics.Raycast(this.transform.position, enemyToPlayer, out hit,sightDistance, sightLayermask))
            {
                if(hit.collider.tag == "Player")
                {
                    anim.SetBool("isPlayerVisible", true);
                    Debug.Log("Player Sighted!");
                }
            }
            else
            {
                anim.SetBool("isPlayerVisible", false);
                
            }
        }
        //else
        //{
        //    anim.SetBool("isPlayerVisible", false);

        //}
    }
    /// <summary>
    /// Provides the Enemy w/ touch sensors to detect target(s)
    /// </summary>
    /// <param name="other"></param>
    #region Touch Trigger Sensors
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("isPlayerVisible", true);
            Debug.Log("Player Touched!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("isPlayerVisible",false);
        }
    }
    #endregion
    public void SetLastKnownPosition()
    {
        LastKnownPos = player.transform.position;
    }
    public Vector3 GetLastKnowPosition()
    {
        return LastKnownPos;
    }

}

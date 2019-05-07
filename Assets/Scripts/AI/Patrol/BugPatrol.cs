using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugPatrol : MonoBehaviour
{
    // This bool decides whether or not the Game Object
    // using this script is actively patrolling.
    [HideInInspector]
    public bool IsPatrolling = false;

    // The speed the Game Object should be moving
    // as it makes its patrol.
    public float PatrolSpeed = 1.0f;

    // The stopping distance of the Game Object
    // as it makes its patrol.
    public float PatrolStoppingDistance = 0.0f;

    // The list of locations that creates the path
    // to be patrolled.
    // This list must contain at least two Waypoints
    // in order to work.
    [SerializeField]
    private List<Waypoint> route;

    // The NavMesh agent of the Game Object using this script.
    [SerializeField]
    private NavMeshAgent navMeshAgent;

    // The index in the route List of the current waypoint 
    // that the Game Object is moving towards.
    [SerializeField]
    private int currentTargetIndex = 0;

    // The position of the next Waypoint to act
    // as the Game Object's destination
    [SerializeField]
    private Vector3 nextWaypointPosition = Vector3.zero;

    // The number of Waypoints in the route List.
    private int routeSize = 0;

    // This bool decides if the Game Object
    // is going up or down its route list.
    //
    // This way, the Game Object is able
    // to move backwards on its route.
    [SerializeField]
    private bool movingForward = true;

    // The distance remaining between the Game Object
    // and its destination
    private float remainingDistance = 0.0f;

    // The minimum distance between the Game Object
    // and is destination before moving onto the next Waypoint
    [SerializeField]
    private float thresholdDistance = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        // Check if there is a NavMeshAgent attached to Game Object
        if (navMeshAgent == null)
        {
            Debug.LogError("No NavMeshAgent attached to Game Object using AIPatrol.");
        }

        // Check if there are enough Waypoints to patrol
        if (route == null || route.Count < 2)
        {
            Debug.LogError("AIPatrol on " + this.gameObject.name + " does not have enough Waypoints.");
        }
        else
        {
            routeSize = route.Count;
            Debug.Log("routeSize = " + route.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPatrolling)
        {
            if (navMeshAgent.remainingDistance < thresholdDistance)
            {
                SetTargetIndex();
                SetNewDestination();
            }

            remainingDistance = navMeshAgent.remainingDistance;
        }
        else // (!IsPatrolling)
        {
            currentTargetIndex = 0;
            movingForward = true;
        }
    }

    // This function goes up or down the route list
    // and sets the next destination of the patrolling
    // Game Object accordingly.
    private void SetTargetIndex()
    {
        if (movingForward)
        {
            ++currentTargetIndex;

            if (currentTargetIndex >= routeSize)
            {
                movingForward = false;
                currentTargetIndex = (routeSize - 2);
            }
        }
        else // if (!movingForward)
        {
            --currentTargetIndex;

            if (currentTargetIndex < 0)
            {
                movingForward = true;
                currentTargetIndex = 1;
            }
        }
    }

    // Set a new destination every time the Game Object 
    // reaches its latest target node.
    private void SetNewDestination()
    {
        nextWaypointPosition = route[currentTargetIndex].transform.position;
        navMeshAgent.SetDestination(nextWaypointPosition);
    }
}

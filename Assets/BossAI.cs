using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// A script to hold variables necessary for the boss to behave properly
// To see the boss' actions, check the Boss AI's Animation state machine and associated behaviour scripts

[RequireComponent(typeof(NavMeshAgent))]
public class BossAI : MonoBehaviour
{
    [Tooltip("The player character")]
    [SerializeField]
    private GameObject player;

    [Tooltip("A target object used for visualization and debugging")]
    [SerializeField]
    private GameObject target;

    [Tooltip("The Nav Mesh Agent attached to the boss character")]
    [SerializeField]
    private NavMeshAgent bossNavMeshAgent;

    [SerializeField]
    private LayerMask obstacleMask;

    [Tooltip("A scalar used to determine how far past a target point the boss charges")]
    [SerializeField]
    [Range(1, 2)]
    private float chargeDistanceScalar = 1;

    [Tooltip("A scalar controlling how much faster the boss moves when charging")]
    [SerializeField]
    private float chargeSpeedScalar = 1;

    [Tooltip("A scalar controlling how fast the boss accelerates when charging")]
    [SerializeField]
    private float chargeAccelerationScalar = 1;

    [Tooltip("A scalar controlling how much faster the boss' angular speed is when charging")]
    [SerializeField]
    private float chargeAngularSpeedScalar = 1;

    [Tooltip("The maximum distance the boss should be from the player character before stopping when charging")]
    [SerializeField]
    private float chargeTempStopping = 1;

    [Tooltip("The maximum distance the boss should be from the player character when strafing")]
    [SerializeField]
    private float strafeRadius = 1;

    [Tooltip("How fast the boss character moves from side to side when strafing")]
    [SerializeField]
    private float strafeSpeed = 1;

    [Tooltip("How long the boss character strafes in one direction before alternating (in seconds)")]
    [SerializeField]
    private float strafeTime = 1;

    [Tooltip("The maximum distance that the boss character can jump back")]
    [SerializeField]
    private float jumpBackDistance = 1;

    [Tooltip("A scalar controlling how fast the boss cahracter rotates to look at the player")]
    [SerializeField]
    private float lookRotationSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        bossNavMeshAgent = GetComponent<NavMeshAgent>();
        obstacleMask = ~obstacleMask;
    }

    // Getters/ Setters
    public NavMeshAgent BossNavMeshAgent
    {
        get { return bossNavMeshAgent; }
        set { bossNavMeshAgent = value; }
    }
    
    public GameObject Player
    {
        get { return player; }
    }

    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    public float ChargeDistanceScalar
    {
        get { return chargeDistanceScalar; }
    }

    public float ChargeSpeedScalar
    {
        get { return chargeSpeedScalar; }
    }

    public float ChargeAccelerationScalar
    {
        get { return chargeAccelerationScalar; }
    }

    public float ChargeAngularSpeedScalar
    {
        get { return chargeAngularSpeedScalar; }
    }

    public float StrafeRadius
    {
        get { return strafeRadius; }
    }

    public float StrafeSpeed
    {
        get { return strafeSpeed; }
    }

    public float StrafeTime
    {
        get { return strafeTime; }
    }

    public float JumpBackDistance
    {
        get { return jumpBackDistance; }
    }

    public float LookRotationSpeed
    {
        get { return lookRotationSpeed; }
    }

    public LayerMask ObstacleMask
    {
        get { return obstacleMask; }
    }

    /*// Update is called once per frame
    void Update()
    {

    }//*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// A script to hold variables necessary for the boss to behave properly
// To see the boss' actions, check the Boss AI's Animation state machine and associated behaviour scripts

[RequireComponent(typeof(NavMeshAgent))]
public class BossAI : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private NavMeshAgent bossNavMeshAgent;

    [SerializeField]
    [Range(1, 2)]
    private float chargeDistanceScalar = 1;

    [SerializeField]
    private float speedScalar = 1;

    [SerializeField]
    private float accelerationScalar = 1;

    [SerializeField]
    private float angularSpeedScalar = 1;

    [SerializeField]
    private float tempStopping = 1;

    [SerializeField]
    private float strafeRadius = 1;

    [SerializeField]
    private float strafeSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        bossNavMeshAgent = GetComponent<NavMeshAgent>();
    }
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
    public float SpeedScalar
    {
        get { return speedScalar; }
    }
    public float AccelerationScalar
    {
        get { return accelerationScalar; }
    }
    public float AngularSpeedScalar
    {
        get { return angularSpeedScalar; }
    }

    public float StrafeRadius
    {
        get { return strafeRadius; }
    }

    public float StrafeSpeed
    {
        get { return strafeSpeed; }
    }

    /*// Update is called once per frame
    void Update()
    {

    }//*/
}

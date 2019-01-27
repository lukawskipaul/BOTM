using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
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
    
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = new CrocEnemy(Health);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distanceFromPlayerSq",enemyStats.SquaredDistanceToPlayer(this.gameObject, player));
        if (showDebug)
        {
            Debug.Log("Square Distance: " + enemyStats.SquaredDistanceToPlayer(this.gameObject, player));
        }
        
    }
    void OnDrawGizmos()
    {
        if (showDebug)
        {
            Debug.DrawLine(this.transform.position, player.transform.position, Color.cyan);

           
        }
    }
}

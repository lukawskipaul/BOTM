using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    private BattleStateMachine bsm;
    public BaseHero Hero;
    AudioSource audioData;
    Animator theAnimator;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState CurrentState;

    //private float cur_cooldown = 0f;
    //private float max_cooldown = 2f;

    //public Image ProgressBar;

    private GameObject selector;

    public GameObject EnemyToAttack;

    private Vector3 startPosition;

    private bool actionStarted = false;

    private float animationSpeed = 10f;

    [SerializeField]
    private bool alive = true;

    public bool Guard = false;

    public ActionType Action = ActionType.NULL;

    // Use this for initialization
    void Start ()
    {
        startPosition = transform.position;
        //cur_cooldown = Random.Range(0, 2.5f);
        bsm = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        CurrentState = TurnState.PROCESSING;
        selector = Hero.Selector;
        selector.SetActive(false);
        theAnimator = GetComponent<Animator>();

    }   // VOID START

    // Update is called once per frame
    void Update ()
    {
        //Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case (TurnState.PROCESSING):
                {
                    //UpgradeProgressBar();
                    CurrentState = TurnState.ADDTOLIST;
                    break;
                }
            case (TurnState.ADDTOLIST):
                {
                    bsm.HeroesToManage.Add(this.gameObject);
                    CurrentState = TurnState.WAITING;
                    break;
                }
            case (TurnState.WAITING):
                {
                    // idle state
                    break;
                }
            /*case (TurnState.SELECTING):
                {
                    break;
                }//*/
            case (TurnState.ACTION):
                {
                    // Resets hero's defense after guarding 
                    Hero.CurrentDEF = Hero.BaseDEF;

                    if (bsm.EnemiesInBattle.Count > 0)
                    {
                        if (Action == ActionType.GUARD)
                        {
                            StartCoroutine(guard());
                        }
                        else if (Action == ActionType.MELEE_ATTACK)
                        {
                            StartCoroutine(timeForAction());
                        }
                    }
                    break;
                }
            case (TurnState.DEAD):
                {
                    if(!alive)
                    {
                        return;
                    }
                    else
                    {
                        // change tag to dead
                        this.gameObject.tag = "DeadHero";

                        // cannot be attacked by enemy
                        bsm.HeroesInBattle.Remove(this.gameObject);

                        // not recognized by manager
                        bsm.HeroesToManage.Remove(this.gameObject);

                        // deactivate selector
                        selector.SetActive(false);

                        // reset gui
                        bsm.AttackPanel.SetActive(false);
                        bsm.EnemySelectPanel.SetActive(false);

                        // remove item from performList
                        for (int i = 0; i < bsm.PerformersList.Count; ++i)
                        {
                            if(bsm.PerformersList[i].AttackersGameObject == this.gameObject)
                            {
                                bsm.PerformersList.Remove(bsm.PerformersList[i]);
                            }
                        }

                        // death animation

                        // // PLACEHOLDER FUNCITON UNTIL DEATH ANIMATIONS ARE IMPLEMENTED // 
                        //this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(0, 0, 0, 64);

                        // reset heroInput
                        bsm.PlayerInput = BattleStateMachine.HeroGUI.ACTIVATE;
                        Debug.Log(this.gameObject.name + ".heroInput Reset");

                        // decrement characters list
                        bsm.DecrementCharactersCount();
                        Debug.Log("charactersCount decremented.");

                        alive = false;
                        Debug.Log(this.gameObject.name + ".alive = false");
                    }

                    break;
                }
            default:
                {

                    break;
                }
        }
	}   // VOID UPDATE

    /*void UpgradeProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
        if(cur_cooldown >= max_cooldown)
        {
            CurrentState = TurnState.ADDTOLIST;
        }

    }   // VOID UPGRADEPROGRESSBAR
    */

    private IEnumerator timeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        EnemyToAttack = CheckEnemyDead(EnemyToAttack);

        actionStarted = true;
        Debug.Log(this.gameObject.name + " Action Started");

        // animate the enemy near the hero to attack
        theAnimator.SetBool("IsMoving", true);
        Vector3 heroPosition = new Vector3(EnemyToAttack.transform.position.x, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z - 1.0f);
        while (moveTowards(heroPosition)) { yield return null; }

        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        // wait
        theAnimator.SetBool("IsMoving", false);
        theAnimator.SetBool("BaseAttack", true);
        yield return new WaitForSeconds(2.0f);
        // theAnimator.SetBool("IsMoving", false);
        theAnimator.SetBool("BaseAttack", false);



        // do damage
        doDamage();
        

        // back to start position
        //theAnimator.SetBool("IsMoving", true);
        Vector3 firstPosition = startPosition;
        while (moveTowards(firstPosition)) { yield return null; }
        
       
        theAnimator.SetBool("IsMoving", false);

        // remove this performer from the list in the BattleStateMachine (BSM)
        bsm.ExecutePerformersList.RemoveAt(0);

        // reset bsm -> wait
        bsm.BattleState = BattleStateMachine.ActionState.WAIT;

        // end coroutine
        actionStarted = false;

        // reset this enemy state
        Debug.Log(this.gameObject.name + " Action Ended");
        CurrentState = TurnState.PROCESSING;
    }

    private IEnumerator guard()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;
        Debug.Log(this.gameObject.name + " Began guarding.");

        // animate the hero going into a guard position
        theAnimator.SetBool("IsGuarding", true);

        // wait
        yield return new WaitForSeconds(1.0f);

        // Defense up
        guardUp();

        // remove this performer from the list in the BattleStateMachine (BSM)
        bsm.ExecutePerformersList.RemoveAt(0);

        // reset bsm -> wait
        bsm.BattleState = BattleStateMachine.ActionState.WAIT;

        // end coroutine
        actionStarted = false;

        // reset this enemy state
        Debug.Log(this.gameObject.name + " Finished setting up guard.");
        theAnimator.SetBool("IsGuarding", false);
        CurrentState = TurnState.PROCESSING;
    }

    private bool moveTowards(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animationSpeed * Time.deltaTime));
    }

    public void TakeDamage(float damageAmount)
    {
        // calculate damage
        damageAmount -= Hero.CurrentDEF;

        if (damageAmount <= 0)
        {
            damageAmount = 1;
        }

        // take damage
        Hero.CurrentHP -= damageAmount;
        Debug.Log(this.gameObject.name + " took " + damageAmount + " damage!");

        if (Hero.CurrentHP < 0)
        {
            Hero.CurrentHP = 0;
        }
        Debug.Log(this.gameObject.name + ".HP = " + Hero.CurrentHP);

        // check if the character died
        if (Hero.CurrentHP == 0)
        {
            CurrentState = TurnState.DEAD;
            theAnimator.SetBool("IsDead", true);

        }
    }

    private void doDamage()
    {
        float calculatedDamage = Hero.CurrentATK + Hero.Attacks[0].AttackBaseDamage;

        EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calculatedDamage);
    }

    public void CheckTargetDead(HandleTurn myAttack)
    {
        for (int i = 0; i < bsm.HeroesInBattle.Count; ++i)
        {
            if (myAttack.AttackersTarget == bsm.EnemiesInBattle[i])
            {
                // If the hero's attack target is still active, return
                return;
            }
        }

        myAttack.AttackersTarget = bsm.EnemiesInBattle[Random.Range(0, bsm.EnemiesInBattle.Count)];
        Debug.Log(this.gameObject.name + " switched targets to " + myAttack.AttackersTarget.name);
    }

    public GameObject CheckEnemyDead(GameObject target)
    {
        for (int i = 0; i < bsm.EnemiesInBattle.Count; ++i)
        {
            if (target == bsm.EnemiesInBattle[i])
            {
                // If the hero's attack target is still active, return
                return target;
            }
        }

        target = bsm.EnemiesInBattle[Random.Range(0, bsm.EnemiesInBattle.Count)];
        Debug.Log(this.gameObject.name + " switched targets to " + target.name);
        return target;
    }

    private void guardUp()
    {
        Hero.CurrentDEF = Hero.BaseDEF * 1.5f;
    }

}   // PUBLIC CLASS

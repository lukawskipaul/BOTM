using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
    public BaseEnemy Enemy;

    private BattleStateMachine bsm;

    public EnemySelectButton EnemyButton;

    AudioSource audioData;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState CurrentState;
    
    private Vector3 startPosition;
    //private Vector3 stopPosition;

    private bool actionStarted = false;

    private GameObject selector;

    public GameObject HeroToAttack;

    private float animationSpeed = 10f;

    [SerializeField]
    private bool alive = true;

    // Use this for initialization
    void Start ()
    {
        startPosition = transform.position;
        CurrentState = TurnState.PROCESSING;
        bsm = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        selector = Enemy.Selector;
        selector.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (CurrentState)
        {
            case (TurnState.PROCESSING):
                {
                    //UpgradeProgressBar();
                    CurrentState = TurnState.CHOOSEACTION;
                    break;
                }
            case (TurnState.CHOOSEACTION):
                {
                    if (bsm.HeroesInBattle.Count > 0) { ChooseAction(); }
                    CurrentState = TurnState.WAITING;
                    break;
                }
            case (TurnState.WAITING):
                {
                    // idle state
                    break;
                }
            case (TurnState.ACTION):
                {
                    StartCoroutine(timeForAction());
                    break;
                }
            case (TurnState.DEAD):
                {
                    if (!alive)
                    {
                        return;
                    }
                    else
                    {
                        // change tag to dead
                        this.gameObject.tag = "DeadEnemy";

                        // cannot be attacked by enemy
                        bsm.EnemiesInBattle.Remove(this.gameObject);

                        // deactivate selector
                        selector.SetActive(false);

                        // remove item from performList
                        for (int i = 0; i < bsm.ExecutePerformersList.Count; ++i)
                        {
                            if (bsm.ExecutePerformersList[i].AttackersGameObject == this.gameObject)
                            {
                                bsm.ExecutePerformersList.Remove(bsm.ExecutePerformersList[i]);
                            }
                        }

                        // death animation

                        // // PLACEHOLDER FUNCITON UNTIL DEATH ANIMATIONS ARE IMPLEMENTED // 
                        //this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(0, 0, 0, 64);

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
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Type = "Enemy";
        myAttack.AttackersName = Enemy.Name;
        myAttack.AttackersGameObject = this.gameObject;
        myAttack.AttackersTarget = bsm.HeroesInBattle[Random.Range(0, bsm.HeroesInBattle.Count)];
        myAttack.TurnPriority = Enemy.TurnPriority;

        int randomAttack = Random.Range(0, Enemy.Attacks.Count);
        myAttack.ChosenAttack = Enemy.Attacks[randomAttack];
        Debug.Log(this.gameObject.name + " uses " + myAttack.ChosenAttack.AttackName + " for " + myAttack.ChosenAttack.AttackBaseDamage + " DMG!");

        bsm.CollectActions(myAttack);
    }

    private IEnumerator timeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;
        Debug.Log(this.gameObject.name + " Action Started");

        if (bsm.HeroesInBattle.Count > 0)
        {
            // animate the enemy near the hero to attack
            Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z + 1.0f);
            while (moveTowards(heroPosition)) { yield return null; }

            audioData = GetComponent<AudioSource>();
            audioData.Play(0);
            // wait
            yield return new WaitForSeconds(0.5f);

            // do damage
            doDamage();

            // back to start position
            Vector3 firstPosition = startPosition;
            while (moveTowards(firstPosition)) { yield return null; }
        }

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

    private bool moveTowards(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animationSpeed * Time.deltaTime));
    }

    public void TakeDamage(float damageAmount)
    {
        // calculate damage
        damageAmount -= Enemy.CurrentDEF;

        if (damageAmount <= 0)
        {
            damageAmount = 1;
        }

        // take damage
        Enemy.CurrentHP -= damageAmount;

        // check if the character died
        if (Enemy.CurrentHP <= 0)
        {
            CurrentState = TurnState.DEAD;
        }
    }

    private void doDamage()
    {
        float calculatedDamage = Enemy.CurrentATK + bsm.ExecutePerformersList[0].ChosenAttack.AttackBaseDamage;

        HeroToAttack.GetComponent<HeroStateMachine>().TakeDamage(calculatedDamage);
    }

    public void CheckTargetDead(HandleTurn myAttack)
    {
        for (int i = 0; i < bsm.HeroesInBattle.Count; ++i)
        {
           if (myAttack.AttackersTarget == bsm.HeroesInBattle[i])
            {
                // If the enemy's attack target is still active, return
                return;
            }
        }

        myAttack.AttackersTarget = bsm.HeroesInBattle[Random.Range(0, bsm.HeroesInBattle.Count)];
        Debug.Log(this.gameObject.name + " switched targets to " + myAttack.AttackersTarget.name);
    }
}

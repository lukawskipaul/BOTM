using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameObjects with this script require the components below, a component will be added if one does not exist
[RequireComponent(typeof(Animator))]

//This script goes on the player
public class PlayerAttack : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private int tkPullDamageAmount = 10;
    [SerializeField]
    private float tkPullCooldownInSeconds = 10.0f;
    [SerializeField]
    private GameObject journalMenu;
    [SerializeField]
    private GameObject pauseMenu;

    private Animator anim;
    private DamageEnemy swordAttack;
    private GameObject enemy;

    private bool canAttack;
    private bool canDoTKPull;

    private const string attackButtonName = "Attack";
    private const string tkThrowButtonName = "Throw";
    private const string baseAttackBooleanName = "isAttackBase";
    private const string combo1AttackBooleanName = "isAttackCombo";
    private const string attackAnimationBooleanName = "Attack";
    private const string tkPullAnimationTriggerName = "TKPull";
    private const string freeLookDodgeAnimationTriggerName = "FreeLookDodge";
    private const string lockedOnDodgeAnimationTriggerName = "LockedOnDodge";

    #endregion

    private void Awake()
    {
        canAttack = true;
        canDoTKPull = true;
    }

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        swordAttack = this.gameObject.GetComponentInChildren<DamageEnemy>();
    }

    private void Update()
    {
        if (canAttack)
        {
            Attack();

            //if (canDoTKPull)
            //{
            //    TKPull();
            //}
        }
    }

    private void Attack()
    {
        /* Play attack animation when attack button is pressed */
        if (Input.GetButtonDown(attackButtonName) && !journalMenu.gameObject.activeInHierarchy && !pauseMenu.gameObject.activeInHierarchy)
        {
            /* Cancels possible tk pull queuing */
            anim.ResetTrigger(tkPullAnimationTriggerName);

            /* Cancels possible dodge queuing */
            anim.ResetTrigger(freeLookDodgeAnimationTriggerName);
            anim.ResetTrigger(lockedOnDodgeAnimationTriggerName);

            anim.SetBool(attackAnimationBooleanName, true);
        }
    }

    private void TKPull()
    {
        /* Play TK pull animation when push button is pressed */
        if (Input.GetButtonDown(tkThrowButtonName))
        {
            /* Cancels possible combo attack queuing */
            anim.SetBool(attackAnimationBooleanName, false);

            /* Cancels possible dodge queuing */
            anim.ResetTrigger(freeLookDodgeAnimationTriggerName);
            anim.ResetTrigger(lockedOnDodgeAnimationTriggerName);

            /* Search for enemy to attack */
            DetectObject.TKPullTargetSearchNeeded = true;

            anim.SetTrigger(tkPullAnimationTriggerName);

            //TODO: change enemy location
            //TODO: stun enemy?

            enemy.gameObject.GetComponent<EnemyHealth>().DamageEnemy(tkPullDamageAmount);

            //TODO: ability cooldown as animation event
        }
    }

    /* Disables/enables attacking when carrying/dropping with telekenesis */
    private void SetCanAttack()
    {
        if (canAttack)
        {
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
    }

    /* Assigns enemy GameObject to class variable */
    private void FindEnemy(GameObject detectedEnemy)
    {
        enemy = detectedEnemy;
    }

    /* Subscribe to events */
    private void OnEnable()
    {
        Telekinesis.TeleManualMovingObject += SetCanAttack;
        Telekinesis.TeleStoppedManualMovingObject += SetCanAttack;

        DetectObject.TKPullTargetDetected += FindEnemy;
    }

    /* Unsubscribe from events */
    private void OnDisable()
    {
        Telekinesis.TeleManualMovingObject -= SetCanAttack;
        Telekinesis.TeleStoppedManualMovingObject -= SetCanAttack;

        DetectObject.TKPullTargetDetected -= FindEnemy;
    }

    #region Animation Events

    /* Remember, changing name of animation event functions requires changing the function in the animation event! */

    /* Called during specific attack animation frame to start doing damage to hit enemies */
    public void StartDamageWindow()
    {
        swordAttack.IsAttacking = true;
        Time.timeScale = 0.5f;
    }

    /* Called during specific attack animation frame to stop doing damage to hit enemies */
    public void EndDamageWindow()
    {
        swordAttack.IsAttacking = false;
        Time.timeScale = 1.0f;
    }

    /* Called at specific tk pull animation frame to start tk pull cooldown */
    public void StartTKPullCooldown()
    {
        StartCoroutine(TKPullCooldown());
    }

    /* Starts cooldown for the player's tk pull ability */
    private IEnumerator TKPullCooldown()
    {
        canDoTKPull = false;

        yield return new WaitForSecondsRealtime(tkPullCooldownInSeconds);

        canDoTKPull = true;
    }

    #endregion
}

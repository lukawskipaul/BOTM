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

    private Animator anim;
    private DamageEnemy swordAttack;

    private bool canAttack;

    private const string attackButtonName = "Attack";
    private const string tkThrowButtonName = "Throw";
    private const string baseAttackBooleanName = "isAttackBase";
    private const string combo1AttackBooleanName = "isAttackCombo";
    private const string attackAnimationBooleanName = "Attack";
    private const string freeLookDodgeAnimationTriggerName = "FreeLookDodge";
    private const string lockedOnDodgeAnimationTriggerName = "LockedOnDodge";

    #endregion

    private void Awake()
    {
        canAttack = true;
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
            //TKPull();
        }
    }

    private void Attack()
    {
        /* Play attack animation when attack button is pressed */
        if (Input.GetButtonDown(attackButtonName))
        {
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
            //bool attackAnimationIsPlaying = anim.GetBool(baseAttackBooleanName) || anim.GetBool(combo1AttackBooleanName);   //will need to be updated with all attack animation names

            ///* Cancels possible combo attack queuing */
            //if (attackAnimationIsPlaying)
            //{
            //    anim.ResetTrigger(attackAnimationTriggerName);
            //}

            ///* Cancels possible dodge queuing */
            //anim.ResetTrigger(freeLookDodgeAnimationTriggerName);
            //anim.ResetTrigger(lockedOnDodgeAnimationTriggerName);

            //TODO: play animation
            //TODO: change enemy location
            //TODO: stun enemy?

            //TODO: ENEMY.gameObject.GetComponent<EnemyHealth>().DamageEnemy(tkPullDamageAmount);

            //TODO: ability cooldown
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

    /* Subscribe to events */
    private void OnEnable()
    {
        Telekinesis.TeleManualMovingObject += SetCanAttack;
        Telekinesis.TeleStoppedManualMovingObject += SetCanAttack;

        //TODO: subscribe to detectobject OnEnemyObjDetected
    }

    /* Unsubscribe from events */
    private void OnDisable()
    {
        Telekinesis.TeleManualMovingObject -= SetCanAttack;
        Telekinesis.TeleStoppedManualMovingObject -= SetCanAttack;

        //TODO: unsubscribe to detectobject OnEnemyObjDetected
    }

    #region Animation Events

    /* Remember, changing name of animation event functions requires changing the function in the animation event! */

    /* Was originally intended to prevent attack queuing, but not helpful with combo system active */
    /* Called at start of attack animation to prevent being able to attack again */
    //public void StartAttackAnimation()
    //{
    //    canAttack = false;
    //}

    ///* Called at end of attack animation to allow being able to attack again */
    //public void EndAttackAnimation()
    //{
    //    canAttack = true;
    //}

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

    #endregion
}

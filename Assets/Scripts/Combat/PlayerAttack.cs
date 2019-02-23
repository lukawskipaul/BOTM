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
    private const string baseAttackAnimationName = "Attack Base";
    private const string combo1AttackAnimationName = "Attack Combo 1";

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
            anim.ResetTrigger("FreeLookDodge");
            anim.ResetTrigger("LockedOnLookDodge");

            anim.SetTrigger("Attack");

            bool baseAttackAnimationIsPlaying = anim.GetCurrentAnimatorStateInfo(0).IsName(baseAttackAnimationName);
            bool combo1AttackAnimationIsPlaying = anim.GetCurrentAnimatorStateInfo(0).IsName(combo1AttackAnimationName);

            /* Changes damage value depending on whether base or combo animation is playing */
            if (baseAttackAnimationIsPlaying)
            {
                swordAttack.ChangeToBaseDamage();
            }
            else if (combo1AttackAnimationIsPlaying)
            {
                swordAttack.ChangeToCombo1Damage();
            }
        }
    }

    private void TKPull()
    {
        /* Play TK pull animation when push button is pressed */
        if (Input.GetButtonDown(tkThrowButtonName))
        {
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
    }

    /* Unsubscribe from events */
    private void OnDisable()
    {
        Telekinesis.TeleManualMovingObject -= SetCanAttack;
        Telekinesis.TeleStoppedManualMovingObject -= SetCanAttack;
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

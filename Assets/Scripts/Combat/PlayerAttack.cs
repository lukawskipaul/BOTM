using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//GameObjects with this script require the components below, a component will be added if one does not exist
[RequireComponent(typeof(Animator))]

//This script goes on the player
public class PlayerAttack : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Slider tkPullCooldownSlider;
    [SerializeField]
    private int tkPullDamageAmount = 10;
    [SerializeField]
    private float tkPullCooldownInSeconds = 10.0f;

    private Animator anim;
    private DamageEnemy swordAttack;
    private GameObject enemy;

    private float tkPullCooldownRemaining;

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

        tkPullCooldownSlider.maxValue = tkPullCooldownInSeconds;
        tkPullCooldownSlider.minValue = 0;
    }

    private void Update()
    {
        InputCameraChange cameraChange = GetComponent<InputCameraChange>();

        canDoTKPull = cameraChange.lockOn;

        if (canAttack)
        {
            Attack();

            if (canDoTKPull)
            {
                TKPull();
            }
        }


    }

    private void Attack()
    {
        /* Play attack animation when attack button is pressed */
        if (Input.GetButtonDown(attackButtonName))
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
            InputCameraChange cameraChange = GetComponent<InputCameraChange>();
            if (cameraChange.lockOn)
            {
                /* Cancels possible combo attack queuing */
                anim.SetBool(attackAnimationBooleanName, false);

                /* Cancels possible dodge queuing */
                anim.ResetTrigger(freeLookDodgeAnimationTriggerName);
                anim.ResetTrigger(lockedOnDodgeAnimationTriggerName);

                /* Search for enemy to attack */
                enemy = cameraChange.GetLockOnTarget();

                anim.SetTrigger(tkPullAnimationTriggerName);

                //TODO: change enemy location
                //TODO: stun enemy?

                enemy.gameObject.GetComponent<EnemyHealth>().DamageEnemy(tkPullDamageAmount);

                //TODO: ability cooldown as animation event
            }
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
        StopCoroutine(TKPullCooldown());
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

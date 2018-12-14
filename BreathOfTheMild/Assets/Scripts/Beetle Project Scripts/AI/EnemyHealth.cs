using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script will go on an AI to determine its Health and Flinchin
public class EnemyHealth : MonoBehaviour
{
    //BRENDAN ADDED CODE HERE
    public GameObject thisEnemy;
    //END BRENDAN CODE
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    private float sliderOffset = 5f;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float maxFlinch = 0f;
    [SerializeField]
    private float slamAttackDelay = 0f;
    [SerializeField]
    private Material[] injuryState;

    //private Canvas enemyCanvasClone;
    //private Slider healthBarClone;
    //private SkinnedMeshRenderer sknMeshRndr;
    //private Animator anim;

    private float currentFlinch;
    private bool waitActive;

    

    void Start()
    {
        //sknMeshRndr = GetComponent<SkinnedMeshRenderer>();
        //anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        currentFlinch = 0;
        UpdateHealthBar();
        InjuryStatus();
    }

    private void Update()
    {
        InjuryStatus();
        if (waitActive == false)
        {
            //anim.SetBool("canSlam", false);
        }
    }

    void LateUpdate()
    {
        UpdateHealthBar();
    }

    //This is called by the DamageEnemy script, which will be placed on a Trigger that overlaps with the AI and can cause damage
    public void DamageEnemy(float amount)
    {
        currentHealth -= amount;
        currentFlinch++;

        //This will trigger the AI Death Animation when hit
        if (currentHealth <= 0)
        {
            //anim.SetBool("isDead", true);
            //anim.SetBool("chasePlayer", false);
            //anim.SetBool("breatheFire", false);
            //anim.SetBool("attackPlayer", false);
            Destroy(healthBar.gameObject); //was Destroy(healthBar)
            //BRENDAN ADDED CODE HERE
            Destroy(thisEnemy);
            //END BRENDAN CODE
            
        }

        /// <summary>
        /// Once the AI has flinched a certain amount of times, it will enter the Slam Animation, similar
        /// to other attacks in the Basic AI script, before resetting the currentFlinch counter.  The AI
        /// cannot be flinched while performing this action due to the Wait Coroutine.
        /// </summary>
        else if (currentFlinch >= maxFlinch)
        {
            //anim.SetBool("canSlam", true);
            StartCoroutine(Wait(slamAttackDelay));            
            currentFlinch = 0;
        }
        else
        {
            //anim.SetTrigger("takeDamage");
        }
    }

    //This function can restore an AI's health, not currently used
    public void HealEnemy(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //This is used to update the healthBar attached to the AI
    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;                
    }

    //This is used to alter the damage material on the AI depending on currentHealth, similar to ARK: Survival Evolved
    private void InjuryStatus()
    {
        if (currentHealth / maxHealth >= 2f / 3f)
        {
            //sknMeshRndr.sharedMaterial = injuryState[0];
        }
        else if (currentHealth / maxHealth >= 1f / 3f)
        {
            //sknMeshRndr.sharedMaterial = injuryState[1];
        }
        else
        {
            //sknMeshRndr.sharedMaterial = injuryState[2];
        }
    }
    private IEnumerator Wait(float delay)
    {
        waitActive = true;
        yield return new WaitForSeconds(delay);
        waitActive = false;
    }
}

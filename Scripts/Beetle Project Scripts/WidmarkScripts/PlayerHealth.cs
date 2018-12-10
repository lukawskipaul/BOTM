using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;

    //CameraShake camShake;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float deathDelay = 1f;

    [SerializeField]
    Transform respawnPoint;

    public AudioMixerSnapshot NearDeath;
    public AudioMixerSnapshot Healthy;

    private Animator anim;

    void Start()
    {
        //camShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
        currentHealth = maxHealth;
        UpdateHealthBar();
        anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        UpdateHealthBar();
        
    }

    public void DamagePlayer(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            // TODO: lose game

            //temp solution to losing
            StartCoroutine(Die());
        }
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;

        NearDeathAudio();
    }

    IEnumerator Die()
    {
        Destroy(this.gameObject.GetComponent<Rigidbody>());
        GetComponent<PlayerMove>().enabled = false;
        anim.SetBool("isDead", true);
        yield return new WaitForSecondsRealtime(deathDelay);
        anim.SetBool("isDead", false);       

        currentHealth = maxHealth;
        this.gameObject.transform.position = respawnPoint.position;
        GetComponent<PlayerMove>().enabled = true;
    }

    void NearDeathAudio()
    {
        if (currentHealth <= 30)
        {
            NearDeath.TransitionTo(.1f);
        }
        else
        {
            Healthy.TransitionTo(.02f);
        }
    }



}

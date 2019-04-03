using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script goes on the player
public class PlayerHealthUI : MonoBehaviour
{
    #region Variables
    //fuck the system again
    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private GameObject playerDamageVignetteLowDamage;
    [SerializeField]
    private GameObject playerDamageVignetteLowMidDamage;
    [SerializeField]
    private GameObject playerDamageVignetteMidHighDamage;
    [SerializeField]
    private GameObject playerDamageVignetteHighDamage;
    [SerializeField]
    private PlayerHealth health;

    public bool lowDamageVignetteActive;
    public bool lowMidDamageVignetteActive;
    public bool midHighDamageVignetteActive;
    public bool highDamageVignetteActive;

    #endregion

    void Start()
    {
        //health = this.gameObject.GetComponent<PlayerHealth>();

        healthBar.maxValue = health.MaxHealth;

        playerDamageVignetteLowDamage.gameObject.SetActive(false);
        playerDamageVignetteLowMidDamage.gameObject.SetActive(false);
        playerDamageVignetteMidHighDamage.gameObject.SetActive(false);
        playerDamageVignetteHighDamage.gameObject.SetActive(false);
    }

    void Update()
    {
        DamageVignetteOneActivate();
        DamageVignetteOneFadeOff();
        DamageVignetteFadeInOne();

        DamageVignetteTwoActivate();
        DamageVignetteFadeOffTwo();
        DamageVignetteFadeInTwo();

        DamageVignetteThreeActivate();
        DamageVignetteFadeOffThree();
        DamageVignetteFadeInThree();

        DamageVignetteFourActivate();
        DamageVignetteFadeOffFour();
        DamageVignetteFadeInFour();

        HandleVignette();
        //UpdateHealthBar();
    }

    void FixedUpdate()
    {
        UpdateHealthBar();
        HandleVignette();
    }

    void UpdateHealthBar()
    {
        Debug.Log("This is being called.");
        /* Updates health bar with current health */
        healthBar.value = health.CurrentHealth;
    }

    void HandleVignette()
    {
        if (health.CurrentHealth <= 75 && health.CurrentHealth >= 50)
        {
            lowDamageVignetteActive = false;

            lowMidDamageVignetteActive = true;
        }
        else if (health.CurrentHealth <= 50 && health.CurrentHealth >= 25)
        {
            lowDamageVignetteActive = false;
            lowMidDamageVignetteActive = false;

            midHighDamageVignetteActive = true;
        }
        else if (health.CurrentHealth <= 25 && health.CurrentHealth >= 1)
        {
            lowDamageVignetteActive = false;
            lowMidDamageVignetteActive = false;
            midHighDamageVignetteActive = false;

            highDamageVignetteActive = true;
        }
        else if (health.CurrentHealth == health.MaxHealth)
        {
            lowDamageVignetteActive = false;
            lowMidDamageVignetteActive = false;
            midHighDamageVignetteActive = false;
            highDamageVignetteActive = false;
            playerDamageVignetteLowDamage.GetComponent<CanvasGroup>().alpha = 0;
            playerDamageVignetteLowMidDamage.GetComponent<CanvasGroup>().alpha = 0;
            playerDamageVignetteMidHighDamage.GetComponent<CanvasGroup>().alpha = 0;
            playerDamageVignetteHighDamage.GetComponent<CanvasGroup>().alpha = 0;

            playerDamageVignetteLowDamage.gameObject.SetActive(false);
            playerDamageVignetteLowMidDamage.gameObject.SetActive(false);
            playerDamageVignetteMidHighDamage.gameObject.SetActive(false);
            playerDamageVignetteHighDamage.gameObject.SetActive(false);
        }
    }

    void DamageVignetteOneActivate()
    {
        if (lowDamageVignetteActive == true)
        {
            DamageVignetteFadeInOne();

            playerDamageVignetteLowDamage.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
        }
    }

    void DamageVignetteOneFadeOff()
    {

        if (playerDamageVignetteLowDamage.GetComponent<CanvasGroup>().alpha <= 0 && lowDamageVignetteActive == true)
        {
            lowDamageVignetteActive = false;
        }
    }

    void DamageVignetteFadeInOne()
    {
        if (lowDamageVignetteActive == false && playerDamageVignetteLowDamage.activeSelf == true)
        {
            lowDamageVignetteActive = true;
            playerDamageVignetteLowDamage.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void DamageVignetteTwoActivate()
    {
        if (lowMidDamageVignetteActive == true)
        {
            playerDamageVignetteLowDamage.GetComponent<CanvasGroup>().alpha = 0;

            DamageVignetteFadeInTwo();

            playerDamageVignetteLowMidDamage.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
        }
    }

    void DamageVignetteFadeOffTwo()
    {
        if (playerDamageVignetteLowMidDamage.GetComponent<CanvasGroup>().alpha <= 0 && lowMidDamageVignetteActive == true)
        {
            lowMidDamageVignetteActive = false;
        }
    }

    void DamageVignetteFadeInTwo()
    {
        if (lowMidDamageVignetteActive == false && playerDamageVignetteLowMidDamage.activeSelf == true)
        {
            lowMidDamageVignetteActive = true;
            playerDamageVignetteLowMidDamage.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void DamageVignetteThreeActivate()
    {
        if (midHighDamageVignetteActive == true)
        {
            playerDamageVignetteLowDamage.GetComponent<CanvasGroup>().alpha = 0;
            playerDamageVignetteLowMidDamage.GetComponent<CanvasGroup>().alpha = 0;

            DamageVignetteFadeInThree();

            playerDamageVignetteMidHighDamage.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
        }
    }

    void DamageVignetteFadeOffThree()
    {
        if (playerDamageVignetteMidHighDamage.GetComponent<CanvasGroup>().alpha <= 0 && midHighDamageVignetteActive == true)
        {
            midHighDamageVignetteActive = false;
        }
    }

    void DamageVignetteFadeInThree() // Checks to see if the SplashScreen is active. If it isn't already active, then the DamageSlashScreen appears. 
    {
        if (midHighDamageVignetteActive == false && playerDamageVignetteMidHighDamage.activeSelf == true)
        {
            midHighDamageVignetteActive = true;
            playerDamageVignetteMidHighDamage.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void DamageVignetteFourActivate()
    {
        if (highDamageVignetteActive == true)
        {
            playerDamageVignetteLowDamage.GetComponent<CanvasGroup>().alpha = 0;
            playerDamageVignetteLowMidDamage.GetComponent<CanvasGroup>().alpha = 0;
            playerDamageVignetteMidHighDamage.GetComponent<CanvasGroup>().alpha = 0;

            DamageVignetteFadeInFour();

            playerDamageVignetteHighDamage.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
        }
    }

    void DamageVignetteFadeOffFour()
    {
        if (playerDamageVignetteHighDamage.GetComponent<CanvasGroup>().alpha <= 0 && highDamageVignetteActive == true)
        {
            highDamageVignetteActive = false;
        }
    }

    void DamageVignetteFadeInFour() // Checks to see if the SplashScreen is active. If it isn't already active, then the DamageSlashScreen appears.
    {
        if (highDamageVignetteActive == false && playerDamageVignetteHighDamage.activeSelf == true)
        {
            highDamageVignetteActive = true;
            playerDamageVignetteHighDamage.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}

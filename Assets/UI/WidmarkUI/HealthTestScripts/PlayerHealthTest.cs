using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthTest : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    GameObject playerDamageVignetteLowDamage;
    [SerializeField]
    GameObject playerDamageVignetteLowMidDamage;
    [SerializeField]
    GameObject playerDamageVignetteMidHighDamage;
    [SerializeField]
    GameObject playerDamageVignetteHighDamage;
    [SerializeField]
    Transform respawnPoint;

    public bool lowDamageVignetteActive;
    public bool lowMidDamageVignetteActive;
    public bool midHighDamageVignetteActive;
    public bool highDamageVignetteActive;

    public bool isDamaged;
    public bool healthIsRegenerating;

    public bool damagedWhileRegen;



    void Start()
    {

        currentHealth = maxHealth;
        UpdateHealthBar();

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


        HealthRegenTrigger();
        HealthAtMax();
        CheckIfRegenIsFalse();
        HealthRegen();



    }

    void LateUpdate()
    {
        UpdateHealthBar();
    }
    

    public void DamagePlayer(float amount)
    {
        //StopCoroutine(HealthRegen());
        isDamaged = true;
        healthIsRegenerating = false;

        currentHealth -= amount;

        lowDamageVignetteActive = true;
        playerDamageVignetteLowDamage.gameObject.SetActive(true);

        if (currentHealth <= 75)
        {
            lowDamageVignetteActive = false;
            lowMidDamageVignetteActive = true;
            playerDamageVignetteLowMidDamage.gameObject.SetActive(true);

        }


        if (currentHealth <= 50)
        {
            lowMidDamageVignetteActive = false;
            midHighDamageVignetteActive = true;
            playerDamageVignetteMidHighDamage.gameObject.SetActive(true);
        }

        if (currentHealth <= 25)
        {
            midHighDamageVignetteActive = false;
            highDamageVignetteActive = true;
            playerDamageVignetteHighDamage.gameObject.SetActive(true);
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }

        else if (currentHealth >= 100)
        {
            lowDamageVignetteActive = false;
            lowMidDamageVignetteActive = false;
            midHighDamageVignetteActive = false;
            highDamageVignetteActive = false;

            StopCoroutine(HealthRegenTimer());
        }
        
    }

    void HandleVignette()
    {
        if (currentHealth <= 75 && currentHealth >= 50)
        {
            lowDamageVignetteActive = false;


            lowMidDamageVignetteActive = true;
        }


        if (currentHealth <= 50 && currentHealth >= 25 )
        {
            lowDamageVignetteActive = false;
            lowMidDamageVignetteActive = false;

            midHighDamageVignetteActive = true;
        }

        if (currentHealth <= 25 && currentHealth >= 1)
        {
            lowDamageVignetteActive = false;
            lowMidDamageVignetteActive = false;
            midHighDamageVignetteActive = false;

            highDamageVignetteActive = true;
        }

        else if (currentHealth == maxHealth)
        {
            isDamaged = false;
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
            StopCoroutine(HealthRegenTimer());
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

  

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            currentHealth = maxHealth;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            healthIsRegenerating = false;
            isDamaged = true;
            StopRegen();
        }
    }


    IEnumerator Die()
    {
        Destroy(this.gameObject.GetComponent<Rigidbody>());

        yield return new WaitForSecondsRealtime(3.0f);

        currentHealth = maxHealth;
        this.gameObject.transform.position = respawnPoint.position;
    }

    void HealthRegenTrigger()
    {
        if (isDamaged == true)
        {
            //StartCoroutine(DamageIsHandled());
            StartCoroutine(HealthRegenTimer());
            healthIsRegenerating = false;
        }
    }

    IEnumerator HealthRegenTimer()
    {
        
         
        yield return new WaitForSecondsRealtime(4.0f);
        //currentHealth += Time.deltaTime * 5;
        healthIsRegenerating = true;
        isDamaged = false;
    }

    void HealthRegen()
    {
        if (healthIsRegenerating == false)
        {
            StopCoroutine(HealthRegenTimer());
            currentHealth = currentHealth;
        }

        if (healthIsRegenerating == true)
        {
            currentHealth += Time.deltaTime * 5;
        }
    }

    void StopRegen()
    {
        StopCoroutine(HealthRegenTimer());
        
    }

    void CheckIfRegenIsFalse()
    {
        if (healthIsRegenerating == false)
        {
            StopRegen();
        }
    }

    void HealthAtMax()
    {
        if (currentHealth >= maxHealth)
        {
            StopRegen();
            currentHealth = maxHealth;
            healthIsRegenerating = false;
            isDamaged = false;
        }
    }

    //IEnumerator DamageIsHandled()
    //{
    //    if (IsDamaged == true)
    //    {
    //        yield return new WaitForSecondsRealtime(1.0f);
    //        IsDamaged = false;
    //    }


    //}

    




}

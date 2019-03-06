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

    public bool hasTakenDamage = false;

    public float healthCoolDown;







    //

    //public float shakeStrength = 1;
    //public float shake = 1;

   // Vector3 originalPosition;



    //

    private void Awake()
    {
       // originalPosition = transform.localPosition;
    }

    void Start()
    {

        currentHealth = maxHealth;
        UpdateHealthBar();

        playerDamageVignetteLowDamage.gameObject.SetActive(false);
        playerDamageVignetteLowMidDamage.gameObject.SetActive(false);
        playerDamageVignetteMidHighDamage.gameObject.SetActive(false);
        playerDamageVignetteHighDamage.gameObject.SetActive(false);

        healthCoolDown = 1000.0f;

        //shake = 0;


        // might not need a camera shake 
        //originalPosition = transform.localPosition;

        


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


        //PlayerHealthRestoreCountDown();
        //HealthFullyRestored();

        PlayerHealthRestoreCountDown();
        HealthFullyRestored();
        DamageTakenHandler();

    }

    void LateUpdate()
    {
        UpdateHealthBar();
        //CameraShake();
    }
    

    public void DamagePlayer(float amount)
    {
        hasTakenDamage = true;

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

        else if (currentHealth == 100)
        {
            
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
            hasTakenDamage = false;
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


    //

    //void CameraShake()
    //{
    //    if (HasTakenDamage == true)
    //    {
    //        shake = shakeStrength;
    //    }

    //    Camera.main.transform.localPosition = originalPosition + (Random.insideUnitSphere * shake);

    //    shake = Mathf.MoveTowards(shake, 0, Time.deltaTime * shakeStrength);

    //    HasTakenDamage = false;

    //    if (shake == 0)
    //    {
    //        Camera.main.transform.localPosition = originalPosition;
    //        HasTakenDamage = false;
    //    }
    //}


    //


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

    void PlayerHealthRestoreCountDown()
    {
        if (currentHealth < maxHealth )
        {
            StopCoroutine(HasTakenDamageTimer());
            StartCoroutine(HealthRestoreTimer());
            //hasTakenDamage = false;
        }
    }


    //void RestoreHealth()
    //{
    //    currentHealth =+ Time.deltaTime;
    //}

    void HealthFullyRestored()
    {
        if (currentHealth >= maxHealth)
        {
            StopCoroutine(HealthRestoreTimer());
            //healthCoolDown = 1000f;
            currentHealth = 100;
            hasTakenDamage = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            currentHealth = maxHealth;
        }
    }

    void DamageTakenHandler()
    {
        if (hasTakenDamage == true)
        {
            StartCoroutine(HasTakenDamageTimer());
            StopCoroutine(HealthRestoreTimer());
            

        }
    }



    IEnumerator Die()
    {
        Destroy(this.gameObject.GetComponent<Rigidbody>());

        yield return new WaitForSecondsRealtime(3.0f);

        currentHealth = maxHealth;
        this.gameObject.transform.position = respawnPoint.position;
    }

    IEnumerator HealthRestoreTimer()
    {
        yield return new WaitForSecondsRealtime(10.0f);
        currentHealth += Time.deltaTime * 2;
    
    }

    IEnumerator HasTakenDamageTimer()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        hasTakenDamage = false;
    }

    

}

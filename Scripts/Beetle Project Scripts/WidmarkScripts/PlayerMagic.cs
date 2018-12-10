using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMagic : MonoBehaviour
{
    [SerializeField]
    Slider magicBar;
    [SerializeField]
    Transform projectileSpawnPoint;
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    private float projectileSpeed = 40f;

    [SerializeField]
    GameObject healSplashScreen;
    [SerializeField]
    GameObject healSpellActiveText;
    [SerializeField]
    GameObject fireSpellActiveText;




    [SerializeField]
    private float healAmount = -10f;



    AudioSource[] audioSources;
    AudioSource fireBall;
    AudioSource healSFX;

    public float currentMagic;
    const int maxMagic = 100;
    bool coolDownHasStarted;

    const string magicButtonName = "Magic Attack";

    const string toggleAbilityName = "Ability Toggle";


    public bool healsActive;
    public bool fireActive;

    private bool healScreenHasBeenActivated;



    //PlayerHealth other;

    void Start ()
    {
        audioSources = this.gameObject.GetComponents<AudioSource>();
        coolDownHasStarted = false;
        currentMagic = maxMagic;
        fireBall = audioSources[1];
        healSFX = audioSources[4];

        healsActive = true;
        fireActive = false;
        healSplashScreen.gameObject.SetActive(false);
    }
	
	void Update ()
    {
        CurrentActiveSpell();
        HealSplashScreenOn();
        HealSplashScreenOff();
        ToggleAbilities();
        CastMagic();
        UpdateMagicBar();
        MagicBarCoolDown();
        MagicBarHasCooledDown();
    }

    void CastMagic()
    {
       if (Input.GetButtonDown(magicButtonName) && currentMagic == maxMagic && healsActive == false && fireActive == true)
        {
            Debug.Log("reached cast magic");
            Fire();
            currentMagic = 0f;
            fireBall.Play();
        }

       if (Input.GetButtonDown(magicButtonName) && currentMagic == maxMagic && healsActive == true && fireActive == false)
        {
            Debug.Log("You have been healed");
            CastHeal();
            currentMagic = 0f;
            healSFX.Play();
        }
    }

    void UpdateMagicBar()
    {
        magicBar.value = currentMagic / maxMagic;
    }

    void MagicBarCoolDown()
    {
        if (currentMagic < maxMagic)
        {
            coolDownHasStarted = true;
            currentMagic += Time.deltaTime * 50;
        }
    }

    void MagicBarHasCooledDown()
    {
        if (coolDownHasStarted = true && currentMagic >= maxMagic)
        {
            coolDownHasStarted = false;
            currentMagic = 100;
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var projectile = (GameObject)Instantiate(
            projectilePrefab,
            projectileSpawnPoint.position,
            projectileSpawnPoint.rotation);

        // Add velocity to the bullet
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;

        // Destroy the bullet after 2 seconds
        Destroy(projectile, 2.0f);
    }

    void CastHeal()
    {
        test();
        healScreenHasBeenActivated = true;
        healSplashScreen.gameObject.SetActive(true);
        this.gameObject.GetComponent<PlayerHealth>().HealPlayer(healAmount);
    }

    void ToggleAbilities()
    {
        if (Input.GetButtonDown(toggleAbilityName) && healsActive == false && fireActive == true)
        {
            fireActive = false;
            healsActive = true;           
        }

        else if (Input.GetButtonDown(toggleAbilityName) && healsActive == true && fireActive == false)
        {
            healsActive = false;
            fireActive = true;
        }
    }

    void HealSplashScreenOn()
    {
        if (healScreenHasBeenActivated == true)
        {
            test();

            healSplashScreen.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
        }
    }

    void HealSplashScreenOff()
    {

        if (healSplashScreen.GetComponent<CanvasGroup>().alpha <= 0 && healScreenHasBeenActivated == true)
        {
            healScreenHasBeenActivated = false;
        }
    }


    void test() // Checks to see if the SplashScreen is active. If it isn't already active, then the DamageSlashScreen appears. 

    {
        if (healScreenHasBeenActivated == false && healSplashScreen.activeSelf == true)
        {
            healScreenHasBeenActivated = true;
            healSplashScreen.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void CurrentActiveSpell()
    {
        if(healsActive == true)
        {
            healSpellActiveText.SetActive(true);
            fireSpellActiveText.SetActive(false);
        }
        else if(fireActive == true)
        {
            healSpellActiveText.SetActive(false);
            fireSpellActiveText.SetActive(true);
        }
    }
}

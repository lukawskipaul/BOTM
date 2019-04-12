using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// this script component goes on to the Player

public class NewHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    GameObject damageSplashScreen;

    private bool splashScreenHasBeenActivated;


    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        damageSplashScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        SplashScreenOn();
        SplashScreenOff();
    }

    void LateUpdate()
    {
        UpdateHealthBar();
    }


    public void DamagePlayer(float amount)
    {
        currentHealth -= amount;
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            currentHealth = maxHealth;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Test();
            splashScreenHasBeenActivated = true;
            damageSplashScreen.gameObject.SetActive(true);
        }
    }


    IEnumerator Die()
    {
        Destroy(this.gameObject.GetComponent<Rigidbody>());

        yield return new WaitForSecondsRealtime(3.0f);

        currentHealth = maxHealth;
        //this.gameObject.transform.position = respawnPoint.position;
    }


    void SplashScreenOn()
    {
        if (splashScreenHasBeenActivated == true)
        {
            Test();

            damageSplashScreen.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
        }
    }

    void SplashScreenOff()
    {
        if (damageSplashScreen.GetComponent<CanvasGroup>().alpha <= 0 && splashScreenHasBeenActivated == true)
        {
            splashScreenHasBeenActivated = false;
        }
    }

    void Test() // Checks to see if the SplashScreen is active. If it isn't already active, then the DamageSlashScreen appears. 

    {
        if (splashScreenHasBeenActivated == false && damageSplashScreen.activeSelf == true)
        {
            splashScreenHasBeenActivated = true;
            damageSplashScreen.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This script will go on any object with a Trigger that will be able to damage the player
public class DamagePlayer : MonoBehaviour
{
    /*
    [SerializeField]
    GameObject damageSplashScreen;
    */
    [SerializeField]
    private float attackDamage = 15f;

    //private bool splashScreenHasBeenActivated;

    /*
    void Start()
    {
        damageSplashScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        SplashScreenOn();
        SplashScreenOff();
    }
    */

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //Test();
            //splashScreenHasBeenActivated = true;
            //damageSplashScreen.gameObject.SetActive(true); // Damage splash screen appears
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(attackDamage); // player takes damage
        }
    }

    /*
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
    */
}

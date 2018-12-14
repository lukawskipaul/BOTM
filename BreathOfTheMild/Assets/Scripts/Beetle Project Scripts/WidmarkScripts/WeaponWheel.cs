using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheel : MonoBehaviour
{
    [SerializeField]
    GameObject weaponWheelCanvas;
    [SerializeField]
    GameObject weaponSpawnPoint;
    //[SerializeField]
    //GameObject weaponOne;
    //[SerializeField]
    //GameObject weaponTwo;
    //[SerializeField]
    //GameObject weaponThree;

    bool isPressed;

    const string weaponWheelButtonName = "Weapon Wheel";
	
	void Start ()
    {
        //sets all weapons as false at Start
        //weaponOne.SetActive(false);
        //weaponTwo.SetActive(false);
        //weaponThree.SetActive(false);
        isPressed = false;
        weaponWheelCanvas.SetActive(false);
	}
	
	
	void Update ()
    {
        HandleWeaponWheel();
	}

    void FixedUpdate()
    {
        //TimeSlow();
    }

    void HandleWeaponWheel() //opens/closes the weapon wheel canvas
    {
        if (Input.GetButtonDown(weaponWheelButtonName))
        {
            isPressed = !isPressed;

            if (isPressed == true)
            {
                weaponWheelCanvas.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                weaponWheelCanvas.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    //public void SelectWeaponOne()
    //{
    //    //activates first weapon on wheel, deactivates any other weapons that might be activated.
    //    weaponTwo.SetActive(false);
    //    weaponThree.SetActive(false);
    //    weaponOne.SetActive(true);
    //    weaponOne.transform.position = weaponSpawnPoint.transform.position;
    //}

    //public void SelectWeaponTwo()
    //{
    //    //activates second weapon on wheel, deactivates any other weapons that might be activated.
    //    weaponOne.SetActive(false);
    //    weaponThree.SetActive(false);
    //    weaponTwo.SetActive(true);
    //    weaponTwo.transform.position = weaponSpawnPoint.transform.position;
    //}
    
    //public void SelectWeaponThree()
    //{
    //    //activates third weapon on wheel, deactivates any other weapons that might be activated.
    //    weaponOne.SetActive(false);
    //    weaponTwo.SetActive(false);
    //    weaponThree.SetActive(true);
    //    weaponThree.transform.position = weaponSpawnPoint.transform.position;
    //}

    //void TimeSlow() 
    //{
    //    // slows down world time while the weapon wheel is open (probably doesn't work with caleb's pause menu script active)
    //    if (isPressed == true)
    //    {
    //        Time.timeScale = 0.2f;
    //    }
    //    else
    //    {
    //        Time.timeScale = 1f;
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelekenisisCoolDownTest : MonoBehaviour
{
    [SerializeField]
    Slider coolDownUI;

    public bool coolDownHasStarted;

    // Start is called before the first frame update
    void Start()
    {
        coolDownUI.value = 100f;
        coolDownUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CoolDownTrigger();
        CoolDown();
        EndCoolDown();
    }

    void CoolDownTrigger()
    {
        if (Input.GetKey(KeyCode.G))
        {
            coolDownUI.gameObject.SetActive(true);
            coolDownHasStarted = true;
        }
    }

    void CoolDown()
    {
        if (coolDownHasStarted == true)
        {
            coolDownUI.value -= Time.deltaTime * 50;
        }
    }

    void EndCoolDown()
    {
        if (coolDownUI.value <= 0)
        {
            coolDownHasStarted = false;
            coolDownUI.value = 100f;
            coolDownUI.gameObject.SetActive(false);
        }
    }

    
}


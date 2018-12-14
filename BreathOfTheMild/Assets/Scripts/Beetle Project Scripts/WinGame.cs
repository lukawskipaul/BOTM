using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    [SerializeField]
    private Canvas endCanvas;

    private GameObject enemyObject;

    private void Start()
    {
        endCanvas.enabled = false;
        enemyObject = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {
        enemyObject = GameObject.FindGameObjectWithTag("Enemy");

        if (enemyObject == null)
        {
            endCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("End");
        }
    }
}

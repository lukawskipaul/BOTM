using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    [SerializeField]
    private RootMotionMovementController movement;
    [SerializeField]
    private Animator PlayerAnimator;
    [SerializeField]
    private ScreenFade ScreenFade;
    [SerializeField]
    private SlowMoGameTime slowMo;
    [SerializeField]
    private EnemyHealth bossHealth;
    [SerializeField]
    private GameObject endLight;
    [SerializeField]
    private string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        CheckEndGame();
    }

    private void CheckEndGame()
    {
        if (bossHealth.isDead)
        {
            endLight.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && bossHealth.isDead)
        {
            movement.DisableMovement(4f);
            PlayerAnimator.SetTrigger("TakeDamage");
            ScreenFade.BeginFade(1);
            slowMo.SlowMo();
            StartCoroutine(WaitForFade());
        }
    }

    private IEnumerator WaitForFade()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(sceneToLoad);
    }
}

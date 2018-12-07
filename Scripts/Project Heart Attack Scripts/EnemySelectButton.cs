using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;

    private EnemyStateMachine esm;

    private void Start()
    {
        esm = EnemyPrefab.GetComponent<EnemyStateMachine>();
    }

    public void SelectEnemy()
    {
        if (deathCheck())
        {
            GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().PlayerTargetInput(EnemyPrefab);   // save input of enemy prefab
        }
    }

    public void ShowSelector()
    {
        if (deathCheck())
        {
            EnemyPrefab.transform.Find("Selector").gameObject.SetActive(true);
        }
    }

    public void HideSelector()
    {
        EnemyPrefab.transform.Find("Selector").gameObject.SetActive(false);
    }

    private bool deathCheck()
    {
        return (esm.CurrentState != EnemyStateMachine.TurnState.DEAD);
    }
}

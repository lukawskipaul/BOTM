using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CombatMusicControl : MonoBehaviour
{
    public AudioMixerSnapshot outOfCombat;
    public AudioMixerSnapshot inCombat;
    public AudioMixerSnapshot bossCombat;
    //public AudioClip[] stings;
    //public AudioSource stingSource;
    public float bpm = 128;

    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;

    //public EnemyHealth enemyHealthReference;

    [SerializeField]
    private GameObject musicTriggerObject;

    private Collider musicTrigger;

    // Start is called before the first frame update
    void Start()
    {
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        m_TransitionOut = m_QuarterNote * 32;

        //musicTrigger = musicTriggerObject.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CombatZone1"))
        {
            inCombat.TransitionTo(m_TransitionIn);
        }
        else if (other.CompareTag("CombatZone2"))
        {
            inCombat.TransitionTo(m_TransitionIn);
        }
        else if (other.CompareTag("BossZone"))
        {
            bossCombat.TransitionTo(m_TransitionIn);
        }

    }

    void TransitionToOutOfCombat()
    {
        outOfCombat.TransitionTo(m_TransitionOut);
        Destroy(musicTriggerObject);

        //if (enemyHealthReference.isDead = true && CompareTag("CombatZone1"))
        //{
        //    Destroy(GameObject.Find("BattleMusicTrigger1"));
        //}
        //else if (enemyHealthReference.isDead = true && CompareTag("CombatZone2"))
        //{
        //    Destroy(GameObject.Find("BattleMusicTrigger2"));
        //}
        //else if (enemyHealthReference.isDead = true && CompareTag("BossZone"))
        //{
        //    Destroy(GameObject.Find("BossMusicTrigger"));
        //}
        
    }
    //void OnTriggerExit (Collider other)
    //{
    //    if (other.CompareTag("CombatZone"))
    //    {
    //        outOfCombat.TransitionTo(m_TransitionOut);
    //    }
    //}


    private void OnEnable()
    {
        EnemyHealth.EnemyDied += TransitionToOutOfCombat;       
    }
    private void OnDisable()
    {
        EnemyHealth.EnemyDied -= TransitionToOutOfCombat;
    }
}
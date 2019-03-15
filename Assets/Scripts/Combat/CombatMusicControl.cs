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

    // Start is called before the first frame update
    void Start()
    {
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        m_TransitionOut = m_QuarterNote * 32;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CombatZone"))
        {
            inCombat.TransitionTo(m_TransitionIn);
        }
        else if (other.CompareTag("BossZone"))
        {
            bossCombat.TransitionTo(m_TransitionIn);
        }
        
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("CombatZone"))
        {
            outOfCombat.TransitionTo(m_TransitionOut);
        }
    }
}

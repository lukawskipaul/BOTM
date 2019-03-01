using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKObject : MonoBehaviour
{
    Animator enemyAnim;

    enum State{Neutral, Levitating, Thrown};

    State currentState;

    private State CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CurrentState == State.Thrown || CurrentState == State.Levitating)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                enemyAnim = collision.gameObject.GetComponent<Animator>();
                enemyAnim.SetTrigger("Stun");
                Destroy(this.gameObject);
            }
            else
            {
                if (CurrentState == State.Thrown)
                {
                    currentState = State.Neutral;
                }            
            }
        }
        
    }

    public void SetNeutral()
    {
        CurrentState = State.Neutral;
    }

    public void SetLevitating()
    {
        CurrentState = State.Levitating;
    }

    public void SetThrown()
    {
        CurrentState = State.Thrown;
    }
}

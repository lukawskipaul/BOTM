using UnityEngine;

public class Target : MonoBehaviour
{

    public float objHealth = 50f;
    
    public void TakeDamage(float amount)
    {
        objHealth -= amount;
        if(objHealth <= 0f)
        {
            BreakObj();
        }
    }

    void BreakObj()
    {
        Destroy(gameObject);
    }

}

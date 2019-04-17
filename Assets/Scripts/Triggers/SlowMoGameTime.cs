using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoGameTime : MonoBehaviour
{
    [SerializeField]
    private float scaleToSlowTo;
    [SerializeField]
    private float lerpScale;
    [SerializeField]
    private float timeDuringFullSlow;
    
    public void SlowMo()
    {
        StartCoroutine("StartSlowMo");
    }

    private IEnumerator StartSlowMo()
    {
        while(Time.timeScale > scaleToSlowTo)
        {
            Mathf.Lerp(1, scaleToSlowTo, Time.timeScale);
            Time.timeScale -= Time.deltaTime * lerpScale;
            Debug.Log(Time.timeScale);
            yield return null;
        }
        Time.timeScale = scaleToSlowTo;
        yield return new WaitForSecondsRealtime(timeDuringFullSlow);
        while(Time.timeScale < 1)
        {
            Mathf.Lerp(scaleToSlowTo, 1, Time.timeScale);
            Time.timeScale += Time.deltaTime * 2 * lerpScale;
            yield return null;
        }
        Time.timeScale = 1;
    }
}

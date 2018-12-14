using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    static MenuMusic instance = null;

	void Awake()
	{
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void StopMusic()
    {
        Destroy(this.gameObject);
    }
}
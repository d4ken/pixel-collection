using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    Debug.Log(typeof(T) + " is nothing.");
                }
            }
            return instance;
        }
    }

    protected void Awake()
    {
        CheckInstance();
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (T)this;
            return true;
        } else if (Instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }
}
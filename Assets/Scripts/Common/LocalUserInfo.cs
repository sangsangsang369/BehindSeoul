using UnityEngine;

public class LocalUserInfo : MonoBehaviour
{
    private static LocalUserInfo instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static LocalUserInfo Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public string lc_nickname;
    public string lc_email;
    public string lc_password;
}

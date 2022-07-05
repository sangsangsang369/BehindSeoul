using UnityEngine;

public class LocalUserInfo : MonoBehaviour
{
    //로컬 유저 정보
    //씬 바뀌어도 값 유지하도록 

    //앱 내에서 LocalUserInfo 인스턴스는 이 instance에 담긴 녀석만 존재하게 할 것
    private static LocalUserInfo instance = null;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 
            //전역변수 instance에 LocalUserInfo 인스턴스가 담겨있지 않다면, 자신을 넣어줌
            instance = this;
            //씬 전환이 되더라도 파괴되지 않게 함
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //LocalUserInfo 인스턴스에 접근할 수 있는 프로퍼티
    //static이므로 다른 클래스에서 맘껏 호출할 수 있음
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

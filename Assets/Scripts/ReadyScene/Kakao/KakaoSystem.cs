using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KakaoSystem : MonoBehaviour
{
    

    void awake()
    {
        
    }

    public void login()
    {
        AndroidJavaObject ajo = new AndroidJavaObject( "com.DefaultCompany.BehindSeoul.UKakao" );
        ajo.Call( "KakaoLogin" );
    }
}

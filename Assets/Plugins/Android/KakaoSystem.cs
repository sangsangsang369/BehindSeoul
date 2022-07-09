using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KakaoSystem : MonoBehaviour
{
    private AndroidJavaObject ajo;

    void Start()
    {
        ajo = new AndroidJavaObject( "com.DefaultCompany.BehindSeoul.UKakao" );
    }

    public void login()
    {
        ajo.Call( "KakaoLogin" );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Auth;


public class MyPageManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text myPage_Nickname;
    [SerializeField]
    GameObject mainPage, myPage;

    public void MyPageBtnFunc()
    {
        if(myPage_Nickname.text != LocalUserInfo.Instance.lc_nickname)
        {
            Debug.Log(LocalUserInfo.Instance.lc_nickname);
            myPage_Nickname.text = LocalUserInfo.Instance.lc_nickname;
        }
        mainPage.SetActive(false);
        myPage.SetActive(true);
    }

    public void LogoutBtnFunc()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene("ReadyScene", LoadSceneMode.Single); 
    }
}

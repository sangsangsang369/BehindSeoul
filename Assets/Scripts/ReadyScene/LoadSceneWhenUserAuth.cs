using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class LoadSceneWhenUserAuth : MonoBehaviour
{
    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateCahnged;
        CheckUser();
    }

    void OnDestroy() 
    {
        FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateCahnged;    
    }

    void HandleAuthStateCahnged(object sender, EventArgs e)
    {
        CheckUser();
    }

    void CheckUser()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null && SceneManager.GetActiveScene().name == "ReadyScene")
        {
            SceneManager.LoadScene("MainScene");
        }
        /*else if(FirebaseAuth.DefaultInstance.CurrentUser == null && SceneManager.GetActiveScene().name == "MainScene")
        {
            SceneManager.LoadScene("ReadyScene"); 
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Firestore;

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
            GetUserInfoFromDB(FirebaseAuth.DefaultInstance.CurrentUser.UserId);
        }
        else if(FirebaseAuth.DefaultInstance.CurrentUser != null && SceneManager.GetActiveScene().name == "MainScene")
        {
            GetUserInfoFromDB(FirebaseAuth.DefaultInstance.CurrentUser.UserId);
        }
    }

    async void GetUserInfoFromDB(string userUIDValue) 
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("Users").Document(userUIDValue);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            Debug.LogFormat("Document data for {0} document:", snapshot.Id);
            Dictionary<string, object> UserInfo = snapshot.ToDictionary();
            foreach (KeyValuePair<string, object> pair in UserInfo)
            {
                if(pair.Key == "name")
                {
                    LocalUserInfo.Instance.lc_nickname = pair.Value.ToString();
                }
                if(pair.Key == "email")
                {
                    LocalUserInfo.Instance.lc_email = pair.Value.ToString();
                }
                if(pair.Key == "password")
                {
                    LocalUserInfo.Instance.lc_password = pair.Value.ToString();
                }
            }
        }
        else
        {
            Debug.LogFormat("Document {0} does not exist!", snapshot.Id);
        }
    }
}

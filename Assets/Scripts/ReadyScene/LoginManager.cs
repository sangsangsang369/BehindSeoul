using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    GameObject lg_emailInput, lg_PWInput;
    string lg_emailValue, lg_PWValue;
    FirebaseAuth auth;

    void Awake()
    {
        // 초기화
        auth = FirebaseAuth.DefaultInstance;
    }

    public void InitLoginInputs()
    {
        //뒤로가기 했을 때 값 초기화
        lg_emailInput.GetComponent<TMP_InputField>().text = null;
        lg_PWInput.GetComponent<TMP_InputField>().text = null;
    }

    public void LoginSubmitBtnFunc()
    {
        lg_emailValue = lg_emailInput.GetComponent<TMP_InputField>().text;
        lg_PWValue = lg_PWInput.GetComponent<TMP_InputField>().text;
 
        Debug.Log("email: " + lg_emailValue + ", password: " + lg_PWValue);
 
        LoginUser();
        SceneManager.LoadScene("MainScene");
    }

    void LoginUser()
    {
        auth.SignInWithEmailAndPasswordAsync(lg_emailValue, lg_PWValue).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                //loginResult.text = "로그인 실패";                
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                //loginResult.text = "로그인 실패";               
                return;
            }
 
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            GetUserInfoFromDB(newUser.UserId);
        });
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

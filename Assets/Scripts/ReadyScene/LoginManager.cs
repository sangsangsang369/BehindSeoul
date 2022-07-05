using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;

public class LoginManager : MonoBehaviour
{
    //로그인 제어

    [SerializeField]
    GameObject lg_emailInput, lg_PWInput;
    string lg_emailValue, lg_PWValue;
    FirebaseAuth auth;

    void Awake()
    {
        // 초기화
        auth = FirebaseAuth.DefaultInstance;
    }

    void Start()
    {
        //auth.StateChanged += HandleAuthStateCahnged;
        CheckUser();
    }
    
    /* 
    //계정 로그인에 어떠한 변경점이 발생시 진입
    void HandleAuthStateCahnged(object sender, EventArgs e)
    {
        CheckUser();
    }

    //오브젝트가 삭제되기 직전에 실행하는 함수
    void OnDestroy() 
    {
        //유저의 로그인 정보에 변경점이 생기면 실행되게 이벤트를 걸어줌
        FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateCahnged;    
    }*/

    //로그인 되어있는지 체크하는 함수
    void CheckUser()
    {
        //로그인한 유저가 있고 && 씬이 레디씬일 때
        if(FirebaseAuth.DefaultInstance.CurrentUser != null && SceneManager.GetActiveScene().name == "ReadyScene")
        {
            //메인씬으로
            SceneManager.LoadScene("MainScene");
            //db에서 로그인된 사용자 정보 갖고오기
            GetUserInfoFromDB(FirebaseAuth.DefaultInstance.CurrentUser.UserId);
        }
        /*
        //로그인한 유저 있고 && 씬이 메인씬일 때
        else if(FirebaseAuth.DefaultInstance.CurrentUser != null && SceneManager.GetActiveScene().name == "MainScene")
        {
            GetUserInfoFromDB(FirebaseAuth.DefaultInstance.CurrentUser.UserId);
        }*/
    }

    //뒤로가기 했을 때 값 초기화
    public void InitLoginInputs()
    { 
        lg_emailInput.GetComponent<TMP_InputField>().text = null;
        lg_PWInput.GetComponent<TMP_InputField>().text = null;
    }
    //이메일 비번 입력 제출 버튼
    public void LoginSubmitBtnFunc()
    {
        lg_emailValue = lg_emailInput.GetComponent<TMP_InputField>().text;
        lg_PWValue = lg_PWInput.GetComponent<TMP_InputField>().text;

        if(lg_emailValue.Length < 1 || lg_PWValue.Length < 1)
        {
            //입력 없음
        }
        else
        {
            Debug.Log("email: " + lg_emailValue + ", password: " + lg_PWValue);
            LoginUser();
        }
    }

    //로그인(인증)
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

            SceneManager.LoadScene("MainScene");
        });
    }

    //db에서 정보 갖고오기
    async void GetUserInfoFromDB(string userUIDValue) 
    {
        //초기화
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        //firestore의 콜렉션 "Users"에서 도큐먼트로 저장된 유저 UID reference 갖고옴
        DocumentReference docRef = db.Collection("Users").Document(userUIDValue);
        //Snapshot은 비동기로 실제 서버 데이터를 가져온 내용물
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        //스냅샷이 존재하면
        if (snapshot.Exists)
        {
            Debug.LogFormat("Document data for {0} document:", snapshot.Id);
            //db에서 가저온 데이터를 딕셔너리형으로 
            Dictionary<string, object> UserInfo = snapshot.ToDictionary();
            foreach (KeyValuePair<string, object> pair in UserInfo)
            {
                //로컬에 데이터 저장
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
        //스냅샷 존재 안하면
        else
        {
            Debug.LogFormat("Document {0} does not exist!", snapshot.Id);
        }
    }
}

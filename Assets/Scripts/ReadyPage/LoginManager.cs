using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField  emailInput,
                    passwordInput,
                    prevPasswordInput,
                    newPasswordInput;
    [SerializeField]
    TMP_Text    emailErrorText,
                passwordErrorText,
                settingPageEmailText,
                newPasswordErrorText;
    [SerializeField]
    GameObject  loginPage,
                readyPage,
                mainPage,
                settingPage,
                passwordChangePage;
    public Dictionary<string, object> user_Info;
    protected Firebase.Auth.FirebaseAuth auth;
    protected Firebase.Firestore.FirebaseFirestore db;

    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;
        CheckUser();
    }

    public void LoginBtnFunc()
    {
        emailErrorText.enabled = false;
        passwordErrorText.enabled = false;
        SigninWithEmailAsync();
    }
    
     //로그인 되어있는지 체크하는 함수
    void CheckUser()
    {
        //로그인한 유저가 있고 && 씬이 레디씬일 때
        if(FirebaseAuth.DefaultInstance.CurrentUser != null && readyPage.activeSelf == true)
        {
            //메인페이지로
            loginPage.SetActive(false);
            readyPage.SetActive(false);
            mainPage.SetActive(true);
            //db에서 로그인된 사용자 정보 갖고오기
            GetUserInfoFromDB(FirebaseAuth.DefaultInstance.CurrentUser.Email);
        }
    }

    //로그인 함수
    public Task SigninWithEmailAsync()
    {
        var email = emailInput.text;
        var password = passwordInput.text;
        Debug.LogWarning(String.Format("Attempting to sign in as {0}...", email));
        
        return auth.SignInAndRetrieveDataWithCredentialAsync(
                Firebase.Auth.EmailAuthProvider.GetCredential(email, password))
                    .ContinueWithOnMainThread(HandleSignInWithSignInResult);
    }

    // when a sign-in with profile data completes 할 때 호출
    void HandleSignInWithSignInResult(Task<Firebase.Auth.SignInResult> task)
    {
        if (LogTaskCompletion(task, "Sign-in"))
        {
            DisplaySignInResult(task.Result, 1);
        }
    }

    // 유저 정보 display 하기
    protected void DisplaySignInResult(Firebase.Auth.SignInResult result, int indentLevel)
    {
        string indent = new String(' ', indentLevel * 2);

        var metadata = result.Meta;
        if (metadata != null)
        {
            Debug.LogWarning(String.Format("{0}Created: {1}", indent, metadata.CreationTimestamp));
            Debug.LogWarning(String.Format("{0}Last Sign-in: {1}", indent, metadata.LastSignInTimestamp));
        }

        var info = result.Info;
        if (info != null)
        {
            Debug.LogWarning(String.Format("{0}Additional User Info:", indent));
            Debug.LogWarning(String.Format("{0}  User Name: {1}", indent, info.UserName));
            Debug.LogWarning(String.Format("{0}  User ID: {1}", indent, info.ProviderId));
            DisplayProfile<string>(info.Profile, indentLevel + 1);
        }
    }

    // 추가적인 유저 프로필 정보 display 하기
    protected void DisplayProfile<T>(IDictionary<T, object> profile, int indentLevel)
    {
        string indent = new String(' ', indentLevel * 2);
        foreach (var kv in profile)
        {
            var valueDictionary = kv.Value as IDictionary<object, object>;
            if (valueDictionary != null)
            {
                Debug.Log(String.Format("{0}{1}:", indent, kv.Key));
                DisplayProfile<object>(valueDictionary, indentLevel + 1);
            }
            else
            {
                Debug.Log(String.Format("{0}{1}: {2}", indent, kv.Key, kv.Value));
            }
        }
    }

    // 특정 테스크의 결과에 대한 로그 찍기 
    // 테스크 잘 실행되면 true 리턴하고, 안되면 false 리턴
    protected bool LogTaskCompletion(Task task, string operation)
    {
        bool complete = false;
        if (task.IsCanceled)
        {
            Debug.LogWarning(operation + " canceled.");
        }
        else if (task.IsFaulted)
        {
            Debug.LogWarning(operation + " encounted an error.");
            foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
            {
                string authErrorCode = "";
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    authErrorCode = String.Format("AuthError.{0}: ",
                        ((Firebase.Auth.AuthError)firebaseEx.ErrorCode).ToString());
                    GetErrorMessage((Firebase.Auth.AuthError)firebaseEx.ErrorCode);
                }
                Debug.Log(authErrorCode + exception.ToString());
            }
        }
        else if (task.IsCompleted)
        {
            Debug.LogWarning(operation + " completed");
            
            GetUserInfoFromDB(emailInput.text);
            InputSetActiveInit();

            complete = true;
        }
        return complete;
    }

    private void GetErrorMessage(AuthError errorCode)
    {
        switch (errorCode)
        {
            case AuthError.InvalidEmail:
                emailErrorText.text = "유효하지 않은 이메일 입니다.";
                emailErrorText.enabled = true;
                break;
            case AuthError.MissingEmail:
                emailErrorText.text = "이메일을 입력해주세요.";
                emailErrorText.enabled = true;
                break;
            case AuthError.MissingPassword:
                passwordErrorText.text = "비밀번호를 입력해주세요.";
                passwordErrorText.enabled = true;
                break;
            case AuthError.WrongPassword:
                passwordErrorText.text = "비밀번호를 확인해 주세요";
                passwordErrorText.enabled = true;
                break;
            case AuthError.UserNotFound:
                emailErrorText.text = "계정이 존재하지 않습니다.";
                emailErrorText.enabled = true;
                break;
            default:
                emailErrorText.text = "알 수 없는 에러가 발생했습니다.";
                emailErrorText.enabled = true;
                break;
        }
    }

    //db에서 정보 갖고오기
    public async void GetUserInfoFromDB(string email) 
    {
        DocumentReference docRef = db.Collection("Users").Document(email);
        //Snapshot은 비동기로 실제 서버 데이터를 가져온 내용물
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        //스냅샷이 존재하면
        if (snapshot.Exists)
        {
            Debug.LogFormat("Document data for {0} document:", snapshot.Id);
            settingPageEmailText.text = email;
            //db에서 가저온 데이터를 딕셔너리형으로 
            user_Info = snapshot.ToDictionary();
            foreach(KeyValuePair<string, object>pair in user_Info)
            {
                Debug.LogError(pair.Key + " : " + pair.Value);
            }
        }
        else
        {
            Debug.LogFormat("Document {0} does not exist!", snapshot.Id);
        }
    }

    void InputSetActiveInit()
    {
        readyPage.SetActive(false);
        loginPage.SetActive(false);
        mainPage.SetActive(true);
        emailInput.text = "";
        passwordInput.text = "";
    }

    public void ChangePasswordBtnFunc()
    {
        newPasswordErrorText.text = "";
        object userPW = user_Info.TryGetValue("password", out object pw);
        if(prevPasswordInput.text != pw.ToString())
        {
            Debug.LogError(pw.ToString());
            newPasswordErrorText.text = "현재 비밀번호를 다시 입력해주세요.";
        }
        else if(newPasswordInput.text.Length < 6)
        {
            newPasswordErrorText.text = "비밀번호 길이가 너무 짧습니다";
        }
        else
        {
            ChangePassword_auth(newPasswordInput.text);
            ChangePassword_db(newPasswordInput.text);
            passwordChangePage.SetActive(false);
            prevPasswordInput.text = "";
            newPasswordInput.text = "";
        }
        
    }   

    void ChangePassword_auth(string newPW)
    {
        //auth 쪽 비밀번호 변경
        Firebase.Auth.FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        string newPassword = newPW;
        if (user != null) 
        {
            user.UpdatePasswordAsync(newPassword).ContinueWith(task => {
                if (task.IsCanceled) 
                {
                    Debug.LogError("UpdatePasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) 
                {
                    Debug.LogError("UpdatePasswordAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.LogError("Password updated successfully.");
            });
        }
    }

    async void ChangePassword_db(string newPW)
    {
        //db 쪽 비밀번호 변경
        DocumentReference docRef = db.Collection("Users").Document(auth.CurrentUser.Email);
        Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { "password", newPW }
        };
        await docRef.UpdateAsync(updates);
    }

    public void LogOutBtnFunc()
    {
        auth.SignOut();
        mainPage.SetActive(false);
        settingPage.SetActive(false);
        readyPage.SetActive(true);
    }
}

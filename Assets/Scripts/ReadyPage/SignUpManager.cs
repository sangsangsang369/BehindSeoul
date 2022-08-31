using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using Firebase.Firestore;

public class SignUpManager : MonoBehaviour
{ 
    [SerializeField]
    TMP_InputField  emailInput,
                    passwordInput,
                    rePasswordInput;
    [SerializeField]
    TMP_Text    emailErrorText,
                passwordErrorText;
    [SerializeField]
    GameObject  signUpPage,
                readyPage,
                mainPage;
    protected Firebase.Auth.FirebaseAuth auth;
    LoginManager loginMng;

    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        loginMng = FindObjectOfType<LoginManager>();
    }

    public void SignUpBtnFunc()
    {
        emailErrorText.enabled = false;
        passwordErrorText.enabled = false;
        if (passwordInput.text != rePasswordInput.text) 
        {
            passwordErrorText.text = "비밀번호가 일치하지 않습니다.";
            passwordErrorText.enabled = true;
        }
        else {
            CreateUserWithEmailAsync();
        }
    }

    //유저 생성 함수
    public Task CreateUserWithEmailAsync() 
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        Debug.Log(String.Format("Attempting to create user {0}...", email));

        return auth.CreateUserWithEmailAndPasswordAsync(email, password)
         .ContinueWithOnMainThread((task) => {
            LogTaskCompletion(task, "User Creation");
            return task;
        }).Unwrap();
    }

    // Log the result of the specified task, returning true if the task
    // completed successfully, false otherwise.
    protected bool LogTaskCompletion(Task task, string operation) 
    {
        bool complete = false;
        if (task.IsCanceled) 
        {
             Debug.LogError(operation + " canceled.");
        } 
        else if (task.IsFaulted) 
        {
            Debug.LogError(operation + " encounted an error.");
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
            Debug.LogError(operation + " completed");
            
            CreateUserInfoInDB();
            InputSetActiveInit();

            complete = true;
        }
        return complete;
    }
    
    //비밀번호는 6자리 이상
    private void GetErrorMessage(AuthError errorCode)
    {
        switch (errorCode)
        {
            case AuthError.MissingEmail:
                emailErrorText.text = "이메일을 입력해주세요.";
                emailErrorText.enabled = true;
                break;
            case AuthError.InvalidEmail:
                emailErrorText.text = "유효하지 않은 이메일 입니다.";
                emailErrorText.enabled = true;
                break;           
            case AuthError.EmailAlreadyInUse:
                emailErrorText.text = "이미 사용중인 이메일 입니다.";
                emailErrorText.enabled = true;
                break;
            case AuthError.UserNotFound:
                emailErrorText.text = "Account not found.";
                emailErrorText.enabled = true;
                break;
           
            case AuthError.MissingPassword:
                passwordErrorText.text = "비밀번호를 입력해주세요.";
                passwordErrorText.enabled = true;
                break;
            case AuthError.WeakPassword:
                passwordErrorText.text = "비밀번호가 너무 단순합니다.";
                passwordErrorText.enabled = true;
                break;
            default:
                emailErrorText.text = "알 수 없는 에러가 발생했습니다.";
                emailErrorText.enabled = true;
                break;
        }
    }

    void CreateUserInfoInDB()
    {
        var userInfo = new UserInfo
        {
            password = passwordInput.text
        };

        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Collection("Users").Document(emailInput.text).SetAsync(userInfo);
        loginMng.GetUserInfoFromDB(emailInput.text);
    }

    void InputSetActiveInit()
    {
        signUpPage.SetActive(false);
        readyPage.SetActive(false);
        mainPage.SetActive(true);
        emailInput.text = "";
        passwordInput.text = "";
        rePasswordInput.text = "";
    }
}

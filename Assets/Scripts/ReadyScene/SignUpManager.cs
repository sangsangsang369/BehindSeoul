using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;

public class SignUpManager : MonoBehaviour
{
    //회원가입 제어

    [SerializeField]
    GameObject enter, 
               Login_Email_PW,
               signUp1_Email, 
               signUp2_Password, 
               signUp3_Nickname, 
               signUp4_GetStarted;
    [SerializeField]
    GameObject emailInput, 
               PWInput, 
               re_PWInput, 
               nicknameInput;
    public string emailValue, 
                  PWValue, 
                  re_PWValue, 
                  nicknameValue;
    [SerializeField]
    TMP_Text getStarted_insertName;
    FirebaseAuth auth;

    void Awake()
    {
        // 초기화
        auth = FirebaseAuth.DefaultInstance;
    }

    public void InitInputs()
    {
        //뒤로가기 했을 때 값 초기화
    }

    //회원가입하기 버튼 함수
    public void SignUpBtnFunc()
    {
        enter.SetActive(false);
        signUp1_Email.SetActive(true);
    }
    //로그인하기 버튼 함수
    public void LoginBtnFunc()
    {
        enter.SetActive(false);
        Login_Email_PW.SetActive(true);
    }
    //이메일 입력 제출 버튼
    public void EmailSubmitBtnFunc()
    {
        emailValue = emailInput.GetComponent<TMP_InputField>().text;
        if(emailValue.Length < 1)
        {
            //이메일이 비어있음
        }
        else
        {
            signUp1_Email.SetActive(false);
            signUp2_Password.SetActive(true);
        }
    }
    //비밀번호 입력 제출 버튼
    public void PWSubmitBtnFunc()
    {
        PWValue = PWInput.GetComponent<TMP_InputField>().text;
        re_PWValue = re_PWInput.GetComponent<TMP_InputField>().text;
        if(PWValue.Length < 5)
        {
            //비밀번호 6자리 이상 입력바람
        }
        else
        {
            if(PWValue != re_PWValue)                                                                                     
            {
                //비밀번호 잘 못 재입력 했을 때
            }
            else if(PWValue == re_PWValue)
            {
                signUp2_Password.SetActive(false);
                signUp3_Nickname.SetActive(true);
            }
        }
    }
    //닉네임 입력 제출 버튼
    public void NicknameSubmitBtnFunc()
    {
        nicknameValue = nicknameInput.GetComponent<TMP_InputField>().text;
        getStarted_insertName.text = getStarted_insertName.text.Replace("name", nicknameValue);
        signUp3_Nickname.SetActive(false);
        signUp4_GetStarted.SetActive(true);

        CreateUser();
    }
    //시작하기 버튼
    public void GetStartedBtnFunc()
    {
        SceneManager.LoadScene("MainScene");
    }  
    //유저 생성 함수
    void CreateUser()                                                                         
    {                                                                                           
        auth.CreateUserWithEmailAndPasswordAsync(emailValue, PWValue).ContinueWith(task => {  
            if (task.IsCanceled)                                                               
            {                                                                                     
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
 
            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            InsertUserInfoInDB(newUser.UserId);
        });
    }
    //db에도 유저 생성
    void InsertUserInfoInDB(string userUIDValue)
    {
        var userInfo = new UserInfo
        {
            name = nicknameValue,
            email = emailValue,
            password = PWValue
        };
        SetLocalUserInfo();

        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Collection("Users").Document(userUIDValue).SetAsync(userInfo);
    }
    //로컬에 유저 정보 저장
    void SetLocalUserInfo()
    {
        LocalUserInfo.Instance.lc_nickname = nicknameValue;
        LocalUserInfo.Instance.lc_email = emailValue;
        LocalUserInfo.Instance.lc_password = PWValue;
    }
}

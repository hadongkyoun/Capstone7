using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class AuthManager
{
    private static AuthManager instance = null;

    public static AuthManager Instance
    { get 
       
        {
            if(instance == null)
            {
                instance = new AuthManager();
            }
            
            
            return instance; 
        } 
    
    }

    public bool IsFirebaseReady {  get; private set; }
    public bool IsLogInOnProgress { get; private set; }
    public bool IsCreateOnProgress { get; private set; }
    public bool IsLogOutOnProgress { get; private set; }




    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;

    public static FirebaseUser User;

    public void Init()
    {

        // Firebase가 구동 가능 한 상태인지 판별 ( 오류 방지 )
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var result = task.Result;

            // 구동 가능 한 상태가 아니다
            if (result != DependencyStatus.Available)
            {
                Debug.LogError(result.ToString());
                IsFirebaseReady = false;
            }
            else
            {
                Debug.Log("Firebase Ready");
                IsFirebaseReady = true;

                //앱 할당
                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance; // firbaseAuth = FirebaseAuth.GetAuth(firebaseApp);

            
            }

        });


    }

    public FirebaseUser GetUser() {
        return User;
    }

    public void Create(string email, string password)
    {
        if (!IsFirebaseReady || IsCreateOnProgress)
        {
            return;
        }

        IsCreateOnProgress = true;

        firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            Debug.Log($"Create Account status : {task.Status}");

            IsCreateOnProgress = false;

            if (task.IsFaulted)
            {
                //회원가입 실패 => 이메일이 비정상 / 비밀번호가 간단 / 등등
                Debug.LogError("회원가입 실패");
                return;
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("회원가입 취소");
                return;
            }
            else
            {
                User = task.Result.User;
                Debug.Log("회원가입 완료 : " + User.Email);
            }
        });
    }
    public void LogIn(string email, string password)
    {
        if (!IsFirebaseReady || IsLogInOnProgress)
        {
            return;
        }

        IsLogInOnProgress = true;

        firebaseAuth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            Debug.Log($"Log-in status : {task.Status}");

            IsLogInOnProgress = false;

            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("Log-in canceled");
            }
            else
            {
                //로그인
                User = task.Result.User;
                Debug.Log(User.Email);
                SceneManager.LoadScene("Lobby");
            }
        });
    }
    public void LogOut()
    {
        if (!IsFirebaseReady || IsLogOutOnProgress || User == null)
        {
            return;
        }

        IsLogOutOnProgress = true;

        Debug.Log(User.Email + "로그아웃");
        firebaseAuth.SignOut();
        IsLogOutOnProgress = false;
        SceneManager.LoadScene("SignIn");
    }
}

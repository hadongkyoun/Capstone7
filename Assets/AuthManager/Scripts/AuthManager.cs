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

        // Firebase�� ���� ���� �� �������� �Ǻ� ( ���� ���� )
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var result = task.Result;

            // ���� ���� �� ���°� �ƴϴ�
            if (result != DependencyStatus.Available)
            {
                Debug.LogError(result.ToString());
                IsFirebaseReady = false;
            }
            else
            {
                Debug.Log("Firebase Ready");
                IsFirebaseReady = true;

                //�� �Ҵ�
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
                //ȸ������ ���� => �̸����� ������ / ��й�ȣ�� ���� / ���
                Debug.LogError("ȸ������ ����");
                return;
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("ȸ������ ���");
                return;
            }
            else
            {
                User = task.Result.User;
                Debug.Log("ȸ������ �Ϸ� : " + User.Email);
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
                //�α���
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

        Debug.Log(User.Email + "�α׾ƿ�");
        firebaseAuth.SignOut();
        IsLogOutOnProgress = false;
        SceneManager.LoadScene("SignIn");
    }
}

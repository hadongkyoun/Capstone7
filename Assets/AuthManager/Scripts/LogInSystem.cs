using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInSystem : MonoBehaviour
{

    public InputField emailField;
    public InputField passwordField;

    public Text outputText;
    void Start()
    {
        // ΩÃ±€≈Ê  
        AuthManager.Instance.Init();
    }

    public void Create()
    {
        string e = emailField.text;
        string p = passwordField.text;

        AuthManager.Instance.Create(emailField.text, passwordField.text);
    }

    public void LogIn()
    {
        AuthManager.Instance.LogIn(emailField.text, passwordField.text);
    }
    
    public void LogOut()
    {
        AuthManager.Instance.LogOut();
    }
}

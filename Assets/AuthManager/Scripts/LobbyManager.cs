using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public 

    void Start()
    {
        // �̱���  
        AuthManager.Instance.Init();

    }

    public void Session()
    {

    }

    public void LogOut()
    {
        AuthManager.Instance.LogOut();
    }

}

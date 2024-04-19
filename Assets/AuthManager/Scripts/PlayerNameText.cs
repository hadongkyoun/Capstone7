using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameText : MonoBehaviour
{
    private Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        nameText = GetComponent<Text>();

        if(AuthManager.Instance.GetUser() != null) 
        {
            nameText.text = $"WelCome!! {AuthManager.Instance.GetUser().Email}";
        }
        else
        {
            nameText.text = "Error : None User";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

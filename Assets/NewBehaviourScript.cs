using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject xBot;
    public GameObject init;
    void Start()
    {
        xBot.transform.position = new Vector3(init.transform.position.x, init.transform.position.y,
            init.transform.position.z);
        
    }

    
    void Update()
    {
        Debug.Log(xBot.transform.position);
    }
}

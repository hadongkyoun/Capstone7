using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haha : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject xb;
    void Start()
    {
        this.transform.position = new Vector3(xb.transform.position.x, xb.transform.position.y,
            xb.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

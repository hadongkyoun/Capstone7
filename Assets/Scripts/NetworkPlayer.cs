using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    [Networked]public int totalScore { get; set; }
    [Networked]public NetworkBool Ready { get; set; }

    public override void Spawned()
    {
        NetworkManager.instance.AddPlayer(Runner.LocalPlayer, this);
        base.Spawned();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

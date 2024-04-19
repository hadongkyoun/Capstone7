using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Fusion.Sockets;
using System;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField]
    private GameObject networkRunnerPrefab;
    [SerializeField] private NetworkObject playerPrefab;

    private Dictionary<PlayerRef, NetworkPlayer> NetworkPlayers = new ();

    public NetworkRunner runner;
    public UnityEvent OnConnectionStart;
    public UnityEvent OnConnectionSuccessful;

    //싱글톤 => 씬 전환 시 인스턴스가 중복 되는 것을 방지, 메모리 부담 down
    public static NetworkManager instance { get; private set; }
    

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 중복 방지
            Destroy(gameObject);
        }

        CreateNetworkRunner();
    }

    private void Start()
    {
        ConnectGame();
    }

    private void CreateNetworkRunner()
    {
        if (!runner)
        {
            runner = Instantiate(networkRunnerPrefab).GetComponent<NetworkRunner>();
        }
        runner.AddCallbacks(this);
    }

    public async void ConnectGame()
    {
        OnConnectionStart.Invoke();
        var args = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "Test",
            Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
            //다른 정보들
        };

        var connectionResult = await runner.StartGame(args);
        if (connectionResult.Ok)
        {
            Debug.Log("StartGame successful");
            OnConnectionSuccessful.Invoke();
        }
        else
        {
            Debug.LogError(connectionResult.ErrorMessage);
        }
    }
    public void AddPlayer(PlayerRef player, NetworkPlayer networkPlayer)
    {
        NetworkPlayers[player] = networkPlayer;
        networkPlayer.transform.SetParent(runner.transform);
    }

    public NetworkPlayer GetPlayer(PlayerRef player = default)
    {
        if (!runner) return null;
        if (player == default) player = runner.LocalPlayer;

        NetworkPlayers.TryGetValue(player, out NetworkPlayer networkPlayer);
        return networkPlayer;
    }

    public void RemovePlayer(PlayerRef player)
    {
        if (NetworkPlayers.ContainsKey(player))
        {
            NetworkPlayers.Remove(player);
        }
        else
        {
            Debug.LogWarning("This Player : " + player + " not found.");
        }
    }

    private void SpawnPlayer(NetworkRunner runner, PlayerRef player)
    {
        if (player == runner.LocalPlayer)
        {
            runner.Spawn(playerPrefab, transform.position, transform.rotation, player);
        }
    }

    #region NetworkRunnerCallbacks

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("NewPlayer Joined" + player);
        SpawnPlayer(runner, player);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }
    #endregion
}

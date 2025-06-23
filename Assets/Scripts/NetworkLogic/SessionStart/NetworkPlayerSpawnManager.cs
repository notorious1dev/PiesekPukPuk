using Fusion;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.Unicode;

public sealed class NetworkPlayerSpawnManager : SimulationBehaviour, IPlayerJoined
{
    [SerializeField]
    private NetworkPrefabRef _playerPrefab;

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            NetworkObject playerObj = Runner.Spawn(_playerPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0), player);
            Runner.SetPlayerObject(player, playerObj);
        }
    }
    
    public void RespawnLocalPlayer()
    {
        PlayerRef playerRef = Runner.LocalPlayer;
        NetworkObject playerObj = Runner.GetPlayerObject(playerRef);

        if (playerObj == null)
        {
            Debug.LogWarning("Player object is null, cannot move");
            return;
        }

        HealthComponent health = playerObj.GetComponent<HealthComponent>();
        health.isDead = false;
        
        playerObj.transform.position = Vector3.zero;
    } 
}

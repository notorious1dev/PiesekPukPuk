using Fusion;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.Unicode;

public sealed class NetworkPlayerSpawnManager : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField]
    private NetworkPrefabRef _playerPrefab;
    public readonly Dictionary<PlayerRef, NetworkObject> players = new();

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        if (!Runner.IsServer) return;

        NetworkObject playerObject = Runner.Spawn(_playerPrefab, Vector3.zero, Quaternion.Euler(0, 0, -90), player);
        players.Add(player, playerObject);
    }

    void IPlayerLeft.PlayerLeft(PlayerRef player)
    {
        if (!Runner.IsServer) return;

        if (players.Remove(player, out NetworkObject playerObject))
            Runner.Despawn(playerObject);
    }
}

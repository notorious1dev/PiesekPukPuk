using Fusion;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.Unicode;

public sealed class NetworkPlayerSpawnManager : SimulationBehaviour, IPlayerJoined
{
    [SerializeField]
    private GameObject _playerPrefab;

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
            Runner.Spawn(_playerPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0), player);
    }
}

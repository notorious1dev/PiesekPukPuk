using Fusion;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.Unicode;

public class PointSpawner : NetworkBehaviour, IPlayerLeft
{
    [SerializeField] NetworkPrefabRef pointInstance;
    [SerializeField] int amountOfPoints;

    private List<NetworkObject> points = new List<NetworkObject>();

    public override void Spawned()
    {
        if (!Runner.IsSharedModeMasterClient) return;
        SpawnPoints();
    }

    public void PlayerLeft(PlayerRef player)
    {
        Debug.Log("I'm quitting");
        if (Runner.GameMode != GameMode.Shared) return;
        if (Runner.IsSharedModeMasterClient && player == Object.StateAuthority)
        {
            RespawnPoints();
        }
    }

    private void RespawnPoints()
    {
        foreach (var point in points)
        {
            if (point != null)
                Runner.Despawn(point);
        }

        points.Clear();
        SpawnPoints();
    }

    private void SpawnPoints()
    {
        for (int i = 0; i < amountOfPoints; i++)
        {
            NetworkObject point = Runner.Spawn(pointInstance);
            points.Add(point);
        }
    }
}
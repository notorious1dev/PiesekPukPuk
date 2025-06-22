using Fusion;
using System.Collections;
using UnityEngine;

public class HealthComponent : NetworkBehaviour
{
    [Networked] public int health { get; set; } = 3;
    [SerializeField] int healthStandart = 3;
    [SerializeField] PlayerPointsLogic playerPointsLogic;

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Respawn();
        }
    }
    private void Respawn()
    {
        transform.position = Vector3.zero;
        playerPointsLogic.ResetPoint();
        health = healthStandart;
    }

    public int GetHealth() => health;
}

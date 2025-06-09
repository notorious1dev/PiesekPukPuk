using Fusion;
using System.Collections;
using UnityEngine;

public class HealthComponent : NetworkBehaviour
{
    [Networked] int health { get; set; } = 3;
    [SerializeField] int healthStandart = 3;

    public override void FixedUpdateNetwork()
    {
        if (health <= 0)
        {
            Respawn();
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
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
        health = healthStandart;
    }

    public int GetHealth() => health;
}

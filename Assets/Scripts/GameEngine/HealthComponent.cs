using Fusion;
using System.Collections;
using UnityEngine;

public class HealthComponent : NetworkBehaviour
{
    [Networked] public int health { get; set; } = 3;
    [SerializeField] int healthStandart = 1;
    [SerializeField] PlayerPoints playerPoints;
    public bool isDead = false;

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
        if (!HasInputAuthority) return;
        
        health = healthStandart;
        playerPoints.ResetPoint();
        
        isDead = true;
        
        PlayerUiManager.instance.ShowDeathUI();
    }

    public int GetHealth() => health;
}

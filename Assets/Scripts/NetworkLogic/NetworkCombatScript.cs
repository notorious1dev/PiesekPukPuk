using UnityEngine;
using Fusion;

public class NetworkCombatScript : NetworkBehaviour
{
    [SerializeField] float punchRadius = 2f;
    [SerializeField] float punchOffset = 0f;
    [SerializeField] int damage = 1;

    private bool wasPunchingLastTick;
    public override void FixedUpdateNetwork()
    {
        if (!GetInput(out NetworkInputData inputData)) return;
        if (!HasInputAuthority) return;

        //Punch
        if (inputData.isPlayerPunching && !wasPunchingLastTick)
        {
            Debug.Log("Punch");
            Rpc_RequestAttack();
        }


        wasPunchingLastTick = inputData.isPlayerPunching;
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_RequestAttack()
    {
        Vector2 pos = transform.position + transform.forward * punchOffset;
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, punchRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;

            Vector2 directionToTarget = (hit.transform.position - transform.position).normalized;
            float product = Vector2.Dot(transform.up, directionToTarget);
            float angle = product * Mathf.Rad2Deg;
            Debug.Log("Angle to target is " + angle);

            if (angle < 30) continue;

            var health = hit.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.Rpc_TakeDamage(damage);
            }
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void Rpc_AnimateAttack()
    {

    }
}

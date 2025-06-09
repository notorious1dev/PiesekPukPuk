using UnityEngine;
using Fusion;

public class PlayerCombatLogic : NetworkBehaviour
{
    [SerializeField]HealthComponent healthComponent;

    [SerializeField] float punchRadius = 2f;
    [SerializeField] float punchOffset = 0f;
    [SerializeField] float attackAngle = 30f;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] int damage = 1;

    private float lastAttacktime;
    private bool wasPunchingLastTick;

    public override void FixedUpdateNetwork()
    {
        if (healthComponent.GetHealth() <= 0) return;
        if (!GetInput(out NetworkInputData inputData)) return;
        if (!HasInputAuthority) return;

        //Punch
        if (inputData.isPlayerPunching && !wasPunchingLastTick)
        {
            if (CheckCoolDown())
            {
                lastAttacktime = Time.time;
                Rpc_RequestAttack();
            }
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

            if (angle < attackAngle) continue;

            var health = hit.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.Rpc_TakeDamage(damage);
            }
        }
    }

    public bool CheckCoolDown()
    {
        return Time.time - lastAttacktime >= attackCooldown;
    }
}

using UnityEngine;
using Fusion;

public class AnimationController : NetworkBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] PlayerCombatLogic playerCombatLogic;

    public override void FixedUpdateNetwork()
    {
        if (!GetInput(out NetworkInputData inputData)) return;

        //Death
        if (healthComponent.GetHealth() <= 0)
        {
            animator.SetBool("isPlayerDead", true);
            return;
        }
        else
        {
            animator.SetBool("isPlayerDead", false);
        }

        animator.SetBool("isAttack", inputData.isPlayerPunching);

        //Walk
        animator.SetFloat("speed", inputData.direction.magnitude);
    }
}

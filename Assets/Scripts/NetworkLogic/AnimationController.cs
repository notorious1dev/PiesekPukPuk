using UnityEngine;
using Fusion;

public class AnimationController : NetworkBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] HealthComponent healthComponent;

    public override void FixedUpdateNetwork()
    {
        if (!GetInput(out NetworkInputData inputData)) return;

        if (healthComponent.GetHealth() <= 0)
        {
            animator.SetBool("isPlayerDead", true);
            return;
        }
        else
        {
            animator.SetBool("isPlayerDead", false);
        }

        animator.SetFloat("speed", inputData.direction.magnitude);
        animator.SetBool("isAttack", inputData.isPlayerPunching);
    }
}

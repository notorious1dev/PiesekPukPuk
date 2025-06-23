using UnityEngine;
using Fusion;
using System.Collections;

public class PlayerMovementLogic : NetworkBehaviour
{
    [SerializeField]
    private MovementComponent _movementComponent;

    [SerializeField]
    private Transform spriteTransform;

    [SerializeField]
    private HealthComponent healthComponent;
    public Vector2 direction;

    private bool wasPunchingLastTick;
    public override void FixedUpdateNetwork()
    {
        if (healthComponent.isDead) return;
        if (!GetInput(out NetworkInputData inputData)) return;
        direction = inputData.direction;

        //Moving
        _movementComponent.Move(direction);
        RotateToDirection();
    }

    private void RotateToDirection()
    {
        if (direction == Vector2.zero) return;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteTransform.transform.rotation = Quaternion.Slerp(spriteTransform.transform.rotation, Quaternion.Euler(0, 0, angle + 90), 15f * Runner.DeltaTime);
    }
}
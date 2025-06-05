using UnityEngine;
using Fusion;
using System.Collections;

public class NetworkInputDataReciever : NetworkBehaviour
{
    [SerializeField]
    private MovementComponent _movementComponent;
    public Vector2 direction;

    private bool wasPunchingLastTick;
    public override void FixedUpdateNetwork()
    {
        if (!GetInput(out NetworkInputData inputData)) return;
        direction = inputData.direction;

        //Moving
        _movementComponent.Move(direction, Runner.DeltaTime);
        RotateToDirection();
    }

    private void RotateToDirection()
    {
        if (direction == Vector2.zero) return;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle -90), 15f * Runner.DeltaTime);
    }
}

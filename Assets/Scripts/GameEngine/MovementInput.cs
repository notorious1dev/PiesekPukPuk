using UnityEngine;

public sealed class MovementInput : MonoBehaviour
{
    [SerializeField]
    private FixedJoystick _joystick;
    public Vector2 GetDirection()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;
        return new Vector2(horizontal, vertical).normalized;
    }
}

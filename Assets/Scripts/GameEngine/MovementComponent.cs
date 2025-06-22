using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public float speed = 5.0f;

    [SerializeField]
    private Rigidbody2D _rigidbody;
    public void Move(Vector3 direction)
    {
        _rigidbody.linearVelocity = direction * speed;
    }
}

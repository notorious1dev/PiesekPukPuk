using Fusion;
using TMPro;
using UnityEngine;

public class PointPickUp : NetworkBehaviour
{
    [SerializeField] int pointValue = 1;
    [SerializeField] int xBound = 10;
    [SerializeField] int yBound = 10;
    [Networked] public Vector2 TargetPosition { get; set; } = Vector2.zero;

    public override void Spawned()
    {
        MoveToRandomPosition(xBound, yBound);
    }

    public override void FixedUpdateNetwork()
    {
        transform.position = TargetPosition;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!HasStateAuthority) return;

        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.TryGetComponent(out PlayerPointsLogic pointsLogic))
        {
            pointsLogic.AddPoint(pointValue);
            MoveToRandomPosition(xBound, yBound);
        }
    }
    private void MoveToRandomPosition(float xMax, float yMax)
    {
        float x = Random.Range(-xMax, xMax);
        float y = Random.Range(-yMax, yMax);
        TargetPosition = new Vector2(x, y);
    }
}

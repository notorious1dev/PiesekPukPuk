using Fusion;
using System.Collections;
using UnityEngine;

public class PointLogic : NetworkBehaviour
{
    [SerializeField] private int pointValue = 1;

    //Position
    [Networked] private Vector2 TargetPosition { get; set; }
    [SerializeField] private float xBound = 10;
    [SerializeField] private float yBound = 10;

    //Color
    [Networked] private Color Color { get; set; }
    private SpriteRenderer spriteRenderer;
    private Color[] colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.cyan,
        Color.yellow,
        new Color(255, 166, 0, 255),
        new Color(163, 255, 0, 255),
        new Color(255, 0, 104, 255),
    };

    public override void Spawned()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (!Runner.IsSharedModeMasterClient) return;
        MoveToRandomPosition();
        GenerateNewColor();
    }

    public override void FixedUpdateNetwork()
    {
        if (!Runner.IsSharedModeMasterClient) return;

        transform.position = TargetPosition;
        spriteRenderer.color = Color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!HasStateAuthority) return;

        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.TryGetComponent(out PlayerPointsLogic pointsLogic))
        {
            pointsLogic.AddPoint(pointValue);
            MoveToRandomPosition();
            GenerateNewColor();
        }
    }

    private void MoveToRandomPosition()
    {
        float x = Random.Range(-xBound, xBound);
        float y = Random.Range(-yBound, yBound);
        TargetPosition = new Vector2(x, y);
    }

    private void GenerateNewColor() => Color = colors[Random.Range(0, colors.Length - 1)];
}

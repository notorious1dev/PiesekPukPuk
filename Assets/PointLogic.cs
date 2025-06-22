using System;
using Fusion;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class PointLogic : NetworkBehaviour
{
    [SerializeField] private int pointValue = 1;

    [Networked] public Vector2 TargetPosition { get; set; }
    [Networked] public Color NetworkColor { get; set; }
    [Networked] public bool IsVisible { get; set; } = true;

    [SerializeField] private float xBound = 10;
    [SerializeField] private float yBound = 10;

    private SpriteRenderer spriteRenderer;

    private Color[] colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.cyan,
        Color.yellow,
        new Color(1f, 166f, 0f, 1f),
        new Color(163f, 1f, 0f, 1f),
        new Color(1f, 0f, 104f, 1f),
    };

    public override void Render()
    {
        spriteRenderer.enabled = IsVisible;
        spriteRenderer.color = NetworkColor;
    }

    public override void Spawned()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (!HasStateAuthority) return;

        MoveToRandomPosition();
        GenerateNewColor();
        IsVisible = true;
    }

    public override void FixedUpdateNetwork()
    {
        transform.position = TargetPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsVisible = false;
        //if (!Runner.IsSharedModeMasterClient) return;

        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.TryGetComponent(out PlayerPointsLogic pointsLogic))
        {
            pointsLogic.AddPoint(pointValue);
            RespawnPoint();
        }
    }

    private void RespawnPoint()
    {
        MoveToRandomPosition();
        GenerateNewColor();
        StartCoroutine(nameof(SetVisibilityTrue));
    }

    private IEnumerator SetVisibilityTrue()
    {
        yield return new WaitForSeconds(1f);
        IsVisible = true;
    }

    private void MoveToRandomPosition()
    {
        float x = Random.Range(-xBound, xBound);
        float y = Random.Range(-yBound, yBound);
        TargetPosition = new Vector2(x, y);
    }

    private void GenerateNewColor()
    {
        NetworkColor = colors[Random.Range(0, colors.Length)];
    }
}

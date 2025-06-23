using System;
using UnityEngine;
using Fusion;
using Unity.Cinemachine;

public class PlayerPointsLogic : NetworkBehaviour
{
    [SerializeField] private PlayerPoints playerPoints;
    [SerializeField] private PlayerCombatLogic playerCombat;
    [SerializeField] private HealthComponent playerHealth;
    [SerializeField] private MovementComponent playerMovement;
    
    [SerializeField] Transform playerSprite;
    private CinemachineCamera playerCamera;
    
    private int points;
    private float normalScale;
    private float normalSpeed;
    private float normalCameraLens;
    private float normalAttackCoolDown;
    private float normalPunchOffset;

    public void Awake()
    {
        normalScale = playerSprite.localScale.x;
        normalSpeed = playerMovement.speed;
        normalCameraLens = MainCameraManager.instance.Lens.OrthographicSize;
        normalAttackCoolDown = playerCombat.attackCooldown;
        normalPunchOffset = playerCombat.punchOffset;
    }

    public void Update()
    {
        if (!HasInputAuthority) return;
        CalculateResizing();
    }

    private void CalculateResizing()
    {
        int currentPoints = playerPoints.GetPoints();
        if (points == currentPoints) return;

        points = currentPoints;

        float scaleMultiplier = 1f + Mathf.Log10(points + 1) * 0.3f;
        playerSprite.localScale = new Vector3(normalScale * scaleMultiplier, normalScale * scaleMultiplier, 1);

        float speedReduction = Mathf.Log10(points + 1) * 1.5f;
        playerMovement.speed = Mathf.Max(normalSpeed - speedReduction, normalSpeed * 0.6f);

        float cameraZoom = normalCameraLens + Mathf.Log10(points + 1);
        MainCameraManager.instance.Lens.OrthographicSize = cameraZoom;

        playerCombat.attackCooldown = normalAttackCoolDown + Mathf.Log10(points + 1) * 0.15f;

        playerCombat.punchOffset = normalPunchOffset + Mathf.Log10(points + 1) * 0.1f;

        int newHealthAndDamage = Mathf.Clamp(Mathf.FloorToInt(Mathf.Sqrt(points)), 1, 10);
        playerHealth.health = newHealthAndDamage;
        playerCombat.damage = newHealthAndDamage;
    }


}

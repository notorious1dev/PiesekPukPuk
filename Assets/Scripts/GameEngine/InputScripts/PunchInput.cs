using Fusion;
using UnityEngine;
using System.Collections;

public class PunchInput : MonoBehaviour
{
    public static PunchInput instance;
    public bool isPlayerPunching;
    private float punchBoofer = 0;
    [SerializeField] private float punchRegularValue = 0;

    private HealthComponent playerHealth;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        isPlayerPunching = false;

        if (punchBoofer > 0)
        {
            punchBoofer -= Time.deltaTime;
            isPlayerPunching = true;
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
            {
                punchBoofer = punchRegularValue;
                break;
            }
        }
    }
}
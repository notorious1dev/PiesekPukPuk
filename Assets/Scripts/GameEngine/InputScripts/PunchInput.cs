using UnityEngine;

public class PunchInput : MonoBehaviour
{
    public bool isPlayerPunching;
    
    private float punchBoofer = 0;
    [SerializeField] public float punchRegularValue = 0;
    void Update()
    {
        isPlayerPunching = false;

        if (punchBoofer > 0)
        {
            punchBoofer -= Time.deltaTime;
            isPlayerPunching = true;
        }
        else
        {
            isPlayerPunching = false;
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

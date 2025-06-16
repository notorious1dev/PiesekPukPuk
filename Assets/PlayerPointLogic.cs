using Fusion;
using TMPro;
using UnityEngine;

public class PlayerPointsLogic : NetworkBehaviour
{
    [Networked] public int pointCounter { get; set; } = 0;

    public void AddPoint(int value)
    {
        pointCounter += value;
    }

    public int GetPoints() => pointCounter;

    public void ResetPoint()
    {
        pointCounter = 0;
    }
}

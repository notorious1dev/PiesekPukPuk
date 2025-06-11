using Fusion;
using UnityEngine;

public class PlayerPointsLogic : NetworkBehaviour
{
    [SerializeField] private int pointCounter = 0;
    public void AddPoint(int value)
    {
        pointCounter += value;
    }
    public int GetPoints() => pointCounter;
    public int ResetPoint() => pointCounter = 0;
}

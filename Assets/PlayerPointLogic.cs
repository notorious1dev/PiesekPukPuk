using TMPro;
using UnityEngine;

public class PlayerPointsLogic : MonoBehaviour
{
    [SerializeField] private int pointCounter = 0;

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

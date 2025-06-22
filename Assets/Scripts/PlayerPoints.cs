using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPoints : NetworkBehaviour
{
    [Networked] internal int pointCounter { get; set; } = 0;

    public void Update()
    {
        if (!HasInputAuthority) return;
        
        if (CounterUIManager.Instance is null)
            return;
        
        CounterUIManager.Instance.UpdateCounter(pointCounter);
    }
    
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

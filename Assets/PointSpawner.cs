using Fusion;
using UnityEngine;

public class PointSpawner : NetworkBehaviour
{
    [SerializeField] GameObject pointInstance;
    [SerializeField] int amountOfPoints = 25;
    public override void Spawned()
    {
        for (int i = 0; i < amountOfPoints; i++)
        {
            Runner.Spawn(pointInstance);
        }
    }
}
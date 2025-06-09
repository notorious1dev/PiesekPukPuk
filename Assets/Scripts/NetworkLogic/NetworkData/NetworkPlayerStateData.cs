using Fusion;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public struct NetworkPlayerStateData : INetworkInput
{
    public bool isDead;
}

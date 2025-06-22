using System;
using Unity.Cinemachine;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{
    public static CinemachineCamera instance;

    public void Awake()
    {
        instance = GetComponent<CinemachineCamera>();  
    }
}

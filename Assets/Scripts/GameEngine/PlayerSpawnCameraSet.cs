using UnityEngine;
using Fusion;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class PlayerSpawnCameraSet : NetworkBehaviour
{
    private CinemachineCamera _camera;
    public override void Spawned()
    {
        SetCamera();
    }

    public void SetCamera()
    {
        if (!HasInputAuthority) return;
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineCamera>();

        if (_camera == null)
        {
            Debug.LogWarning("Camera not found");
            return;
        }
        _camera.LookAt = transform;
        _camera.Follow = transform;
    }
}

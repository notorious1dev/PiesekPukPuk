using Fusion;
using UnityEngine;

public class PlayerSFXManager : NetworkBehaviour
{
    [SerializeField] private NetworkAudioSFX audioSFX;
    [SerializeField] private PlayerCombatLogic combatLogic;

    //check in the future
    private bool _punchInputDetectedThisFrame = false;

    public override void Render()
    {
        if (HasInputAuthority)
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2 && combatLogic.CheckCoolDown())
                {
                    audioSFX.PlayPunchLocally();
                    _punchInputDetectedThisFrame = true;
                }
            }
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (HasInputAuthority && _punchInputDetectedThisFrame)
        {
            _punchInputDetectedThisFrame = false;
            RPC_RequestPlayPunchSFX();
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_RequestPlayPunchSFX(RpcInfo info = default)
    {
        audioSFX.RPC_PlayPunchSFX(info.Source);
    }
}
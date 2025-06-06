using Fusion;
using UnityEngine;

public class NetworkAudioSFX : NetworkBehaviour
{
    [SerializeField] private AudioClip[] punches;
    [SerializeField] private AudioSource source;

    public void PlayPunchLocally()
    {
        if (punches == null || punches.Length == 0 || source == null) return;
        source.PlayOneShot(punches[Random.Range(0, punches.Length)]);
    }


    //TOFIX: Host plays punch sound 2 times
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_PlayPunchSFX(PlayerRef initiator)
    {
        if (initiator == Runner.LocalPlayer)
        {
            return;
        }

        Debug.Log($"Remote client {Runner.LocalPlayer} is playing sound for initiator {initiator}.");
        PlayPunchLocally();
    }
}
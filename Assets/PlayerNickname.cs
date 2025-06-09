using Fusion;
using TMPro;
using UnityEngine;

public class PlayerNickname : NetworkBehaviour
{
    [Networked] public NetworkString<_32> Nickname { get; set; }
    [SerializeField] private TextMeshPro nicknameMesh;

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            string localNick = PlayerPrefs.GetString("nickname", "Sobaka_" + Random.Range(0, 9999));
            RPC_SetNickname(localNick);
        }

        nicknameMesh.text = Nickname.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_SetNickname(string nick)
    {
        Nickname = nick;
    }

    public override void FixedUpdateNetwork()
    {
        if (nicknameMesh.text != Nickname.ToString())
        {
            nicknameMesh.text = Nickname.ToString();
        }
    }
}

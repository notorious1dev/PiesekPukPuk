using Fusion;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class NetworkStartSession : MonoBehaviour
{
    [SerializeField]
    private NetworkSceneManagerDefault _networkSceneManager;

    [SerializeField]
    private NetworkRunner _networkRunner;

    [SerializeField]
    private string _sessionName = "DefaultSession";

    private void Start()
    {
        StartSession(GameMode.Shared);
    }

    private async Task StartSession(GameMode GameMode)
    {
        _networkRunner.ProvideInput = true;

        await _networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode,
            SceneManager = _networkSceneManager,
            Scene = GetScene(),
            SessionName = _sessionName,
        });
    }

    private SceneRef GetScene()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        return SceneRef.FromIndex(buildIndex);
    }
}

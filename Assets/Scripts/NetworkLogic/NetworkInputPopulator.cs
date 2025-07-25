using Fusion;
using UnityEngine;

public class NetworkInputPopulator : MonoBehaviour
{
    [SerializeField]
    private MovementInput _movementInput;

    [SerializeField]
    private PunchInput _punchInput;

    [SerializeField]
    private NetworkCallbacksReceiver _callbacksReceiver;

    private void OnEnable()
    {
        _callbacksReceiver.OnPopulateInput += PopulateInput;
    }

    private void OnDisable()
    {
        _callbacksReceiver.OnPopulateInput -= PopulateInput;
    }

    private void PopulateInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData inputData = new()
        {
            direction = _movementInput.GetDirection(),
            isPlayerPunching = _punchInput.isPlayerPunching,
        };
        input.Set(inputData);
    }
}

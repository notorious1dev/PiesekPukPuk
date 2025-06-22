using UnityEngine;
using TMPro;

public class CounterUIManager : MonoBehaviour
{
    public static CounterUIManager Instance;
    [SerializeField] private TMP_Text counterUI;
    public void Awake()
    {
        Instance = this;
    }

    public void UpdateCounter(int value)
    {
        counterUI.text = value.ToString();
    }
}

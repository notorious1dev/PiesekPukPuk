using System;
using UnityEngine;

public class PlayerUiManager : MonoBehaviour
{
    public static PlayerUiManager instance;
    [SerializeField] private GameObject deathUI;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private NetworkPlayerSpawnManager playerRespawnManager;

    public void Awake()
    {
        instance = this;
        deathUI.SetActive(false);
        playerUI.SetActive(true);
    }

    public void ShowDeathUI()
    {
        deathUI.SetActive(true);
        playerUI.SetActive(false);
    }

    public void ShowPlayerUI()
    {
        deathUI.SetActive(false);
        playerUI.SetActive(true);
    }
    public void BtnExit()
    {
        Application.Quit();
    }

    public void BtnRestart()
    {
        ShowPlayerUI();
        playerRespawnManager.RespawnLocalPlayer();
    }
}

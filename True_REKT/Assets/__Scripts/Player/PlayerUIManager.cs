using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    StatusPopUpsUI statusPopUpsUI;
    PlayerManager player;

    [Header("Crosshair")]
    public GameObject crosshair;

    [Header("Ammo")]
    public Text currentAmmoCountText;
    public Text reserveAmmoCountText;

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
        statusPopUpsUI = GetComponentInChildren<StatusPopUpsUI>();
    }

    public void DisplayHealthPopUp()
    {
        statusPopUpsUI.DisplayHealthPopUp(player.playerStatsManager.health);
    }
}

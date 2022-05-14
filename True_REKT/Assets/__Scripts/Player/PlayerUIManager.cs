using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Crosshair")]
    public GameObject crosshair;

    [Header("Ammo")]
    public Text currentAmmoCountText;
    public Text reserveAmmoCountText;
}

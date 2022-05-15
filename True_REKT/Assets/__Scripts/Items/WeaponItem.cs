using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    [Header("Weapon animation")]
    public AnimatorOverrideController weaponAnimator;

    [Header("Damage")]
    public int damage = 20;

    [Header("Ammo info")]
    public int remainingAmmo = 0;
    public int maxAmmo = 12;
    public AmmoType ammoType = AmmoType.handgun;
}

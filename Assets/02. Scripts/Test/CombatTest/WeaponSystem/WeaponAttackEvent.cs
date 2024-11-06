using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackEvent : MonoBehaviour
{
    public event Action<WeaponAttackEvent, WeaponAttackEventArgs> OnWeaponAttack;

    public void CallWeaponAttackEvent(Weapon weapon, int weaponIndex)
    {
        OnWeaponAttack?.Invoke(this, new WeaponAttackEventArgs()
        {
            weapon = weapon,
            weaponIndex = weaponIndex
        });
    }
}

public class WeaponAttackEventArgs : EventArgs
{
    public Weapon weapon;
    public int weaponIndex; // 몇번째 무기인지
}
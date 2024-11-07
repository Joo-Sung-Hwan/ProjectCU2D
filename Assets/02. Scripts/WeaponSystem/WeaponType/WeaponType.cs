using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

[System.Serializable]
public abstract class WeaponType
{
    public abstract void Attack(Weapon weapon);
}

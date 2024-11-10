using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;


public abstract class WeaponTypeDetailsSO : ScriptableObject
{
    public abstract void Attack(Weapon weapon);
}

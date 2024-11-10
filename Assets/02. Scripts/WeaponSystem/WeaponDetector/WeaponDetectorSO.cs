using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponDetectorSO : ScriptableObject, IDetector
{
    public abstract bool DetectMonster(Weapon weapon, out Vector2 direction, out GameObject monster);
}

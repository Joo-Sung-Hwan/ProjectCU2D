
using UnityEngine;

public interface IDetector
{
    bool DetectMonster(Weapon weapon, out Vector2 direction, out GameObject monster);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Slow_", menuName = "Scriptable Objects/Weapon/BonusEffect/Slow")]
public class SlowBE : BonusEffectSO
{
    public override void Apply(Monster monster)
    {
        monster.Stat.Speed = 0;
    }
}

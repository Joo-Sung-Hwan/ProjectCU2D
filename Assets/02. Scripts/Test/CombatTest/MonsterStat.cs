using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat
{
    public float Hp { get; private set; }
    public int Atk { get; private set; }


    public MonsterStat(EnemyDetailsSO enemyDetailsSO)
    {
        Hp = enemyDetailsSO.maxHp;
        Atk = enemyDetailsSO.contactDamageAmount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat
{
    public float Hp { get; private set; }
    public int Atk { get; private set; }
    public int Speed { get; private set; }
    public float ChaseDistance { get; private set; }


    public MonsterStat(MonsterDetailsSO enemyDetailsSO)
    {
        Hp = enemyDetailsSO.maxHp;
        Atk = enemyDetailsSO.contactDamageAmount;
        Speed = enemyDetailsSO.speed;
        ChaseDistance = enemyDetailsSO.chaseDistance;
    }
}

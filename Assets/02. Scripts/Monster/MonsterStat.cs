using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat
{
    public float Hp { get; set; }
    public int Atk { get; set; }
    public float Speed { get; set; }
    public float ChaseDistance { get; set; }


    public void InitializeMonsterStat(MonsterDetailsSO enemyDetailsSO)
    {
        Hp = enemyDetailsSO.maxHp;
        Atk = enemyDetailsSO.contactDamageAmount;
        Speed = enemyDetailsSO.speed;
        ChaseDistance = enemyDetailsSO.chaseDistance;
    }


}

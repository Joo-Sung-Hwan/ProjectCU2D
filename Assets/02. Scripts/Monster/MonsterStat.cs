using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat
{
    public float Hp { get; set; }
    public float Atk { get; set; }
    public float Speed { get; set; }
    public float ChaseDistance { get; set; }


    public void InitializeMonsterStat(MonsterDetailsSO enemyDetailsSO, int waveCount)
    {
        Hp = enemyDetailsSO.maxHp + (enemyDetailsSO.waveBonusHp * waveCount);
        Atk = enemyDetailsSO.baseDamage + (enemyDetailsSO.waveBonusDmg * waveCount);
        Speed = enemyDetailsSO.speed;
        ChaseDistance = enemyDetailsSO.chaseDistance;
    }


}

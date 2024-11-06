using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public float Hp { get; private set; }
    public float HpRegen { get; private set; }
    public float HpSteal { get; private set; }
    public int MeleeDamage { get; private set; }
    // ... �� �� ��Ÿ ���ȵ�


    public PlayerStat(PlayerDetailsSO playerDetailsSO)
    {
        Hp = playerDetailsSO.Hp;
        HpRegen = playerDetailsSO.HpRegen;
        HpSteal = playerDetailsSO.HpSteal;
        MeleeDamage = playerDetailsSO.MeleeDamage;
    }
}

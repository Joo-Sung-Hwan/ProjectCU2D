using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public float Hp { get; private set; }
    public float HpRegen { get; private set; }
    //public float HpSteal { get; private set; }
    public int BonusDamage { get; private set; }
    public int MeleeDamage { get; private set; }
    public int RangeDamage { get; private set; }
    public float Speed { get; private set; }
    public float Dodge { get; private set; }
    public float PickUpRange { get; private set; }
    // ... 그 외 기타 스탯들


    public void InitializePlayerStat(PlayerDetailsSO playerDetailsSO)
    {
        Hp = playerDetailsSO.Hp;
        HpRegen = playerDetailsSO.HpRegen;
        //HpSteal = playerDetailsSO.HpSteal;
        BonusDamage = playerDetailsSO.BonusDamage;
        MeleeDamage = playerDetailsSO.MeleeDamage;
        RangeDamage = playerDetailsSO.RangeDamage;
        Speed = playerDetailsSO.Speed;
        Dodge = playerDetailsSO.Dodge;
        PickUpRange = playerDetailsSO.PickUpRange;
    }
}

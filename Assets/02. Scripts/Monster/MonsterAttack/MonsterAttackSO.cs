using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAttackSO : ScriptableObject, IAttack, ICloneable
{
    protected Monster monster;


    public void InitializeMonsterAttack(Monster monster)
        => this.monster = monster;


    #region INTERFACE
    public abstract void Attack();

    public object Clone()
        => Instantiate(this);
    #endregion
}

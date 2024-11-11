using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MonsterMovementSO : ScriptableObject,IMovement , ICloneable
{
    protected Rigidbody2D rigid;
    protected Monster monster;
    protected float dist;
    protected Vector2 moveVec;


    public void InitializeMonsterMovement(Monster monster)
    {
        this.monster = monster;
        rigid = monster.Rigid;
    }


    #region INTERFACE
    public virtual void Move()
    {
        // 모든 이동패턴에서 플레이어와의 거리는 무조건 필요함 -> 코드 중복되지 않게 여기서 구하기
        dist = (monster.Player.position - monster.transform.position).sqrMagnitude;
    }

    public object Clone()
        => Instantiate(this);
    #endregion
}

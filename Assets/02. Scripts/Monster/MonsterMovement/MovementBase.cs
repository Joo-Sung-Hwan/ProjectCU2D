using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MovementBase", menuName = "Scriptable Objects/Monster/MonsterMovement/Base")]
public class MovementBase : MonsterMovementSO
{
    public override void Move()
    {
        base.Move(); // 플레이어와의 거리 구하기

        if (dist < monster.Stat.ChaseDistance)
        {
            rigid.velocity = Vector2.zero;
            return;
        }

        moveVec = (monster.Player.position - monster.transform.position).normalized;

        rigid.velocity = moveVec * monster.Stat.Speed;
    }
}

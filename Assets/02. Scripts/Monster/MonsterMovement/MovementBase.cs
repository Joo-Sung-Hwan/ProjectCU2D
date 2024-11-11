using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MovementBase", menuName = "Scriptable Objects/Monster/MonsterMovement/Base")]
public class MovementBase : MonsterMovementSO
{
    public override void Move()
    {
        moveVec = (monster.Player.position - monster.transform.position).normalized;

        rigid.velocity = moveVec * monster.Stat.Speed;
    }
}

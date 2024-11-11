using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MovementCharge", menuName = "Scriptable Objects/Monster/MonsterMovement/Charge")]
public class MovementCharge : MonsterMovementSO
{
    public override void Move()
    {
        moveVec = (monster.Player.position - monster.transform.position).normalized;

        rigid.velocity = moveVec * monster.Stat.Speed;
    }
}

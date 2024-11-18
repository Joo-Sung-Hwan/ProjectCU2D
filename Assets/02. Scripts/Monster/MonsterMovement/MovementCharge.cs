using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MovementCharge", menuName = "Scriptable Objects/Monster/MonsterMovement/Charge")]
public class MovementCharge : MonsterMovementSO
{
    [SerializeField] private float chargeSpeed = 10f; // 돌진속도

    private bool isCharge; // 돌진 중인지


    public override void Move()
    {
        base.Move(); // 플레이어와의 거리 구하기

        // 돌진 거리에 들어왔고 돌진중이 아니라면 돌진시작
        if (dist < monster.Stat.ChaseDistance && isCharge == false)
        {
            rigid.velocity = Vector2.zero;
            Charge().Forget();
            return;
        }

        // 현재 돌진중이 아닐때만 걸어서 이동
        else if (isCharge == false)
        {
            moveVec = (monster.Player.position - monster.transform.position).normalized;
            rigid.velocity = moveVec * monster.Stat.Speed;
        }
    }

    private async UniTask Charge()
    {
        isCharge = true;
        rigid.mass = 100; // 돌진 준비중일때 넉백 X

        monster.Sprite.color = Settings.legend; // 돌진 준비중일때 색 변화

        await UniTask.Delay(1000);

        monster.Sprite.color = Color.white;

        moveVec = (monster.Player.position - monster.transform.position).normalized;
        rigid.velocity = moveVec * chargeSpeed;

        await UniTask.Delay(1000);

        isCharge = false;
        rigid.mass = 0.1f;

        return;
    }
}

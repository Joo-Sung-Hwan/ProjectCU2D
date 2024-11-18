using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AttackTrap", menuName = "Scriptable Objects/Monster/MonsterAttack/Trap")]
public class AttackTrap : MonsterAttackSO
{
    [SerializeField] private int count = 5; // 함정 개수
    private GameObject trap;
    private Vector2 trapPos;


    public override void Attack()
       => CreateTrapRoutine().Forget();


    private async UniTask CreateTrapRoutine()
    {
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                // 플레이어 위치 기준 x,y +-5 위치에 함정 생성
                float dx = Random.Range(-5.0f, 5.0f);
                float dy = Random.Range(-5.0f, 5.0f);
                trapPos.x = monster.Player.position.x + dx;
                trapPos.y = monster.Player.position.y + dy;
                CreateTrap();
            }

            await UniTask.Delay(Settings.monsterFireRate, cancellationToken: monster.DisableCancellation.Token);
            // 몬스터가 중간에 비활성화될때를 대비
        }
    }

    private void CreateTrap()
    {
        // 랜덤으로 지정한 위치에 트랩 활성화
        trap = ObjectPoolManager.Instance.Get("MonsterTrap", trapPos, Quaternion.identity);
        // 데미지 넣어서 초기화
        trap.GetComponent<MonsterTrap>().InitializeMonsterTrap(monster.Stat.Atk);
    }
}

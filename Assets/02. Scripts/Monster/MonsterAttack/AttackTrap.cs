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
        // 발사 명령이 떨어지면 풀에서 투사체 활성화
        trap = ObjectPoolManager.Instance.Get("MonsterTrap", trapPos, Quaternion.identity);
        // 속도, 방향, 데미지 넣어서 초기화
        trap.GetComponent<MonsterTrap>().InitializeMonsterTrap(monster.Stat.Atk);
    }
}

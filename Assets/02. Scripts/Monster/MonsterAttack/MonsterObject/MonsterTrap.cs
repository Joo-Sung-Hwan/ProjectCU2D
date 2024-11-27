using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrap : MonoBehaviour
{
    private CircleCollider2D hitbox;
    private float dmg;



    private void Awake()
    {
        hitbox = GetComponent<CircleCollider2D>();
    }

    public void InitializeMonsterTrap(float dmg)
    {
        // 히트박스 비활성화
        hitbox.enabled = false;
        this.dmg = dmg;

        ActivateTrap().Forget();
    }

    private async UniTask ActivateTrap()
    {
        await UniTask.Delay(1000); // 1초 뒤에 함정 활성화

        hitbox.enabled = true;

        await UniTask.Delay(100); // 0.1초 뒤에 함정 비활성화

        ObjectPoolManager.Instance.Release(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(dmg);
        }
    }
}

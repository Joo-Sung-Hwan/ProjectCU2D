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
        // ��Ʈ�ڽ� ��Ȱ��ȭ
        hitbox.enabled = false;
        this.dmg = dmg;

        ActivateTrap().Forget();
    }

    private async UniTask ActivateTrap()
    {
        await UniTask.Delay(1000); // 1�� �ڿ� ���� Ȱ��ȭ

        hitbox.enabled = true;

        await UniTask.Delay(500); // 0.5�� �ڿ� ���� ��Ȱ��ȭ

        ObjectPoolManager.Instance.Release(gameObject, "MonsterTrap");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(dmg);
        }
    }
}
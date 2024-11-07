using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour  // 아이템에 연결할 클래스
{
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InitializeItem()
    {
        // 몬스터가 드랍하는 아이템의 정보 받아와서 초기화
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 경험치, 돈 획득하는 코드 구현 (부딪힌 대상은 무조건 플레이어 확정임)

        ObjectPoolManager.Instance.Release(gameObject, "Item");
    }
}

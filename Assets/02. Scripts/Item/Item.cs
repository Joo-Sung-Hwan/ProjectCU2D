using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour  // �����ۿ� ������ Ŭ����
{
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InitializeItem()
    {
        // ���Ͱ� ����ϴ� �������� ���� �޾ƿͼ� �ʱ�ȭ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����ġ, �� ȹ���ϴ� �ڵ� ���� (�ε��� ����� ������ �÷��̾� Ȯ����)

        ObjectPoolManager.Instance.Release(gameObject, "Item");
    }
}

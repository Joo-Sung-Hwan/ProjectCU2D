using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MonsterDestroyedEvent))]
[DisallowMultipleComponent]
public class MonsterDestroyed : MonoBehaviour
{
    private MonsterDestroyedEvent destroyedEvent;
    private Monster monster;

    private void Awake()
    {
        destroyedEvent = GetComponent<MonsterDestroyedEvent>();
        monster = GetComponent<Monster>();
    }
    private void OnEnable()
    {
        destroyedEvent.OnMonsterDestroyed += DestroyedEvent_OnDestroyed;
    }
    private void OnDisable()
    {
        destroyedEvent.OnMonsterDestroyed -= DestroyedEvent_OnDestroyed;
    }

    private void DestroyedEvent_OnDestroyed(MonsterDestroyedEvent obj, MonsterDestroyedEventArgs args)
    {
        // ���Ͱ� �ı��� ������ ������ ����
        var item = ObjectPoolManager.Instance.Get("Item", args.point, Quaternion.identity);
        item.GetComponent<Item>().InitializeItem(monster.DropItem);

        ObjectPoolManager.Instance.Release(gameObject, "Monster");
    }
}

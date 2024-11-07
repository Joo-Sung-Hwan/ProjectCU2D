using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MonsterDestroyedEvent))]
[DisallowMultipleComponent]
public class MonsterDestroyed : MonoBehaviour
{
    private MonsterDestroyedEvent destroyedEvent;

    private void Awake()
    {
        destroyedEvent = GetComponent<MonsterDestroyedEvent>();
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
        ObjectPoolManager.Instance.Get("Item", args.point, Quaternion.identity);

        ObjectPoolManager.Instance.Release(gameObject, "Monster");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    private MonsterSpawnEvent monsterSpawnEvent;
    private Stage stage;


    private void Awake()
    {
        monsterSpawnEvent = GetComponent<MonsterSpawnEvent>();
    }
    private void OnEnable()
    {
        monsterSpawnEvent.OnMonsterSpawn += MonsterSpawnEvent_OnMonsterSpawn;
    }                                       
    private void OnDisable()                
    {                                       
        monsterSpawnEvent.OnMonsterSpawn -= MonsterSpawnEvent_OnMonsterSpawn;
    }

    private void MonsterSpawnEvent_OnMonsterSpawn(MonsterSpawnEvent @event, MonsterSpawnEventArgs args)
    {
        stage = args.stage;

    }
}
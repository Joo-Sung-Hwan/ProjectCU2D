using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnEvent : MonoBehaviour
{
    public event Action<MonsterSpawnEvent, MonsterSpawnEventArgs> OnMonsterSpawn;

    public void CallMonsterSpawn(Stage stage)
    {
        OnMonsterSpawn?.Invoke(this, new MonsterSpawnEventArgs()
        {
            stage = stage
        });
    }
}

public class MonsterSpawnEventArgs : EventArgs
{
    public Stage stage;
}
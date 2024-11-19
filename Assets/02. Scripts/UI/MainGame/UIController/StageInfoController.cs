using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoController : MonoBehaviour
{
    [SerializeField] private StageInfoView stageInfo;
    private Stage currentStage;


    public void InitializeStageInfoController()
    {
        StageManager.Instance.CurrentStage.MonsterSpawnEvent.OnWaveStart += MonsterSpawnEvent_OnWaveStart;
        currentStage = StageManager.Instance.CurrentStage;

        stageInfo.InitializeStageInfoView(currentStage.MonsterSpawner.WaveCount, currentStage.MonsterSpawner.WaveTimer);
    }

    private void OnDisable()
    {
        StageManager.Instance.CurrentStage.MonsterSpawnEvent.OnWaveStart -= MonsterSpawnEvent_OnWaveStart;
    }

    private void MonsterSpawnEvent_OnWaveStart(MonsterSpawnEvent arg1, int arg2)
    {
        stageInfo.InitializeStageInfoView(currentStage.MonsterSpawner.WaveCount, currentStage.MonsterSpawner.WaveTimer);
    }
}

using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ExitGames.Client.Photon.StructWrapping;

public class MonsterSpawn : MonoBehaviour
{
    private MonsterSpawnEvent monsterSpawnEvent;
    private List<WaveSpawnParameter> waveSpawnParameterList;
    private WaveSpawnParameter currentWaveSpawnParameter;
    private Vector2 spawnPosition;
    private int waveCount;
    private int waveTimer;

    public int WaveCount => waveCount;
    public int WaveTimer => waveTimer;



    private void Awake()
    {
        monsterSpawnEvent = GetComponent<MonsterSpawnEvent>();
    }
    private void OnEnable()
    {
        monsterSpawnEvent.OnMonsterSpawn += MonsterSpawnEvent_OnMonsterSpawn;
        monsterSpawnEvent.OnWaveStart += MonsterSpawnEvent_OnWaveStart;
        monsterSpawnEvent.OnWaveFinish += MonsterSpawnEvent_OnWaveFinish;
    }
    private void OnDisable()                
    {                                       
        monsterSpawnEvent.OnMonsterSpawn -= MonsterSpawnEvent_OnMonsterSpawn;
        monsterSpawnEvent.OnWaveStart -= MonsterSpawnEvent_OnWaveStart;
        monsterSpawnEvent.OnWaveFinish -= MonsterSpawnEvent_OnWaveFinish;
    }


    private void MonsterSpawnEvent_OnMonsterSpawn(MonsterSpawnEvent @event, MonsterSpawnEventArgs args)
    {
        waveSpawnParameterList = args.stage.WaveSpawnParameter;

        monsterSpawnEvent.CallWaveStart(0); // 첫 웨이브부터 시작
    }

    private void MonsterSpawnEvent_OnWaveStart(MonsterSpawnEvent @event, int waveCnt)
    {
        // 현재 웨이브에 해당하는 웨이브 스폰 파라미터 받아오기
        waveCount = waveCnt;
        currentWaveSpawnParameter = waveSpawnParameterList[waveCount];

        // 웨이브 지속시간 : 20+5(wave) ~ 60 사이
        waveTimer = Mathf.Clamp(waveTimer, Settings.waveTimer + (Settings.extraTimePerWave * waveCount), 60);
        Debug.Log(WaveTimer);

        if (currentWaveSpawnParameter.isBossWave == true) return; // 보스생성 추후에 구현

        WaveMonsterSpawn().Forget(); // UniTask 호출
    }

    private void MonsterSpawnEvent_OnWaveFinish(MonsterSpawnEvent @event, int waveCnt)
    {
        Debug.Log($"Wave Finish!!! - {waveCount}");
    }


    #region SPAWN FUNCTION
    private async UniTaskVoid WaveMonsterSpawn()
    {
        try
        {
            // 첫 1초 대기
            await UniTask.Delay(1000);

            float elapsedTime = 1f;

            // waveTimer초 동안 반복
            while (elapsedTime < waveTimer)
            {
                RandomSpawn();

                // 1초 대기
                await UniTask.Delay(TimeSpan.FromSeconds(Settings.spawnInterval));

                elapsedTime += Settings.spawnInterval; // 스폰 간격만큼 시간 더해주기
            }

            monsterSpawnEvent.CallWaveFinish(waveCount); // 웨이브 종료
        }
        catch (OperationCanceledException)
        {
            Debug.Log("WaveMonsterSpawn - Spawn Canceled!!!");
        }
    }

    private void RandomSpawn()
    {
        List<MonsterSpawnParameter> monsterParameters = currentWaveSpawnParameter.monsterSpawnParameters;

        // totalRatio : 몬스터의 스폰확률 전부 더한 값
        int totalRatio = monsterParameters.Sum(x => x.Ratio);
        // batchSpawnCount : 동시에 스폰할 몬스터의 수
        int batchSpawnCount = currentWaveSpawnParameter.batchSpawnCount;

        // 총 batchSpawnCount개의 몬스터 스폰
        for (int i = 0; i < batchSpawnCount; i++)
        {
            // 난수, 현재 몬스터의 스폰확률 누적값
            int randomNumber = UnityEngine.Random.Range(0, totalRatio);
            int ratioSum = 0;

            foreach (var monsterInfo in monsterParameters)
            {
                // 현재 순회중인 몬스터가 난수에 포함되면 스폰당첨
                ratioSum += monsterInfo.Ratio;
                if (randomNumber < ratioSum)
                {
                    Spawn(monsterInfo).Forget();
                    break;
                }
            }
        }
    }

    private async UniTaskVoid Spawn(MonsterSpawnParameter monsterInfo)
    {
        // 스폰되기 직전 바닥에 스포너 오브젝트 깔아주기
        GameObject spawner = ObjectPoolManager.Instance.Get("Spawner", RandomSpawnPosition(), Quaternion.identity);

        await UniTask.Delay(1000); // 1초 이후에 스폰

        ObjectPoolManager.Instance.Release(spawner);

        var monster = ObjectPoolManager.Instance.Get("Monster", spawner.transform.position, Quaternion.identity);
        monster.GetComponent<Monster>().InitializeEnemy(monsterInfo.monsterDetailsSO, waveCount);
    }

    private Vector2 RandomSpawnPosition()
    {
        // new 키워드로 계속 새 vector2를 생성하는 것보다 지역변수를 두고 대입하는게 낫다고 판단
        spawnPosition.x = UnityEngine.Random.Range(-Settings.stageBoundary, Settings.stageBoundary); 
        spawnPosition.y = UnityEngine.Random.Range(-Settings.stageBoundary, Settings.stageBoundary);
        return spawnPosition;
    }
    #endregion
}
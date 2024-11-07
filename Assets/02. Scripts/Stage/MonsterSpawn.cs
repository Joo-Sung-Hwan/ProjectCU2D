using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon.StructWrapping;

public class MonsterSpawn : MonoBehaviour
{
    private MonsterSpawnEvent monsterSpawnEvent;
    private List<WaveSpawnParameter> waveSpawnParameterList;
    private WaveSpawnParameter currentWaveSpawnParameter;
    private Vector2 spawnPosition;


    private void Awake()
    {
        monsterSpawnEvent = GetComponent<MonsterSpawnEvent>();
    }
    private void OnEnable()
    {
        monsterSpawnEvent.OnMonsterSpawn += MonsterSpawnEvent_OnMonsterSpawn;
        monsterSpawnEvent.OnWaveStart += MonsterSpawnEvent_OnWaveStart;
    }                                       
    private void OnDisable()                
    {                                       
        monsterSpawnEvent.OnMonsterSpawn -= MonsterSpawnEvent_OnMonsterSpawn;
        monsterSpawnEvent.OnWaveStart -= MonsterSpawnEvent_OnWaveStart;
    }


    private void MonsterSpawnEvent_OnMonsterSpawn(MonsterSpawnEvent @event, MonsterSpawnEventArgs args)
    {
        waveSpawnParameterList = args.stage.WaveSpawnParameter;

        monsterSpawnEvent.CallWaveStart(0); // ù ���̺���� ����
    }

    private void MonsterSpawnEvent_OnWaveStart(MonsterSpawnEvent @event, int waveCnt)
    {
        // ���� ���̺꿡 �ش��ϴ� ���̺� ���� �Ķ���� �޾ƿ���
        currentWaveSpawnParameter = waveSpawnParameterList[waveCnt];

        if (currentWaveSpawnParameter.isBossWave == true) return; // �������� ���Ŀ� ����

        WaveMonsterSpawn().Forget(); // UniTask ȣ��
    }

    private async UniTaskVoid WaveMonsterSpawn()
    {
        try
        {
            // ù 1�� ���
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            float elapsedTime = 1f;

            // 60�� ���� �ݺ�
            while (elapsedTime <= Settings.waveTimer)
            {
                RandomSpawn();

                // 1�� ���
                await UniTask.Delay(TimeSpan.FromSeconds(Settings.spawnInterval));

                elapsedTime += Settings.spawnInterval; // ���� ���ݸ�ŭ �ð� �����ֱ�
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("WaveMonsterSpawn - Spawn Canceled!!!");
        }
    }

    private void RandomSpawn()
    {
        List<MonsterSpawnParameter> monsterParameters = currentWaveSpawnParameter.monsterSpawnParameters;

        // totalRatio : ������ ����Ȯ�� ���� ���� ��
        int totalRatio = monsterParameters.Sum(x => x.Ratio);
        // batchSpawnCount : ���ÿ� ������ ������ ��
        int batchSpawnCount = currentWaveSpawnParameter.batchSpawnCount;

        // �� batchSpawnCount���� ���� ����
        for (int i = 0; i < batchSpawnCount; i++)
        {
            // ����, ���� ������ ����Ȯ�� ������
            int randomNumber = UnityEngine.Random.Range(0, totalRatio);
            int ratioSum = 0;

            foreach (var monsterInfo in monsterParameters)
            {
                // ���� ��ȸ���� ���Ͱ� ������ ���ԵǸ� ������÷
                ratioSum += monsterInfo.Ratio;
                if (randomNumber < ratioSum)
                {                   
                    var monster = ObjectPoolManager.Instance.Get("Monster", RandomSpawnPosition(), Quaternion.identity);
                    monster.GetComponent<Monster>().InitializeEnemy(monsterInfo.monsterDetailsSO);
                    break;
                }
            }
        }
    }

    private Vector2 RandomSpawnPosition()
    {
        // new Ű����� ��� �� vector2�� �����ϴ� �ͺ��� ���������� �ΰ� �����ϴ°� ���ٰ� �Ǵ�
        spawnPosition.x = UnityEngine.Random.Range(-Settings.stageBoundary, Settings.stageBoundary); 
        spawnPosition.y = UnityEngine.Random.Range(-Settings.stageBoundary, Settings.stageBoundary);
        return spawnPosition;
    }
}
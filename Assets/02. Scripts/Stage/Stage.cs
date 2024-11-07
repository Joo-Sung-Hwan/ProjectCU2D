using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public MonsterSpawnEvent MonsterSpawnEvent { get; private set; }
    public StageDetailsSO StageDetails { get; private set; } // ���� ������ ��� SO
    public List<WaveSpawnParameter> WaveSpawnParameter { get; private set; } // ���̺꺰 ��������


    private void Awake()
    {
        MonsterSpawnEvent = GetComponent<MonsterSpawnEvent>();
    }

    public void InitializedStage(StageDetailsSO stageDetails)
    {
        StageDetails = stageDetails;
        WaveSpawnParameter = stageDetails.waveSpawnParameter;

        MonsterSpawnEvent.CallMonsterSpawn(this); // �� �ʱ�ȭ�� ��� ����� �Ŀ� ���� �̺�Ʈ ȣ��
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private MonsterSpawnEvent monsterSpawnEvent;

    public StageDetailsSO StageDetails { get; private set; } // ���� ������ ��� SO
    public List<WaveSpawnParameter> WaveSpawnParameter { get; private set; } // ���̺꺰 ��������


    private void Awake()
    {
        monsterSpawnEvent = GetComponent<MonsterSpawnEvent>();
    }

    public void InitializedStage(StageDetailsSO stageDetails)
    {
        StageDetails = stageDetails;
        WaveSpawnParameter = stageDetails.waveSpawnParameter;

        monsterSpawnEvent.CallMonsterSpawn(this); // �� �ʱ�ȭ�� ��� ����� �Ŀ� ���� �̺�Ʈ ȣ��
    }
}

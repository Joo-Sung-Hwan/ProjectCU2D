using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public MonsterSpawnEvent MonsterSpawnEvent { get; private set; }
    public MonsterSpawn MonsterSpawner { get; private set; }
    public StageDetailsSO StageDetails { get; private set; } // 방의 정보가 담긴 SO
    public List<WaveSpawnParameter> WaveSpawnParameter { get; private set; } // 웨이브별 스폰정보


    private void Awake()
    {
        MonsterSpawnEvent = GetComponent<MonsterSpawnEvent>();
        MonsterSpawner = GetComponent<MonsterSpawn>();
    }

    public void InitializedStage(StageDetailsSO stageDetails)
    {
        StageDetails = stageDetails;
        WaveSpawnParameter = stageDetails.waveSpawnParameter;

        Debug.Log("Stage Init !!!!!!!!!!!!!!!! - is master??? : " + PhotonNetwork.IsMasterClient);

        MonsterSpawnEvent.CallMonsterSpawn(this); // 방 초기화가 모두 진행된 후에 스폰 이벤트 호출
    }
}

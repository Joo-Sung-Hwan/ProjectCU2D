using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StageManager : MonoBehaviourPunCallbacks
{
    public Stage CurrentStage {  get; private set; }



    public void CreateStage(StageDetailsSO stageDetails) // 스테이지 생성
    {
        PhotonNetwork.Instantiate("Stage1", Vector2.zero, Quaternion.identity);
        //Instantiate(stageDetails.stagePrefab, this.transform); // Instantiate에서 this.transform 이므로 자식으로 생성됨
        Stage instantiatedStage = GetComponentInChildren<Stage>(); // 생성된 자식오브젝트에서 Stage 컴포넌트 가져오기
        instantiatedStage.InitializedStage(stageDetails); // 생성된 스테이지 초기화

        CurrentStage = instantiatedStage;
    }
}

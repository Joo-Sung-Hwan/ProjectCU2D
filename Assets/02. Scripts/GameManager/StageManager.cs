using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StageManager : MonoBehaviourPunCallbacks
{
    public Stage CurrentStage {  get; private set; }



    public void CreateStage(StageDetailsSO stageDetails) // �������� ����
    {
        PhotonNetwork.Instantiate("Stage1", Vector2.zero, Quaternion.identity);
        //Instantiate(stageDetails.stagePrefab, this.transform); // Instantiate���� this.transform �̹Ƿ� �ڽ����� ������
        Stage instantiatedStage = GetComponentInChildren<Stage>(); // ������ �ڽĿ�����Ʈ���� Stage ������Ʈ ��������
        instantiatedStage.InitializedStage(stageDetails); // ������ �������� �ʱ�ȭ

        CurrentStage = instantiatedStage;
    }
}

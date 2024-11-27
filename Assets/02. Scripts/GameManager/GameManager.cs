using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Cinemachine;
using Firebase.Database;
using UnityEngine.Rendering.Universal;
using Photon.Pun;
using Photon.Realtime;
using GooglePlayGames.BasicApi;

public class GameManager : Singleton<GameManager>
{
    public event Action OnMainGameStarted;

    [SerializeField] private PlayerDetailsSO playerSO; // �ӽ÷� ����ȭ. ���Ŀ� �����ؾ���
    [SerializeField] private StageDetailsSO stageSO; // �ӽ÷� ����ȭ. ���Ŀ� �����ؾ���


    public Player Player { get; private set; }
    public UIController UIController;




    private void OnEnable()
    {
        // TEST CODE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
#if UNITY_EDITOR
        //CreateMainGameScene();
        //Time.timeScale = 0.25f;
#endif
    }

    public void CreateMainGameScene()
    {
        Player = PhotonNetwork.Instantiate("Player", Vector2.zero, Quaternion.identity).GetComponent<Player>();
        // �÷��̾� �ڱ��ڽ� init
        if (Player.GetComponent<PhotonView>().IsMine)
        {
            Player.InitializePlayer(playerSO);
        }
        // �����Ͱ� �������� ����, �ʱ�ȭ
        if (PhotonNetwork.IsMasterClient)
        {
            StageManager.Instance.CreateStage(stageSO);
        }

        //UIController = GameObject.FindWithTag("UIController").GetComponent<UIController>();
        UIController.InitializeUIController();

        // VCameraSetUp -> ī�޶� �¾����� �ʿ�
        OnMainGameStarted?.Invoke();
    }

}

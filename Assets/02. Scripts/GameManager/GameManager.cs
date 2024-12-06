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

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    public event Action OnMainGameStarted;

    public PlayerDetailsSO playerSO; // 임시로 직렬화. 추후에 변경해야함
    [SerializeField] private StageDetailsSO stageSO; // 임시로 직렬화. 추후에 변경해야함
    [SerializeField] private CinemachineVirtualCamera vcam;


    public Player Player { get; private set; }
    public UIController UIController;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CreateMainGameScene();
    }
    /*
    private void OnEnable()
    {
        // TEST CODE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
#if UNITY_EDITOR
        
        //Time.timeScale = 0.25f;
#endif
    }*/

    public void CreateMainGameScene()
    {
        Player = PhotonNetwork.Instantiate("Player", Vector2.zero, Quaternion.identity).GetComponent<Player>();
        //Player.InitializePlayer(playerSO);
        // 마스터가 스테이지 생성, 초기화
        if (PhotonNetwork.IsMasterClient)
        {
            Player.GetComponent<PhotonView>().RPC("InitializePlayer", RpcTarget.All, 0);
            StageManager.Instance.CreateStage(stageSO);
        }
        else
        {
            Player.GetComponent<PhotonView>().RPC("InitializePlayer", RpcTarget.All, 1);
        }

        //UIController = GameObject.FindWithTag("UIController").GetComponent<UIController>();
        UIController.InitializeUIController();

        // VCameraSetUp -> 카메라 셋업에서 필요
        //OnMainGameStarted?.Invoke();
        vcam.Follow = Player.transform;
    }

}

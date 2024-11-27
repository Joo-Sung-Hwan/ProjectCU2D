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

    public PlayerDetailsSO playerSO; // �ӽ÷� ����ȭ. ���Ŀ� �����ؾ���
    [SerializeField] private StageDetailsSO stageSO; // �ӽ÷� ����ȭ. ���Ŀ� �����ؾ���


    public Player Player { get; private set; }
    public UIController UIController;

    PhotonView pv;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        pv = GetComponent<PhotonView>();
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
        Player = PhotonNetwork.Instantiate("Player", Vector2.zero, Quaternion.identity).GetComponent<Player>(); Player.InitializePlayer(playerSO);
        //Player.InitializePlayer(playerSO);
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

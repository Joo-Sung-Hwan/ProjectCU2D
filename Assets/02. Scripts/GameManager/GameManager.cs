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

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerDetailsSO playerSO; // �ӽ÷� ����ȭ. ���Ŀ� �����ؾ���
    [SerializeField] private StageDetailsSO stageSO; // �ӽ÷� ����ȭ. ���Ŀ� �����ؾ���

    public Player Player => player;


    protected override void Awake()
    {
        base.Awake();

        InstantiatePlayer();
    }

    private void Start()
    {
        StageManager.Instance.CreateStage(stageSO);
    }

    private void InstantiatePlayer()
    {
        player.InitializePlayer(playerSO);
    }
}

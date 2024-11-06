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
    [SerializeField] private PlayerDetailsSO playerSO; // 임시로 직렬화. 추후에 변경해야함

    public Player Player => player;


    protected override void Awake()
    {
        base.Awake();

        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        player.InitializePlayer(playerSO);
    }
}

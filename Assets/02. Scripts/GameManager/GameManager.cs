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
    public event Action OnMainGameStarted;

    [SerializeField] private PlayerDetailsSO playerSO; // 임시로 직렬화. 추후에 변경해야함
    [SerializeField] private StageDetailsSO stageSO; // 임시로 직렬화. 추후에 변경해야함

    public Player Player { get; private set; }



    private void Start()
    {
        // TEST CODE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
#if UNITY_EDITOR
        CreateMainGameScene();
        //Time.timeScale = 0.25f;
#endif
    }

    public void CreateMainGameScene()
    {
        Player = Instantiate(playerSO.player, Vector2.zero, Quaternion.identity);
        Player.InitializePlayer(playerSO);

        StageManager.Instance.CreateStage(stageSO);

        OnMainGameStarted?.Invoke();
    }
}

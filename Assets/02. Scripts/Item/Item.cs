using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;


public class Item : MonoBehaviour  // 아이템에 연결할 클래스
{
    private static event Action OnMagnet;

    private SpriteRenderer spriteRenderer;
    private ParticleSystem particle;
    private EItemType itemType;
    private int gainExp;

    // 비활성화 되어도 유니태스크는 계속 실행되므로 관리해줘야함@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    private CancellationTokenSource disableCancellation = new CancellationTokenSource();
    private Player player;
    private Rigidbody2D rigid;
    private Vector2 moveVec;
    private bool isFirstTrigger = false;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
        rigid = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.Player;
    }

    private void OnEnable()
    {
        // 활성화 될때마다 토큰 새롭게 초기화시켜주기
        disableCancellation?.Dispose();
        disableCancellation = new CancellationTokenSource();

        OnMagnet += Item_OnMagnet;
    }


    private void OnDisable()
    {
        // 비활성화 될때 유니태스크 취소명령
        disableCancellation.Cancel();

        OnMagnet -= Item_OnMagnet;
    }

    private void Item_OnMagnet()
    {
        DetectItem();
    }


    public void InitializeItem(ItemDetailsSO data)
    {
        spriteRenderer.sprite = data.ItemSprite;

        ParticleSystem.MainModule main = particle.main; // 파티클 시스템의 MainModule로 색상변경 가능
        main.startColor = UtilitieHelper.GetGradeColor(data.itemGrade);

        itemType = data.itemType;
        gainExp = (int)data.itemGrade; // 해당 등급에 맞는 경험치 획득 (등급마다 경험치 정해져있음)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 경험치 획득 코드 구현
        if (!isFirstTrigger && (Settings.itemDetectorLayer & (1 << collision.gameObject.layer)) != 0)
        {
            if (itemType == EItemType.Magnet)
                OnMagnet.Invoke();

            DetectItem();
        }

        if (isFirstTrigger && (Settings.itemPickUpLayer & (1 << collision.gameObject.layer)) != 0)
        {
                ObjectPoolManager.Instance.Release(gameObject, "Item");
        }
    }

    public void DetectItem()
    {
        isFirstTrigger = true;
        rigid.simulated = false;
        // 플레이어 위치로부터의 방향점
        var outsideDir = (transform.position - player.transform.position).normalized;

        // 바깥쪽으로 이동할 목표 위치 계산 (현재 위치에서 방향 벡터의 1.5배 거리)
        var outsideDesiredPos = transform.position + outsideDir * 2f;

        // 바깥쪽으로 천천히 이동 (0.5초)
        transform.DOMove(outsideDesiredPos, 0.5f).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                rigid.simulated = true;
                MoveToPlayer().Forget();
            });


        // 시퀀스 실행 및 완료 대기
        //await sequence.Play();
    }

    private async UniTask MoveToPlayer()
    {
        while (true)
        {        
            moveVec = (player.transform.position - transform.position).normalized;
            rigid.velocity = moveVec * 13f;     
        
            await UniTask.Delay(100, cancellationToken:disableCancellation.Token);
        }
    }
}

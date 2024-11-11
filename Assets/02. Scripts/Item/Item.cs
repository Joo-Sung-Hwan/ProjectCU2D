using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;


public class Item : MonoBehaviour  // �����ۿ� ������ Ŭ����
{
    private static event Action OnMagnet;

    private SpriteRenderer spriteRenderer;
    private ParticleSystem particle;
    private EItemType itemType;
    private int gainExp;

    private Player player;
    private Rigidbody2D rigid;
    private Vector2 moveVec;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
        rigid = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.Player;
    }

    private void OnEnable()
    {
        OnMagnet += Item_OnMagnet;
    }


    private void OnDisable()
    {
        OnMagnet -= Item_OnMagnet;
    }

    private void Item_OnMagnet()
    {
        MoveToPlayer().Forget();
    }


    public void InitializeItem(ItemDetailsSO data)
    {
        spriteRenderer.sprite = data.ItemSprite;

        ParticleSystem.MainModule main = particle.main; // ��ƼŬ �ý����� MainModule�� ���󺯰� ����
        main.startColor = UtilitieHelper.GetGradeColor(data.itemGrade);

        itemType = data.itemType;
        gainExp = (int)data.itemGrade; // �ش� ��޿� �´� ����ġ ȹ�� (��޸��� ����ġ ����������)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����ġ ȹ�� �ڵ� ����

        if (itemType == EItemType.Magnet)
            OnMagnet.Invoke();

        ObjectPoolManager.Instance.Release(gameObject, "Item");
    }

    private async UniTask MoveToPlayer()
    {
        while (true)
        {        
            moveVec = (player.transform.position - transform.position).normalized;
            rigid.velocity = moveVec * 13f;     
        
            await UniTask.Delay(100);
        }
    }
}

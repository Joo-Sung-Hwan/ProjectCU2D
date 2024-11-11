using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using DG.Tweening;


public class Item : MonoBehaviour  // �����ۿ� ������ Ŭ����
{
    private SpriteRenderer spriteRenderer;
    private ParticleSystem particle;
    private int gainExp;

    private Player player;
    private Rigidbody2D rigid;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
        rigid = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.Player;
    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(player.transform.position);
            
    }


    public void InitializeItem(ItemDetailsSO data)
    {
        spriteRenderer.sprite = data.ItemSprite;

        ParticleSystem.MainModule main = particle.main; // ��ƼŬ �ý����� MainModule�� ���󺯰� ����
        main.startColor = data.gradeColor;

        gainExp = (int)data.itemGrade; // �ش� ��޿� �´� ����ġ ȹ�� (��޸��� ����ġ ����������)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����ġ ȹ�� �ڵ� ����
        Debug.Log($"EXP Gain!! + {gainExp}");

        ObjectPoolManager.Instance.Release(gameObject, "Item");
    }
}

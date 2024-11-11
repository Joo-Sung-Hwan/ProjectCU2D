using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using DG.Tweening;


public class Item : MonoBehaviour  // 아이템에 연결할 클래스
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

        ParticleSystem.MainModule main = particle.main; // 파티클 시스템의 MainModule로 색상변경 가능
        main.startColor = data.gradeColor;

        gainExp = (int)data.itemGrade; // 해당 등급에 맞는 경험치 획득 (등급마다 경험치 정해져있음)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 경험치 획득 코드 구현
        Debug.Log($"EXP Gain!! + {gainExp}");

        ObjectPoolManager.Instance.Release(gameObject, "Item");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private FixedJoystick joy;
    [SerializeField] private float speed = 10f;

    private Player player;
    private Rigidbody2D rigid;
    private Vector2 moveVec;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();

    }

    private void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        moveVec = joy.Direction * speed;

        rigid.velocity = moveVec;
    }

    private void Attack()
    {
        for (int i = 0; i < player.WeaponList.Count; ++i) // 플레이어의 무기 리스트 각각 공격이벤트 호출
        {
            player.WeaponAttackEvent.CallWeaponAttackEvent(player.WeaponList[i], i);
        }
    }
}
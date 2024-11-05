using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private FixedJoystick joy;
    [SerializeField] private float speed = 100f;

    private Rigidbody2D rigid;
    private Vector2 moveVec;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveVec = joy.Direction * speed * Time.fixedDeltaTime;

        rigid.velocity = moveVec;
    }
}
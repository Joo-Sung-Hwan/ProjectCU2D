using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Weapon : MonoBehaviour
{
    public string WeaponName { get; private set; }
    public WeaponType WeaponType { get; private set; }
    public Sprite WeaponSprite { get; private set; }
    public WeaponDetailsSO WeaponDetail { get; private set; }
    public Player Player { get; set; } // ���⸦ ������ �÷��̾�


    // ������ ���� (�������Ͽ� ���� ��� ����)
    public int WeaponLevel { get; private set; } // ���� ����
    public int WeaponDamage { get; private set; } // ������
    public int WeaponCriticChance { get; private set; } // ġ��Ÿ Ȯ�� (%)
    public int WeaponCriticDamage { get; private set; } // ġ��Ÿ ���� (%)
    public float WeaponFireRate { get; private set; } // ���ݼӵ�
    public float WeaponRange { get; private set; } // ��Ÿ�
    public float WeaponAmmoSpeed { get; private set; } // ź�ӵ� (���Ÿ� ���⸸ ����)
    public float WeaponKnockback { get; private set; } // �˹�Ÿ�


    // �ǽð����� �ΰ��ӿ��� ����ϸ鼭 �ٲ�� ����
    public float WeaponFireRateTimer; // ���� ��Ÿ��


    private void Update()
    {
        // ����ӵ� 
        if (WeaponFireRateTimer > 0f)
            WeaponFireRateTimer -= Time.deltaTime;
        else WeaponFireRateTimer = WeaponFireRate;

    }

    public void InitializeWeapon(WeaponDetailsSO weaponDetails) // ���� �ʱ�ȭ
    {
        WeaponDetail = weaponDetails; // �Ҹ�,����Ʈ � �����ϱ� ����
        WeaponName = weaponDetails.weaponName;
        WeaponType = weaponDetails.weaponType;
        WeaponSprite = weaponDetails.weaponSprite;

        WeaponLevel = 1;
        WeaponDamage = weaponDetails.weaponBaseDamage;
        WeaponCriticChance = weaponDetails.weaponCriticChance;
        WeaponCriticDamage = weaponDetails.weaponCriticDamage;
        WeaponFireRate = weaponDetails.weaponFireRate;
        WeaponRange = weaponDetails.weaponRange;
        WeaponAmmoSpeed = weaponDetails.weaponAmmoSpeed;
        WeaponKnockback = weaponDetails.weaponKnockback;

        WeaponFireRateTimer = weaponDetails.weaponFireRate; // ���ݼӵ� �ʱ�ȭ
    }

    /// ���� ������ �Լ� �� ���� �ʿ�

    public bool DetectMonster(out Vector2 direction)
    {
        var playerPosition = Player.transform.position;
        var colliders = Physics2D.OverlapCircleAll(playerPosition, WeaponRange, Settings.monsterLayer);

        if (colliders.Length == 0)
        {
            direction = Vector2.zero;
            return false;
        }

        GameObject monster = null;
        float dist = Mathf.Infinity;
        for (int i = 0; i < colliders.Length; i++)
        {
            // �ܼ� �Ÿ����̹Ƿ� ��귮�� ���� sqrMagnitude ���
            float distance = (colliders[i].transform.position - playerPosition).sqrMagnitude;
            if (dist > distance)
            {
                // ���̾� �˻縦 ���� ������ �����̹Ƿ� GetComponent ����
                monster = colliders[i].gameObject;
                dist = distance;
            }
        }

        direction = (monster.transform.position - playerPosition).normalized;
        return true;
    }
}

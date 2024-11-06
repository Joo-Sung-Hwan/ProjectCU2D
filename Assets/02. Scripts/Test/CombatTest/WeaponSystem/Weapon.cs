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
    public Player Player { get; set; } // 무기를 소유한 플레이어


    // 무기의 스탯 (레벨업하여 스탯 상승 가능)
    public int WeaponLevel { get; private set; } // 무기 레벨
    public int WeaponDamage { get; private set; } // 데미지
    public int WeaponCriticChance { get; private set; } // 치명타 확률 (%)
    public int WeaponCriticDamage { get; private set; } // 치명타 피해 (%)
    public float WeaponFireRate { get; private set; } // 공격속도
    public float WeaponRange { get; private set; } // 사거리
    public float WeaponAmmoSpeed { get; private set; } // 탄속도 (원거리 무기만 적용)
    public float WeaponKnockback { get; private set; } // 넉백거리


    // 실시간으로 인게임에서 사격하면서 바뀌는 변수
    public float WeaponFireRateTimer; // 공격 쿨타임


    private void Update()
    {
        // 연사속도 
        if (WeaponFireRateTimer > 0f)
            WeaponFireRateTimer -= Time.deltaTime;
        else WeaponFireRateTimer = WeaponFireRate;

    }

    public void InitializeWeapon(WeaponDetailsSO weaponDetails) // 무기 초기화
    {
        WeaponDetail = weaponDetails; // 소리,이펙트 등에 접근하기 위해
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

        WeaponFireRateTimer = weaponDetails.weaponFireRate; // 공격속도 초기화
    }

    /// 무기 레벨업 함수 등 구현 필요

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
            // 단순 거리비교이므로 계산량이 적은 sqrMagnitude 사용
            float distance = (colliders[i].transform.position - playerPosition).sqrMagnitude;
            if (dist > distance)
            {
                // 레이어 검사를 통해 무조건 몬스터이므로 GetComponent 생략
                monster = colliders[i].gameObject;
                dist = distance;
            }
        }

        direction = (monster.transform.position - playerPosition).normalized;
        return true;
    }
}

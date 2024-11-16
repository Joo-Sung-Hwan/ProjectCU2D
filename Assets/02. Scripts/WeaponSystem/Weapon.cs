using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class Weapon : MonoBehaviour
{
    private WeaponTypeDetailsSO upgradeType;

    public WeaponDetailsSO WeaponDetails { get; private set; }
    public string WeaponName { get; private set; }
    public WeaponTypeDetailsSO WeaponType { get; private set; }
    public WeaponDetectorSO DetectorType { get; private set; }
    public string WeaponParticle { get; private set; }
    public Sprite WeaponSprite { get; private set; }
    public Player Player { get; set; } // 무기를 소유한 플레이어


    // 무기의 스탯 (레벨업하여 스탯 상승 가능)
    public int WeaponLevel { get; private set; } // 무기 레벨
    public int WeaponDamage { get; private set; } // 데미지
    public int WeaponCriticChance { get; private set; } // 치명타 확률 (%)
    public int WeaponCriticDamage { get; private set; } // 치명타 피해 (%)
    public float WeaponFireRate { get; private set; } // 공격속도
    public float WeaponRange { get; private set; } // 사거리
    public int WeaponKnockback { get; private set; } // 넉백거리


    // 실시간으로 인게임에서 사격하면서 바뀌는 변수
    public float WeaponFireRateTimer; // 공격 쿨타임


    private void Update()
    {
        // 연사속도 
        if (WeaponFireRateTimer > 0f)
            WeaponFireRateTimer -= Time.deltaTime;
        else WeaponFireRateTimer = WeaponFireRate;


        if (Input.GetKeyUp(KeyCode.E))
        {
            UpgrageWeapon();
        }
    }

    public void InitializeWeapon(WeaponDetailsSO weaponDetails) // 무기 초기화
    {
        WeaponDetails = weaponDetails; // 소리,이펙트 등에 접근하기 위해
        WeaponName = weaponDetails.weaponName;
        WeaponType = weaponDetails.weaponType;
        upgradeType = weaponDetails.upgradeType;
        DetectorType = weaponDetails.detectorType;
        WeaponParticle = weaponDetails.weaponParticle.ToString();
        WeaponSprite = weaponDetails.weaponSprite;

        WeaponLevel = 1;
        WeaponDamage = weaponDetails.weaponBaseDamage;
        WeaponCriticChance = weaponDetails.weaponCriticChance;
        WeaponCriticDamage = weaponDetails.weaponCriticDamage;
        WeaponFireRate = weaponDetails.weaponFireRate;
        WeaponRange = weaponDetails.weaponRange;
        WeaponKnockback = weaponDetails.weaponKnockback;

        WeaponFireRateTimer = weaponDetails.weaponFireRate; // 공격속도 초기화
    }

    /// 무기 레벨업 함수 등 구현 필요

    #region TEST
    public void UpgrageWeapon()
    {
        WeaponType = upgradeType;
    }
    #endregion
}

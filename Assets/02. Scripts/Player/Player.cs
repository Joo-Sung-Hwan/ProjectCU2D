using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerDetailsSO playerDetails; // 캐릭터의 종류가 나뉘는지에 따라 필요여부 달라짐
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private CircleCollider2D circleRange; // 자석범위
    private PlayerStat stat; // 캐릭터 스탯
    private PlayerCtrl ctrl; // 캐릭터 컨트롤러

    #region PLAYER EVENT
    public WeaponAttackEvent WeaponAttackEvent { get; private set; }
    private WeaponAttack weaponAttack;
    #endregion

    public List<Weapon> WeaponList { get; private set; } // 무기 리스트


    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleRange = GetComponentInChildren<CircleCollider2D>();
        ctrl = GetComponent<PlayerCtrl>();

        WeaponAttackEvent = GetComponent<WeaponAttackEvent>();
        weaponAttack = GetComponent<WeaponAttack>();
    }

    private void Start()
    {
        
    }

    public void InitializePlayer(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;

        // spriteRenderer.sprite = playerDetails.playerSprite; // 아직 캐릭터 스프라이트가 준비안됨
        //animator.runtimeAnimatorController = playerDetails.runtimeAnimatorController;
        WeaponList = new List<Weapon>(Settings.maxWeaponCount);
        AddWeaponToPlayer(playerDetails.playerStartingWeapon);

        stat = new PlayerStat(playerDetails);
    }

    public Weapon AddWeaponToPlayer(WeaponDetailsSO weaponDetails)
    {
        // 추가할 무기 초기화
        Weapon playerWeapon = gameObject.AddComponent<Weapon>();
        playerWeapon.InitializeWeapon(weaponDetails);
        playerWeapon.Player = this;

        // 무기 추가되면서 캐릭터 스탯 반영
        //playerWeapon.ChangeWeaponStat(PlayerStatType.BaseDamage, stat.baseDamage, false);
        //playerWeapon.ChangeWeaponStat(PlayerStatType.CriticChance, stat.criticChance, false);
        //playerWeapon.ChangeWeaponStat(PlayerStatType.CriticDamage, stat.criticDamage, false);

        WeaponList.Add(playerWeapon); // 무기 리스트에 추가

        Debug.Log($"Add Weapon!! - {weaponDetails.weaponName} , {weaponDetails.weaponBaseDamage}");

        return playerWeapon;
    }
}

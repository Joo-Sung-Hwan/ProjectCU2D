using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerDetailsSO playerDetails; // ĳ������ ������ ���������� ���� �ʿ俩�� �޶���
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private CircleCollider2D circleRange; // �ڼ�����
    private PlayerStat stat; // ĳ���� ����
    private PlayerCtrl ctrl; // ĳ���� ��Ʈ�ѷ�

    #region PLAYER EVENT
    public WeaponAttackEvent WeaponAttackEvent { get; private set; }
    private WeaponAttack weaponAttack;
    #endregion

    public List<Weapon> WeaponList { get; private set; } // ���� ����Ʈ


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

        // spriteRenderer.sprite = playerDetails.playerSprite; // ���� ĳ���� ��������Ʈ�� �غ�ȵ�
        //animator.runtimeAnimatorController = playerDetails.runtimeAnimatorController;
        WeaponList = new List<Weapon>(Settings.maxWeaponCount);
        AddWeaponToPlayer(playerDetails.playerStartingWeapon);

        stat = new PlayerStat(playerDetails);
    }

    public Weapon AddWeaponToPlayer(WeaponDetailsSO weaponDetails)
    {
        // �߰��� ���� �ʱ�ȭ
        Weapon playerWeapon = gameObject.AddComponent<Weapon>();
        playerWeapon.InitializeWeapon(weaponDetails);
        playerWeapon.Player = this;

        // ���� �߰��Ǹ鼭 ĳ���� ���� �ݿ�
        //playerWeapon.ChangeWeaponStat(PlayerStatType.BaseDamage, stat.baseDamage, false);
        //playerWeapon.ChangeWeaponStat(PlayerStatType.CriticChance, stat.criticChance, false);
        //playerWeapon.ChangeWeaponStat(PlayerStatType.CriticDamage, stat.criticDamage, false);

        WeaponList.Add(playerWeapon); // ���� ����Ʈ�� �߰�

        Debug.Log($"Add Weapon!! - {weaponDetails.weaponName} , {weaponDetails.weaponBaseDamage}");

        return playerWeapon;
    }
}

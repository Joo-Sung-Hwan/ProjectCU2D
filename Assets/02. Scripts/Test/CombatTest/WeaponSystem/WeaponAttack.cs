using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] Transform weaponShootPosition; // 사격 포인트
    private WeaponAttackEvent weaponAttackEvent;
    private Weapon weapon;


    private void Awake()
    {
        weaponAttackEvent = GetComponent<WeaponAttackEvent>();
    }
    private void OnEnable()
    {
        weaponAttackEvent.OnWeaponAttack += WeaponAttackEvent_OnWeaponAttack;
    }
    private void OnDisable()
    {
        weaponAttackEvent.OnWeaponAttack -= WeaponAttackEvent_OnWeaponAttack;
    }



    private void WeaponAttackEvent_OnWeaponAttack(WeaponAttackEvent arg1, WeaponAttackEventArgs args)
    {
        weapon = args.weapon;
        OnWeaponAttack(args);
    }

    private void OnWeaponAttack(WeaponAttackEventArgs weaponAttackEventArgs)
    {
        // 공격속도(쿨타임) 만족할때 공격 시도
        if (weapon.WeaponFireRateTimer > 0f)
            return;

        weapon.WeaponType.Attack(weapon);
    }
}

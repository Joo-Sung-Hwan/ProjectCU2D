using ExitGames.Client.Photon.StructWrapping;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PP_", menuName = "Scriptable Objects/Weapon/Projectile/PP")]
public class ProjectilePatternSO : ScriptableObject
{
    // 발사할 투사체의 개수, 간격 등등 다양한 패턴에 필요한 변수들? -> 상속받아서
    // 지금은 추상클래스가 아닌 콘크리트 클래스로 우선 테스트

    // 투사체 오브젝트
    private GameObject projectileObject;


    public void ProjectileLaunch(ProjectileDetailsSO projectileDetails, Vector2 direction, Weapon weapon)
    {
        // 발사 명령이 떨어지면 풀에서 투사체 활성화

        //weapon.Player.WeaponTransform.GetWeaponTransform(weapon, out Vector2 pos, out Quaternion rot);
        projectileObject = ObjectPoolManager.Instance.Get("PlayerProjectile", weapon.Player.WeaponTransform.GetWeaponTransform(weapon));
        // 투사체 데이터SO랑 방향, 무기 정보 넣어서 초기화
        projectileObject.GetComponent<Projectile>().InitializeProjectile(projectileDetails, direction, weapon);
    }
}
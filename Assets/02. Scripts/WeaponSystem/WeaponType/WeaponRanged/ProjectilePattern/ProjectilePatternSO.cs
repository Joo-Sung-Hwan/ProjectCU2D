using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PP_", menuName = "Scriptable Objects/Weapon/Projectile/PP")]
public class ProjectilePatternSO : ScriptableObject
{
    // �߻��� ����ü�� ����, ���� ��� �پ��� ���Ͽ� �ʿ��� ������? -> ��ӹ޾Ƽ�
    // ������ �߻�Ŭ������ �ƴ� ��ũ��Ʈ Ŭ������ �켱 �׽�Ʈ

    // ����ü ������Ʈ
    private GameObject projectileObject;


    public void ProjectileLaunch(ProjectileDetailsSO projectileDetails, Vector2 direction, Weapon weapon)
    {
        // �߻� ����� �������� Ǯ���� ����ü Ȱ��ȭ
        projectileObject = ObjectPoolManager.Instance.Get("bullet", weapon.transform);
        // ����ü ������SO�� ����, ���� ���� �־ �ʱ�ȭ
        projectileObject.GetComponent<Projectile>().InitializeProjectile(projectileDetails, direction, weapon);
    }
}
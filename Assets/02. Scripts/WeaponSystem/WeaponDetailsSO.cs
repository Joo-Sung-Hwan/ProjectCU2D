using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetails_", menuName = "Scriptable Objects/Weapon/Weapon")]
public class WeaponDetailsSO : ScriptableObject
{
    [Header("weapon base details")]
    public string weaponName;
    public WeaponTypeDetailsSO weaponType; // 근거리 , 원거리
    public WeaponTypeDetailsSO upgradeType; // 무기의 업그레이드
    public WeaponDetectorSO detectorType; // 무기 감지타입 (먼 적부터, 체력 낮은 적부터 등)
    public Sprite weaponSprite;

    [Header("weapon base stats")]
    public int weaponBaseDamage = 20;   // 기본데미지
    public int weaponCriticChance = 10; // 치명타 확률 (%)
    public int weaponCriticDamage = 150; // 치명타 피해 (%)
    public float weaponFireRate = 0.5f; // 공격속도
    public int weaponRange = 0; // 사거리
    public int weaponKnockback = 0; // 넉백거리

    [Header("weapon configuration")]
    public EParticleType weaponParticle;
    //public List<GameObject> weaponAmmo;
    //[TextArea] public string upgradeDescription;
    //public bool isTrail; // Trail 렌더러 여부
    //public Material ammoTrailMaterial;
    //public float ammoTrailStartWidth;
    //public float ammoTrailEndWidth;
    //public float ammoTrailTime;
    //public SoundEffectSO weaponFiringSoundEffect;
}

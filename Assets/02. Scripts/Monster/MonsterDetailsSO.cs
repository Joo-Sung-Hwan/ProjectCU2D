using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MonsterDetails_", menuName = "Scriptable Objects/Monster/Monster Details")]
public class MonsterDetailsSO : IdentifiedObject
{
    [Header("Base Monster Details")]
    public string enemyName;
    public Sprite sprite; // 적의 스프라이트
    public Color spriteColor; // 스프라이트 색상
    public RuntimeAnimatorController runtimeAnimatorController; // 적의 애니메이터

    [Header("Base Monster Stats")]
    public MonsterMovementSO movementType; // 이동타입 (돌진, 랜덤, 이동)
    public MonsterAttackSO attackType; // 공격타입 (투사체, 함정 등)
    public float speed = 3f;
    public float chaseDistance = 50f; // 플레이어와의 최대 간격
    public float baseDamage = 10; // 플레이어 접촉 데미지 포함 모든 데미지 (투사체,함정)
    public float maxHp = 10; // 최대체력
    public float waveBonusHp = 1; // 웨이브마다 증가하는 최대체력
    public float waveBonusDmg = 1; // 웨이브마다 증가하는 공격력

    [Header("ETC")]
    public ItemDetailsSO itemDetails;
    public Material enemyStandardMaterial;
    public Material enemyHitMaterial;
    //public float enemyMaterializeTime;
    //public Shader enemyMaterializeShader;
    //public Color enemyMaterializeColor;
}

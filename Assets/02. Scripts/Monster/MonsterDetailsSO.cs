using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MonsterDetails_", menuName = "Scriptable Objects/Monster/Monster Details")]
public class MonsterDetailsSO : ScriptableObject
{
    [Header("Base Monster Details")]
    public string enemyName;
    public Sprite sprite; // ���� ��������Ʈ
    public Color spriteColor; // ��������Ʈ ����
    public RuntimeAnimatorController runtimeAnimatorController; // ���� �ִϸ�����

    [Header("Base Monster Stats")]
    public MonsterMovementSO movementType; // �̵�Ÿ�� (����, ����, �̵�)
    public float speed = 3f;
    public float chaseDistance = 50f; // �÷��̾���� �ִ� ����
    public int contactDamageAmount = 10; // �÷��̾� ���� ������
    public float maxHp = 10; // �ִ�ü��

    [Header("Material")]
    public Material enemyStandardMaterial;
    public Material enemyHitMaterial;
    //public float enemyMaterializeTime;
    //public Shader enemyMaterializeShader;
    //public Color enemyMaterializeColor;
}

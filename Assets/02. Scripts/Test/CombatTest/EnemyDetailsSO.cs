using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/Enemy Details")]
public class EnemyDetailsSO : ScriptableObject
{
    [Header("Base Enemy Details")]
    public string enemyName;
    public Sprite sprite; // ���� ��������Ʈ
    public Color spriteColor; // ��������Ʈ ����
    public RuntimeAnimatorController runtimeAnimatorController; // ���� �ִϸ�����

    [Header("Base Enemy Stats")]
    public int speed; // �̵��ӵ�
    public float chaseDistance = 50f; // �÷��̾���� �ִ� ����
    public int contactDamageAmount = 10; // �÷��̾� ���� ������
    public float maxHp = 10; // �ִ�ü��

    //[Header("Material")]
    //public Material enemyStandardMaterial;
    //public float enemyMaterializeTime;
    //public Shader enemyMaterializeShader;
    //public Color enemyMaterializeColor;
}

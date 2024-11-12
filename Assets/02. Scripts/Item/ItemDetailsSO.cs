using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDetails_", menuName = "Scriptable Objects/Item/Item Details")]
public class ItemDetailsSO : ScriptableObject
{
    public Sprite ItemSprite; // ������ ��������Ʈ
    public EGrade itemGrade; // ���
    public EItemType itemType;
}
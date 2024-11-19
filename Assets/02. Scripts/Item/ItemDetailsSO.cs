using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDetails_", menuName = "Scriptable Objects/Item/Item Details")]
public class ItemDetailsSO : IdentifiedObject
{
    public Sprite ItemSprite; // 아이템 스프라이트
    public EGrade itemGrade; // 등급
    public EItemType itemType; // 아이템 타입 (경험치,자석,폭탄)
}
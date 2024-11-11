using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDetails_", menuName = "Scriptable Objects/Item/Item Details")]
public class ItemDetailsSO : ScriptableObject
{
    public Sprite ItemSprite; // 아이템 스프라이트
    public ItemGrade itemGrade; // 등급
    public Color gradeColor; // 등급별 색상
}
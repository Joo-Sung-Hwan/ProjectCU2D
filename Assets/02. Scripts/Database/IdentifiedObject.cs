using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdentifiedObject : ScriptableObject, ICloneable
{
    [SerializeField,HideInInspector] private int id = -1;

    public int ID => id;


    // ICloneable 인터페이스의 Clone 함수 구현 => 객체를 복사해서 생성
    public virtual object Clone() => Instantiate(this);
}
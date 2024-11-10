using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusEffectSO : ScriptableObject
{
    // 무기에 붙을 추가효과 (디버프, 도트데미지 등)

    public virtual void Start() { }
    public abstract void Apply(Monster monster);
    public virtual void Release() { }
}

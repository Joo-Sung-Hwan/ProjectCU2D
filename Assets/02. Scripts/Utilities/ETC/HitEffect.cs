using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour 
{
    private string effectName;


    private void OnEnable()
        => ReleaseEffect().Forget();

    public void InitializeHitEffect(string effectName)
        => this.effectName = effectName;

    private async UniTask ReleaseEffect()
    {
        await UniTask.Delay(1000);


        if (this.gameObject.activeSelf)
            ObjectPoolManager.Instance.Release(gameObject, effectName);
    }
}

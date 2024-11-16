using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class HitText : MonoBehaviour
{
    private TextMeshPro txtDamage;
    private Transform rect;


    private void Awake()
    {
        txtDamage = GetComponent<TextMeshPro>();
        rect = GetComponent<Transform>();
    }

    public void InitializeHitText(int damageAmount, bool isCritic = false, bool isDodge = false)
    {
        if (isDodge)
        {
            txtDamage.text = "È¸ÇÇ";
            txtDamage.color = Color.white;
            txtDamage.fontSize = 4f;

            rect.DOMoveY(0.6f, 0.6f).SetEase(Ease.InOutQuad).SetRelative()
                .OnComplete(() => ObjectPoolManager.Instance.Release(gameObject, "HitText"));

            return;
        }

        txtDamage.text = damageAmount.ToString();

        if (isCritic)
        {
            txtDamage.color = Settings.critical;
            txtDamage.fontSize = 5;

            rect.DOMoveY(0.8f, 0.8f).SetEase(Ease.InOutQuad).SetRelative()
                .OnComplete(() => ObjectPoolManager.Instance.Release(gameObject, "HitText"));
        }
        else
        {
            txtDamage.color = Color.white;
            txtDamage.fontSize = 4f;

            rect.DOMoveY(0.6f, 0.6f).SetEase(Ease.InOutQuad).SetRelative()
                .OnComplete(() => ObjectPoolManager.Instance.Release(gameObject, "HitText"));
        }
    }
}

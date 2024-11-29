using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Photon.Pun;

public class HitTextUI : MonoBehaviourPun
{
    private TextMeshPro txtDamage;
    private Transform trans;


    private void Awake()
    {
        txtDamage = GetComponent<TextMeshPro>();
        trans = GetComponent<Transform>();
    }

    [PunRPC]
    public void InitializeHitText(int damageAmount, bool isCritic, bool isDodge)
    {
        if (isDodge)
        {
            txtDamage.text = "È¸ÇÇ";

            HitTextMove(Color.white, 4f, 0.6f);

            return;
        }

        txtDamage.text = damageAmount.ToString();

        if (isCritic)
            HitTextMove(Settings.critical, 5f, 0.8f);
        else
            HitTextMove(Color.white, 4f, 0.6f);
    }

    private void HitTextMove(Color color, float fontSize, float yPos)
    {
        txtDamage.color = color;
        txtDamage.fontSize = fontSize;

        trans.DOMoveY(yPos, yPos).SetEase(Ease.InOutQuad).SetRelative()
            .OnComplete(() => ObjectPoolManager.Instance.Release(gameObject));
    }
}

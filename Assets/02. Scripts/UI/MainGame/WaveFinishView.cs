using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Text;

public class WaveFinishView : MonoBehaviour
{
    [SerializeField] private Image imgBackground;
    [SerializeField] private TextMeshProUGUI txtWaveFinish;
    [SerializeField] private BtnWaveFinishUI btnWaveFinish;

    private string text = "웨이브 완료!";


    private void Start()
    {
        imgBackground.DOFade(0.7f, 2f)
            .OnComplete(() => TypeTextEffect().Forget());
    }

    public void InitializeWaveFinishView()
    {
        imgBackground.DOFade(0.7f, 2f)
            .OnComplete(() => TypeTextEffect().Forget());
    }

    private async UniTask TypeTextEffect()
    {
        txtWaveFinish.gameObject.SetActive(true);
        StringBuilder sb = new StringBuilder();

        foreach (var ch in text)
        {
            sb.Append(ch);
            txtWaveFinish.text = sb.ToString();

            await UniTask.Delay(200);
        }

        await UniTask.Delay(500);

        btnWaveFinish.gameObject.SetActive(true);
    }
}

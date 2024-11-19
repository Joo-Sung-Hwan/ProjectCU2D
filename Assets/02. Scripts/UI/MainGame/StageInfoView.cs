using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInfoView : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI txtWaveCount;
    [SerializeField] private TextMeshProUGUI txtWaveTimer;


    public void InitializeStageInfoView(int waveCnt, int waveTimer)
    {
        txtWaveCount.text = $"웨이브 {waveCnt.ToString() + 1}";
        SetWaveTimer(waveTimer).Forget();
    }

    private async UniTask SetWaveTimer(int waveTimer)
    {
        while (waveTimer >= 0)
        {
            if (waveTimer == 5)
                txtWaveTimer.color = Settings.legend; // 빨강

            txtWaveTimer.text = waveTimer.ToString();

            await UniTask.Delay(1000);

            waveTimer--;
        }

        txtWaveTimer.color = Color.white; // 다시 원래 색으로
    }
}

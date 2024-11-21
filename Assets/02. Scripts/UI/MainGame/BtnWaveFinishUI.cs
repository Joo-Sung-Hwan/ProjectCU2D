using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnWaveFinishUI : MonoBehaviour
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private Button btnConfirm;

    public Button BtnConfirm => btnConfirm;


    public void InitializeBtnWaveFinish()
    {

    }
}

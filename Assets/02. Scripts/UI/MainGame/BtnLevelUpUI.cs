using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnLevelUpUI : MonoBehaviour
{
    [SerializeField] private Image imgSprite;
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private Button btnLevelUp;

    public Button BtnLevelUp => btnLevelUp;


    public void InitializeBtnLevelUp(Sprite sprite, string desc)
    {
        imgSprite.sprite = sprite;
        txtDescription.text = desc;
    }
}

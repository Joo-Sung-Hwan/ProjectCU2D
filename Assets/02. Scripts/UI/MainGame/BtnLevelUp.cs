using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnLevelUp : MonoBehaviour
{
    [SerializeField] private Image imgSprite;
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private Button btnLevelUp;

    public Button BtnLevelUpChoice => btnLevelUp;


    public void InitializeBtnLevelUp(Sprite sprite, string desc)
    {
        imgSprite.sprite = sprite;
        txtDescription.text = desc;
    }
}

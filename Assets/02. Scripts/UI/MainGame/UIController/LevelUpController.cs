using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    private List<BtnLevelUpUI> btnLevelUps;


    private void Awake()
    {
        btnLevelUps = GetComponentsInChildren<BtnLevelUpUI>().ToList();


        // 임시. 추후에 레벨업 버튼 클릭시 레벨업 적용 기능 구독해야함
        foreach (var btnLevelUp in btnLevelUps)
        {
            btnLevelUp.BtnLevelUp.onClick.AddListener(() => gameObject.SetActive(false));
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    public void InitializeLevelUpUI(PlayerLevelUpData data, Sprite sprite,int index)
    {
        btnLevelUps[index].InitializeBtnLevelUp(sprite, data.description);
    }
    public void InitializeLevelUpUI(WeaponLevelUpData data, Sprite sprite, int weaponIndex, int index)
    {
        btnLevelUps[index].InitializeBtnLevelUp(sprite, data.description);
    }
    public void InitializeLevelUpUI(WeaponDetailsSO weaponDetailsSO, Sprite sprite, int index)
    {
        btnLevelUps[index].InitializeBtnLevelUp(sprite, weaponDetailsSO.description);
    }
}

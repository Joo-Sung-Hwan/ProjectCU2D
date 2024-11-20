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
    }
    private void OnEnable()
    {
        Time.timeScale = 0.0f;

        // 레벨업 버튼 활성화되면서 기존의 선택지들 지워주기
        foreach (var btnLevelUp in btnLevelUps)
            btnLevelUp.BtnLevelUp.onClick.RemoveAllListeners();
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }


    // 플레이어 레벨업 선택지
    public void InitializeLevelUpUI(PlayerLevelUpData data, Sprite sprite, int btnIndex)
    {
        btnLevelUps[btnIndex].InitializeBtnLevelUp(sprite, data.description);

        btnLevelUps[btnIndex].BtnLevelUp.onClick.AddListener(() => {
            GameManager.Instance.Player.Stat.PlayerStatChanged(data);
            gameObject.SetActive(false);
        });
    }

    // 무기 레벨업 선택지 (무기의 레벨이 10이 아닐 경우)
    public void InitializeLevelUpUI(Weapon weapon, WeaponLevelUpData data, int btnIndex)
    {
        btnLevelUps[btnIndex].InitializeBtnLevelUp(weapon.WeaponSprite, data.description);

        btnLevelUps[btnIndex].BtnLevelUp.onClick.AddListener(() => {
            weapon.WeaponStatChanged(data);
            gameObject.SetActive(false);
        });
    }

    // 무기 업그레이드 선택지 (무기의 레벨이 10에 도달할 경우)
    public void InitializeLevelUpUI(Weapon weapon, int btnIndex)
    {
        btnLevelUps[btnIndex].InitializeBtnLevelUp(weapon.WeaponSprite, weapon.WeaponDetails.upgradeDesc);

        btnLevelUps[btnIndex].BtnLevelUp.onClick.AddListener(() => {
            weapon.UpgrageWeapon();
            gameObject.SetActive(false);
        });
    }

    // 새로운 무기 선택지 (플레이어의 무기 수가 부족할때)
    public void InitializeLevelUpUI(WeaponDetailsSO weaponDetailsSO, int btnIndex)
    {
        btnLevelUps[btnIndex].InitializeBtnLevelUp(weaponDetailsSO.weaponSprite, weaponDetailsSO.description);
    }
}

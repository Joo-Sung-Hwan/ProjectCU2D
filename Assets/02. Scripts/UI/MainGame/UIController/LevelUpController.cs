using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    private List<BtnLevelUp> btnLevelUps;


    private void Awake()
    {
        btnLevelUps = GetComponentsInChildren<BtnLevelUp>().ToList();

        foreach (var btnLevelUp in btnLevelUps)
        {
            btnLevelUp.BtnLevelUpChoice.onClick.AddListener(() => gameObject.SetActive(false));
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
}

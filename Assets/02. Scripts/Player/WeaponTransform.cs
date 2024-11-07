using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;


public class WeaponTransform : MonoBehaviour
{
    private Dictionary<Weapon, SpriteRenderer> weaponTransform = new Dictionary<Weapon, SpriteRenderer>();
    private List<SpriteRenderer> weaponSprites = new List<SpriteRenderer>();

    private int weaponCount = 0;


    private void Awake()
    {
        // WeaponTransform의 자식들(Weapon_1 ~ Weapon_6)의 SpriteRenderer 컴포넌트를 가져옵니다
        weaponSprites = transform.GetComponentsInChildren<SpriteRenderer>().ToList();

    }


    public void Add(Weapon weapon)
    {
        weaponTransform.Add(weapon, weaponSprites[weaponCount++]);
        weaponTransform[weapon].sprite = weapon.WeaponSprite;
    }

    public void RotateWeapon(Weapon weapon, float angle)
    {
        if (weaponTransform.TryGetValue(weapon, out SpriteRenderer sprite))
        {
            angle = (sprite.flipX) ? angle + 180f : angle;

            // 트윈쓸지, 그냥 바로 즉시회전할지 선택
            //sprite.transform.eulerAngles = new Vector3(0f, 0f, angle); 
            sprite.transform.DORotate(new Vector3(0f, 0f, angle), 0.1f);
        }
    }
}

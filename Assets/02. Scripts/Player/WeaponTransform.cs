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
        // WeaponTransform�� �ڽĵ�(Weapon_1 ~ Weapon_6)�� SpriteRenderer ������Ʈ�� �����ɴϴ�
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

            // Ʈ������, �׳� �ٷ� ���ȸ������ ����
            //sprite.transform.eulerAngles = new Vector3(0f, 0f, angle); 
            sprite.transform.DORotate(new Vector3(0f, 0f, angle), 0.1f);
        }
    }
}

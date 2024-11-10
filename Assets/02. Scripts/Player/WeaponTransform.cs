using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;


public class WeaponTransform : MonoBehaviour
{
    private Dictionary<Weapon, SpriteRenderer> weaponTransform = new Dictionary<Weapon, SpriteRenderer>();
    private Dictionary<Weapon, Vector3> weaponLocalOffsets = new Dictionary<Weapon, Vector3>();
    private List<SpriteRenderer> weaponSprites = new List<SpriteRenderer>();

    private int weaponCount = 0;


    private void Awake()
    {
        // WeaponTransform�� �ڽĵ�(Weapon_1 ~ Weapon_6)�� SpriteRenderer ������Ʈ�� ��������
        weaponSprites = transform.GetComponentsInChildren<SpriteRenderer>().ToList();
    }


    public void Add(Weapon weapon)
    {
        var sprite = weaponSprites[weaponCount++];
        weaponTransform.Add(weapon, sprite);
        weaponTransform[weapon].sprite = weapon.WeaponSprite;

        // ���Ⱑ �߰��� �� �ʱ� ���� ��ġ�� ����
        weaponLocalOffsets.Add(weapon, sprite.transform.localPosition);
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

    public void MoveWeapon(Weapon weapon, Vector3 direction)
    {
        if (weaponTransform.TryGetValue(weapon, out SpriteRenderer sprite))
        {
            // �ش� �������� ������ �Ÿ���ŭ �̵�
            sprite.transform.DOLocalMove(direction * weapon.WeaponRange, 0.15f)
                .OnComplete(() => {
                    // �÷��̾� ��ġ�� ����ֵ� ������� ������ǥ�� ������� ��ġ�� �̵�
                    sprite.transform.DOLocalMove(weaponLocalOffsets[weapon], 0.15f);
                });
        }
    }
}

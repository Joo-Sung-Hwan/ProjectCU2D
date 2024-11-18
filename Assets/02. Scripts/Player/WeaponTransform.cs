using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;


public class WeaponTransform : MonoBehaviour
{
    private Dictionary<Weapon, SpriteRenderer> weaponTransform = new Dictionary<Weapon, SpriteRenderer>();
    private Dictionary<Weapon, Vector3> weaponLocalOffsets = new Dictionary<Weapon, Vector3>();
    private Dictionary<Weapon, Transform> weaponLocalTransform = new Dictionary<Weapon, Transform>();
    private List<SpriteRenderer> weaponSprites = new List<SpriteRenderer>();

    private int weaponCount = 0;


    private void Awake()
    {
        // WeaponTransform의 자식들(Weapon_1 ~ Weapon_6)의 SpriteRenderer 컴포넌트를 가져오기
        weaponSprites = transform.GetComponentsInChildren<SpriteRenderer>().ToList();
    }


    public void Add(Weapon weapon)
    {
        var sprite = weaponSprites[weaponCount++];
        weaponTransform.Add(weapon, sprite);
        weaponTransform[weapon].sprite = weapon.WeaponSprite;
       
        // 무기가 추가될 때 초기 로컬 위치를 저장
        weaponLocalOffsets.Add(weapon, sprite.transform.localPosition);
        // 무기가 추가될 때 투사체의 발사위치 저장 (투사체 무기에만 적용되는 트랜스폼)
        weaponLocalTransform.Add(weapon, sprite.transform.GetChild(0));
    }

    public void RotateWeapon(Weapon weapon, float angle)
    {
        if (weaponTransform.TryGetValue(weapon, out SpriteRenderer sprite))
        {
            // 무기 좌/우 위치에 따라 회전하는 각도 다르게 적용
            angle = (sprite.flipX) ? angle + 180f : angle;

            // 트윈쓸지, 그냥 바로 즉시회전할지 선택
            //sprite.transform.eulerAngles = new Vector3(0f, 0f, angle); 
            sprite.transform.DORotate(new Vector3(0f, 0f, angle), 0.05f);
        }
    }

    public void MoveWeapon(Weapon weapon, Vector3 direction)
    {
        if (weaponTransform.TryGetValue(weapon, out SpriteRenderer sprite))
        {
            // 해당 방향으로 무기의 거리만큼 이동
            sprite.transform.DOLocalMove(direction * weapon.WeaponRange, 0.1f)
                .OnComplete(() => {
                    // 플레이어 위치가 어디있든 상관없이 로컬좌표로 상대적인 위치로 이동
                    sprite.transform.DOLocalMove(weaponLocalOffsets[weapon], 0.1f);
                });
        }
    }

    // 원거리 무기 발사시 투사체의 발사위치 (고정적으로 정해져 있음)
    public Transform GetWeaponTransform(Weapon weapon)
    {
        if (weaponTransform.TryGetValue(weapon, out SpriteRenderer sprite))
        {
            return weaponLocalTransform[weapon];
        }

        return null;
    }
}


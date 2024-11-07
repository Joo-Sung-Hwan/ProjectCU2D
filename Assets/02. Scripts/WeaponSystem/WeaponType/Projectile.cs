using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float speed;
    private Weapon weapon;
    private bool isPiercing;
    //private SoundEffectSO soundEffect;

    private float distance;
    private float currentDistance;
    private Vector2 direction;
    private Vector2 distanceVector;


    public void SetUp(float speed, bool isPiercing, Vector2 direction, Weapon weapon)
    {
        //soundEffect = weapon.weaponFiringSoundEffect;
        this.speed = speed;
        this.isPiercing = isPiercing;
        this.direction = direction; // ����ü�� ���� ����
        this.weapon = weapon;
        distance = weapon.WeaponRange;
        currentDistance = 0f;
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // FixedUpdate���� ������ �������� ������ ���� speed��ŭ �̵�
        distanceVector = direction * speed;
        currentDistance += distanceVector.magnitude * Time.fixedDeltaTime; // ���ư� �Ÿ������ ���� magnitude
        rigidBody.velocity = distanceVector;

        if (currentDistance > distance)
            ObjectPoolManager.Instance.Release(this.gameObject, "bullet");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Monster monster))
        {
            //SoundEffectManager.Instance.PlaySoundEffect(soundEffect);
            monster.TakeDamage(weapon);
            monster.Rigid.AddForce(direction * weapon.WeaponKnockback);
        }

        // ����Ǵ� ���� ���ϰ� ������ ����Ǵ� ���� ���� count�� ���� ��
        // ������ ����Ǵ��� ���ο� ���� ��� �����̰ų� �ȵǰų��� ����
        if (!isPiercing)
            ObjectPoolManager.Instance.Release(this.gameObject, "bullet");
    }
}

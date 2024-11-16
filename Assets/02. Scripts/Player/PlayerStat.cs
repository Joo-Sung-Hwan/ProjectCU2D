using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerStat
{
    private Player player;

    public float MaxHp { get; private set; }
    public float Hp { get; private set; }
    public float HpRegen { get; private set; }
    //public float HpSteal { get; private set; }
    public int BonusDamage { get; private set; }
    public int MeleeDamage { get; private set; }
    public int RangeDamage { get; private set; }
    public float Speed { get; private set; }
    public int Dodge { get; private set; }
    public float PickUpRange { get; private set; }
    // ... 그 외 기타 스탯들


    public void InitializePlayerStat(PlayerDetailsSO playerDetailsSO, Player player)
    {
        this.player = player;

        MaxHp = playerDetailsSO.Hp;
        Hp = MaxHp;
        HpRegen = playerDetailsSO.HpRegen;
        //HpSteal = playerDetailsSO.HpSteal;
        BonusDamage = playerDetailsSO.BonusDamage;
        MeleeDamage = playerDetailsSO.MeleeDamage;
        RangeDamage = playerDetailsSO.RangeDamage;
        Speed = playerDetailsSO.Speed;
        Dodge = playerDetailsSO.Dodge;
        PickUpRange = playerDetailsSO.PickUpRange;

        HPRegenRoutine().Forget();
    }

    
    public void TakeDamage(float damage)
    {
        // 회피 검사
        if (UtilitieHelper.isSuccess(Dodge))
        {
            HitText hitText = ObjectPoolManager.Instance.Get("HitText", new Vector2(player.transform.position.x, player.transform.position.y + 1f), Quaternion.identity).GetComponent<HitText>();
            hitText.InitializeHitText(0,false,true);

            return;
        }

        Hp -= damage;
        player.HealthBar.SetHealthBar(Hp / MaxHp);
    }

    public async UniTaskVoid HPRegenRoutine()
    {
        while (true)
        {
            Hp = Mathf.Clamp(Hp+HpRegen, 0, MaxHp);
            player.HealthBar.SetHealthBar(Hp / MaxHp);

            await UniTask.Delay(1000, cancellationToken: player.DisableCancellation.Token);
        }
    }
}

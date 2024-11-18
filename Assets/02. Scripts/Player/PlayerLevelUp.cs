using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerLevelUp : MonoBehaviour
{
    [SerializeField] private PlayerLevelUpDatabase playerDB;
    [SerializeField] private WeaponLevelUpDatabase weaponDB;

    private Player player;
    private PlayerStat playerStat;

    private Dictionary<int, List<int>> validChoice = new();


    private void Start()
    {
        player = GetComponent<Player>();
        playerStat = player.Stat;
        playerStat.OnLevelChanged += PlayerStat_OnLevelChanged;
    }


    private void PlayerStat_OnLevelChanged(PlayerStat stat, int level)
    {
        /// 1. 플레이어,무기 포함 어떤걸 레벨업할지 선택 (int 난수)
        /// 2. if문으로 검사 후 해당 타입에 맞는 랜덤so 가져오기
        /// 3. 딕셔너리에 1번에서 고른 난수와 해당 so의 type 검사하고 없으면 삽입
        /// 4. 모두 통과해서 결정된 so는 i와 함께 gm-uc-lc 통해 i번째 버튼 초기화
        /// 5. 버튼에 구독할 이벤트도 컨트롤러 클래스에서 구현
        ///

        int levelUpIndex = player.WeaponList.Count + 1;

        // 선택지 4개
        for (int i = 0; i < 4;)
        {
            // 플레이어,무기 중에서 어떤걸 업그레이드할지 랜덤선택
            int chose = Random.Range(0, levelUpIndex);

            if (chose == 0) // 플레이어 선택지
            {
                PlayerLevelUpData data = GetRandomData(playerDB.database);

                if (IsValidChoice(chose, data) == false)
                    continue;

                Debug.Log($"{chose}. {data.description} : +{data.value}");
                GameManager.Instance.UIController.LevelUpController.InitializeLevelUpUI(
                        data,
                        player.SpriteRenderer.sprite,
                        i // 선택지 위치
                    );
            }
            else // 무기 선택지
            {
                WeaponLevelUpData data = GetRandomData(weaponDB.database);

                if (IsValidChoice(chose, data) == false)
                    continue;

                Debug.Log($"{chose}. {data.description} : +{data.value}");
                GameManager.Instance.UIController.LevelUpController.InitializeLevelUpUI(
                        data,
                        player.WeaponList[chose-1].WeaponSprite,
                        chose, // 플레이어 무기 중 몇번째인지
                        i
                    );
            }

            ++i;
        }

        GameManager.Instance.UIController.LevelUpController.gameObject.SetActive( true);
        validChoice.Clear();
    }

    private bool IsValidChoice(int chose, ILevelUpData data)
    {
        // 이미 같은 스탯타입이 선택되었는지 검사
        // 동일한 스탯이 중복으로 뜨는 것을 방지

        if (validChoice.TryGetValue(chose, out var prevDatas))
        {
            if (prevDatas.Contains(data.GetStatType()))
                return false; 
        }
        else
            validChoice[chose] = new List<int>();

        validChoice[chose].Add(data.GetStatType());

        return true;
    }

    private T GetRandomData<T>(List<T> database) where T : ILevelUpData
    {
        // ILevelUpData를 상속받은 플레이어/무기 레벨업 데이터에서 랜덤으로 가져오기
        // 각 레벨업 데이터는 정해진 확률이 있고 확률대로 나옴

        int totalRatio = database.Sum(x => x.GetRatio());
        int randomNumber = Random.Range(0, totalRatio);
        int ratioSum = 0;

        foreach (var data in database)
        {
            ratioSum += data.GetRatio();
            if (randomNumber < ratioSum)
                return data;
        }

        return default;
    }
}

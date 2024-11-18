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
        /// 1. �÷��̾�,���� ���� ��� ���������� ���� (int ����)
        /// 2. if������ �˻� �� �ش� Ÿ�Կ� �´� ����so ��������
        /// 3. ��ųʸ��� 1������ �� ������ �ش� so�� type �˻��ϰ� ������ ����
        /// 4. ��� ����ؼ� ������ so�� i�� �Բ� gm-uc-lc ���� i��° ��ư �ʱ�ȭ
        /// 5. ��ư�� ������ �̺�Ʈ�� ��Ʈ�ѷ� Ŭ�������� ����
        ///

        int levelUpIndex = player.WeaponList.Count + 1;

        // ������ 4��
        for (int i = 0; i < 4;)
        {
            // �÷��̾�,���� �߿��� ��� ���׷��̵����� ��������
            int chose = Random.Range(0, levelUpIndex);

            if (chose == 0) // �÷��̾� ������
            {
                PlayerLevelUpData data = GetRandomData(playerDB.database);

                if (IsValidChoice(chose, data) == false)
                    continue;

                Debug.Log($"{chose}. {data.description} : +{data.value}");
                GameManager.Instance.UIController.LevelUpController.InitializeLevelUpUI(
                        data,
                        player.SpriteRenderer.sprite,
                        i // ������ ��ġ
                    );
            }
            else // ���� ������
            {
                WeaponLevelUpData data = GetRandomData(weaponDB.database);

                if (IsValidChoice(chose, data) == false)
                    continue;

                Debug.Log($"{chose}. {data.description} : +{data.value}");
                GameManager.Instance.UIController.LevelUpController.InitializeLevelUpUI(
                        data,
                        player.WeaponList[chose-1].WeaponSprite,
                        chose, // �÷��̾� ���� �� ���°����
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
        // �̹� ���� ����Ÿ���� ���õǾ����� �˻�
        // ������ ������ �ߺ����� �ߴ� ���� ����

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
        // ILevelUpData�� ��ӹ��� �÷��̾�/���� ������ �����Ϳ��� �������� ��������
        // �� ������ �����ʹ� ������ Ȯ���� �ְ� Ȯ����� ����

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

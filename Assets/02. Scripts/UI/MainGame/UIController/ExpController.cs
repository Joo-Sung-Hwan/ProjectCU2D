using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
    [SerializeField] ExpView expView;

    private PlayerStat playerStat;


    private void Start()
    {
        Debug.Log(GameManager.Instance.Player == null);
        //playerStat = GameManager.Instance.Player.Stat;
        StartCoroutine(test());

    }

    private IEnumerator test()
    {
        while (true)
        {
            if (GameManager.Instance.Player == null)
                yield return null;

            else
            {
                Debug.Log("Áö±Ý");
                playerStat = GameManager.Instance.Player.Stat;
                playerStat.OnExpChanged += PlayerStat_OnExpChanged;

                break;
            }
        }
    }

    private void PlayerStat_OnExpChanged(PlayerStat stat, float ratio)
    {
        expView.SetExpBar(ratio);
    }
}

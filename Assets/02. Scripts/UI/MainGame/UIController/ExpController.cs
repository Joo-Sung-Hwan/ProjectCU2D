using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
    [SerializeField] ExpView expView;

    private PlayerStat playerStat;


    public void InitializeExpController()
    {
        playerStat = GameManager.Instance.Player.Stat;
        playerStat.OnExpChanged += PlayerStat_OnExpChanged;
    }

    private void PlayerStat_OnExpChanged(PlayerStat stat, float ratio)  
        => expView.SetExpBar(ratio);
    
}

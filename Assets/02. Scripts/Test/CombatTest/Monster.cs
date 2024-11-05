using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private EnemyDetailsSO enemyDetails;
    private NavMeshAgent navMeshAgent;
    private MonsterStat stat;
    private Transform player;

    public EnemyDetailsSO EnemyDetails => enemyDetails;
    public NavMeshAgent NavMeshAgent => navMeshAgent;
    public MonsterStat Stat => stat;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = GameManager.Instance.Player.transform;
    }

    private void FixedUpdate()
    {
        // 플레이어 위치 꾸준히 따라가기
        navMeshAgent.destination = player.position; 
    }

    public void InitEnemy(EnemyDetailsSO enemyDetails)
    {
        stat = new MonsterStat(enemyDetails);

        player = GameManager.Instance.Player.transform;
        navMeshAgent.destination = player.position;
    }


}

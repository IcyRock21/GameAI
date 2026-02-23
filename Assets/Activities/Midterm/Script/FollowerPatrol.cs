using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class FollowerPatrol : MonoBehaviour
{
    FollowerPatrol followerPatrol;
    private NavMeshAgent m_Agent;
    [SerializeField] EnemyAI enemyAI;
    [SerializeField] Player player;
    [SerializeField]Transform centerPoint, playerPos, enemyPos;
    [SerializeField] float maxPatrolSize;
    [SerializeField] Material followingPlayerMat, followingEnemyMat;
    Vector3 toPatrolTo;
    float xPatrol, zPatrol;
    bool followPlayer, followEnemy;
    public bool hasLeader;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        hasLeader = false;
        GenerateNextPatrol();
        Debug.Log(toPatrolTo);
        m_Agent.SetDestination(toPatrolTo);

    }

    void Update()
    {
        if (!m_Agent.pathPending /*makes sure path is ready*/ && m_Agent.remainingDistance <= m_Agent.stoppingDistance && !followPlayer)
        {
            GenerateNextPatrol();
            m_Agent.SetDestination(toPatrolTo);
        }

        FollowLeader(followPlayer, playerPos, followingPlayerMat);
        FollowLeader(followEnemy, enemyPos, followingEnemyMat);

        Debug.Log(followPlayer);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(!hasLeader)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                followPlayer = true;
                player.playerFolCount++;
                hasLeader = true;
                RemoveFollowerPool();


            }

            else if (collision.gameObject.CompareTag("EnemyLeader"))
            {
                followEnemy = true;
                enemyAI = collision.gameObject.GetComponent<EnemyAI>();
                enemyAI.enemyFolCount++;
                hasLeader = true;
            }

        }

    }

    void RemoveFollowerPool()
    {
        var followersCopy = new List<GameObject>(enemyAI.followers);
        foreach (GameObject follower in followersCopy)
        {
            var followerPatrol = follower.GetComponent<FollowerPatrol>();
            if (followerPatrol.hasLeader)
            {
                enemyAI.followers.Remove(follower);
            }
        }
    }

    void FollowLeader(bool followLeader,Transform leaderPos, Material followingLeaderMat)
    {
        if (followLeader)
        {
            m_Agent.SetDestination(leaderPos.position);

            //Change Material
            if (this.GetComponent<Renderer>().material != followingLeaderMat)
            {
                this.GetComponent<Renderer>().material = followingLeaderMat;
            }
        }
    }

    void GenerateNextPatrol()
    {

        toPatrolTo = new Vector3 (xPostionPatrol(), 0f, yPostionPatrol());
    }


    float xPostionPatrol()
    { xPatrol = UnityEngine.Random.Range(centerPoint.position.x - maxPatrolSize, centerPoint.position.x + maxPatrolSize); return xPatrol; }
    float yPostionPatrol()
    { zPatrol = UnityEngine.Random.Range(centerPoint.position.z - maxPatrolSize, centerPoint.position.z + maxPatrolSize); return zPatrol; }

}

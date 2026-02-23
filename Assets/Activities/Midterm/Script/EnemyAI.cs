using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int enemyFolCount;
    [SerializeField] Transform playerPos;
    [SerializeField] Player player; 
    [SerializeField] GameObject bestTarget;
    [SerializeField] bool enemyNearPlayer;
    [SerializeField] float maxDistToPlayer;
    FollowerPatrol followerPatrol;
    public List<GameObject> followers;
    private NavMeshAgent m_Agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("NoLeader");
        followers.AddRange(objectsWithTag);

        m_Agent = GetComponent<NavMeshAgent>();
        GetClosestFollower(transform.position);
        followerPatrol = bestTarget.GetComponent<FollowerPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        //problem if there are no more followers, null exception
        /*        if (player.playerFolCount <= enemyFolCount)
                {
                    m_Agent.SetDestination(bestTarget.transform.position);
                    if (followerPatrol.hasLeader)
                    {
                        Debug.Log("Follweor has leaeder: " + followerPatrol.hasLeader);
                        followers.Remove(bestTarget);
                        GetClosestFollower(transform.position);
                        followerPatrol = bestTarget.GetComponent<FollowerPatrol>();
                    }
                }*/
        Debug.Log("FollowerCount: " + followers.Count);
        if (enemyNearPlayer && !(followers.Count <= 0)) 
        { 
            if(player.playerFolCount > enemyFolCount)
            {
                Debug.Log("morePlayerFollower");

                Vector3 dir = (transform.position - player.transform.position).normalized;
                Vector3 targetPos = transform.position + dir * 2f;

                m_Agent.SetDestination(targetPos);
            }
            else if (player.playerFolCount < enemyFolCount)
            {
                Debug.Log("moreEnemyFollower");
                m_Agent.SetDestination(player.transform.position);
            }

            else if (player.playerFolCount == enemyFolCount)
            {
                Debug.Log("Equal Follower");
                if (!(followers.Count <= 0))
                {
                    GetFollowers();
                }
            }
        }
        else
        {
            if(!(followers.Count <= 0))
            {
                GetFollowers();
            }
        }

        if (Vector3.Distance(this.transform.position, playerPos.transform.position) <= maxDistToPlayer)
        {
            enemyNearPlayer = true;
        }
        else
        {
            enemyNearPlayer = false;
        }
        Debug.Log("enemyNearPLayer: " + enemyNearPlayer);
    }
    public void GetFollowers()
    {
        m_Agent.SetDestination(bestTarget.transform.position);
        if (followerPatrol.hasLeader)
        {
            Debug.Log("Follweor has leaeder: " + followerPatrol.hasLeader);
            followers.Remove(bestTarget);
            GetClosestFollower(transform.position);
            followerPatrol = bestTarget.GetComponent<FollowerPatrol>();
        }
    }
    public void GetClosestFollower(Vector3 startPosition)
    {
        bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject potentialTarget in followers)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - startPosition;

            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
    }
}

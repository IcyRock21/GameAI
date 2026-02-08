using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AlienPatrolFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    FieldOfView fieldOfView;
    public Transform ellenPos;
    [SerializeField] Transform[] patrolPos;
    [SerializeField] int toPatrolTo;
    [SerializeField] float susTimer;
    [SerializeField] bool chasingPlayer;
    [SerializeField] bool playerWasSeen;
    [SerializeField] bool isSuspicious;
    // Start is called one before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        GenerateNextPatrol();

        agent.SetDestination(patrolPos[toPatrolTo].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(agent.remainingDistance);
        Debug.Log(toPatrolTo);


        if (!agent.pathPending /*makes sure path is ready*/ && agent.remainingDistance <= agent.stoppingDistance && !chasingPlayer)
        {
            GenerateNextPatrol();
            agent.SetDestination(patrolPos[toPatrolTo].transform.position);
        }

        if (fieldOfView.canSeePlayer && !chasingPlayer && !isSuspicious) //suspision starts when the alien sees ellen
        {
            StartCoroutine(Suspicious());
        }

        if (chasingPlayer)
        {
            agent.SetDestination(ellenPos.position);
        }

        if (!fieldOfView.canSeePlayer && chasingPlayer && !isSuspicious) //Suspision starts again when ellen is outside of FOV
        {
            StartCoroutine(Suspicious());
        }
        /*
                if (chasingPlayer && fieldOfView.canSeePlayer)
                {
                    agent.SetDestination(ellenPos.position);
                }
                else
                {
                    StartCoroutine (Suspicious());
                }*/
    }

    void GenerateNextPatrol()
    {
        toPatrolTo = Random.Range(0, patrolPos.Length);
    }

    IEnumerator Suspicious()
    {
        isSuspicious = true; //calls courutine once
        agent.isStopped = true; //stop the agent to look

        yield return new WaitForSeconds(susTimer); //wait for the suspision timer

        agent.isStopped = false;

        if (fieldOfView.canSeePlayer) //if the alien can still see ellen during the suspision, start chasing ellen
        {
            chasingPlayer = true;
        }
        else
        {
            chasingPlayer = false; 
            
            //if ellen is not seen, Patrol on another random point again
            GenerateNextPatrol();
            agent.SetDestination(patrolPos[toPatrolTo].position);
        }

        isSuspicious = false;
    }
}

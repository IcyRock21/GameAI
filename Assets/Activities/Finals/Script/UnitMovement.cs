using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] bool isRedTeam;
    [SerializeField] GameObject bestTarget;
    [SerializeField] UnitAttack unitAttack;
    public List<GameObject> enemyUnits;
    public UnitType unitType;
    public UnitState unitState;
    public Team team; //for flexibility and more team counts

    public NavMeshAgent agent;



    private void Start()
    {
        unitAttack = GetComponent<UnitAttack>();
        agent = GetComponent<NavMeshAgent>();
        CheckEnemyCount();
        GetClosestTarget(transform.position);

    }

    private void Update()
    {
        CheckEnemyCount();
        if (unitState == UnitState.Walk)
        {
            GetClosestTarget(transform.position);
            if(bestTarget != null && enemyUnits.Count > 0)
            {
                agent.SetDestination(bestTarget.transform.position);
                unitAttack.Attack();
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
    }


    public void GetClosestTarget(Vector3 startPosition) //same script as 
    {
        bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject potentialTarget in enemyUnits)
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



    public void CheckEnemyCount()
    {
        enemyUnits.Clear();

        List<GameObject> foundObjects = new List<GameObject>();

        if (team == Team.Red)
        {
            foundObjects.AddRange(GameObject.FindGameObjectsWithTag("BlueTeam"));
        }
        else
        {
            foundObjects.AddRange(GameObject.FindGameObjectsWithTag("RedTeam"));
        }

        foreach (var obj in foundObjects)
        {
            if (obj != null)
            {
                enemyUnits.Add(obj);
            }
        }


        Debug.Log("Found " + foundObjects.Count + " enemies");
    }
}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] bool isRedTeam;
    [SerializeField] int moveSpeed;
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
        if (unitState == UnitState.Walk)
        {
            GetClosestTarget(this.transform.position);
            
            agent.SetDestination(bestTarget.transform.position);
            unitAttack.Attack();
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
        if (team == Team.Red)
        {
            List<GameObject> foundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlueTeam"));
            enemyUnits.AddRange(foundObjects);
            Debug.Log(foundObjects.Count);
        }

        else
        {
            List<GameObject> foundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("RedTeam"));
            enemyUnits.AddRange(foundObjects);
        }

    }
}

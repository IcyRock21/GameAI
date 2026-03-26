using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] bool isRedTeam;
    [SerializeField] int moveSpeed;
    [SerializeField] GameObject bestTarget;
    public List<GameObject> enemyUnits;
    [SerializeField] UnitType unitType;
    [SerializeField] UnitState unitState;
    [SerializeField] Team team; //for flexibility and more team counts

    [SerializeField] int unitHealth;


    private void Start()
    {
       if (team == Team.Red)
        {
            GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("BlueTeam");
        }

        else
        {
            GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("RedTeam");
        }
    }

    private void Update()
    {
        if (unitState == UnitState.Walk)
        {
            //Unit move towards closest enemy
            //if Unit is inside
        }
    }

    public void BerzerkerAttack()
    {

    }

    public void TankAttack()
    {

    }

    public void BowAttack()
    {

    }

    public void MageAttack()
    {

    }


    public void GetClosestFollower(Vector3 startPosition) //same script as 
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
}

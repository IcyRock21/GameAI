// MoveTo.cs
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{

    public Transform goal;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(goal.position);
/*            agent.destination = goal.position;*/
        }
    }
}

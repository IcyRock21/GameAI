using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation.Samples
{
    /// <summary>
    /// Use physics raycast hit from mouse click to set agent destination
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToMove : MonoBehaviour
    {
        NavMeshAgent m_Agent;
        RaycastHit m_HitInfo = new RaycastHit();
        Animator m_animation;

        void Start()
        {
            m_animation = GetComponent<Animator>();
            m_Agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                    m_Agent.destination = m_HitInfo.point;
            }

            if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
            {
                m_animation.SetBool("IsWalking", false);
            }
            else
            {
                m_animation.SetBool("IsWalking", true);
            }

        }
    }
}
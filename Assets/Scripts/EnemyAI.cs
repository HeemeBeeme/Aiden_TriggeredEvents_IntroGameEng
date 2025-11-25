using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform Target;

    public NavMeshAgent agent;
    void Update()
    {
        agent.SetDestination(Target.position);
    }
}

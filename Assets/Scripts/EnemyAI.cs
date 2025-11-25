using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public Transform Target;
    public NavMeshAgent agent;

    public float rotateSpeed = 10;
    void Update()
    {
        agent.SetDestination(Target.position);
    }
}

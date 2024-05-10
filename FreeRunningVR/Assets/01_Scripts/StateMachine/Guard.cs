using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public float moveSpeed = 3f;
    private NavMeshAgent agent;

    private StateMachine stateMachine;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        stateMachine = new StateMachine(this, GetComponents<State>());
        stateMachine.SwitchState(typeof(PatrolState));
    }

    public void Update()
    {
        stateMachine?.OnUpdate();
    }

    public void OnTakeDamage(int damage)
    {

    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }
}

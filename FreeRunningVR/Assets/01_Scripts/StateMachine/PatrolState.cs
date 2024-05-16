using UnityEngine;

public class PatrolState : State
{
    public float stoppingDistance = 1f;
    public Transform[] patrolPoints;
    private int index = -1;
    
    public override void OnEnter()
    {
        index++;
        if(index >= patrolPoints.Length)
        {
            index = 0;
        }
    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        //Transition
        if(Vector3.Distance(transform.position, patrolPoints[index].position) < stoppingDistance)
        {
            //Controller.SwitchState(typeof(IdleState));
        }

        //Move
        Controller.Controller.MoveToPosition(patrolPoints[index].position);
    }
}

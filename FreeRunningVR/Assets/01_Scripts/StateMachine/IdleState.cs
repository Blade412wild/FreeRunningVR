using UnityEngine;

public class IdleState : State
{
    [SerializeField] private float maxTime = 3f;
    [SerializeField] private float minTime = 3f;
    private float waitTime = 0;
    public override void OnEnter()
    {
        waitTime = Random.Range(minTime, maxTime);
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
        waitTime -= Time.deltaTime;
        if(waitTime <= 0)
        {
            Debug.Log(typeof(PatrolState).Name);
            Controller.SwitchState(typeof(PatrolState));  
        }
    }
}
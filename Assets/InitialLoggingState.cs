using UnityEngine;

public class InitialLoggingState : State
{
    public override void EnterState(StateManager context)
    {
        Debug.Log("oieee");
    }

    public override void ExitState(StateManager context)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(StateManager context)
    {
        throw new System.NotImplementedException();
    }
}

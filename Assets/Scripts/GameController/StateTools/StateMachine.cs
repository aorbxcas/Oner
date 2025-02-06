using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState;
    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            //if (currentState.GetType() == newState.GetType()) return;
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }
    public void Update()
    {
        if(currentState != null)
        {
            currentState.Execute();
        }
    }

}

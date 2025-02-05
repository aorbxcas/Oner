using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(CharaController characterController) : base(characterController)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void Execute()
    {
        Debug.Log("Idle State Executing");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}

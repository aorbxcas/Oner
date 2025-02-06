using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class IdleState : State
{
    public IdleState(CharaController characterController) : base(characterController)
    {
    }

    public override void Enter()
    {
        //this.SendCommand(new CharacterActionCommand(characterController, new ChacterActionParams { ActionType = CharacterActionType.Idle }));

    }

    public override void Execute()
    {
        this.SendCommand(new CharacterActionCommand(characterController, new ChacterActionParams { ActionType = CharacterActionType.Idle }));
    }

    public override void Exit()
    {
    }
}
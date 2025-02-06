using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class AttackState : State
{
    public AttackState(CharaController characterController) : base(characterController)
    {
    }

    public override void Enter()
    {
        this.SendCommand(new CharacterActionCommand(characterController, new ChacterActionParams { ActionType = CharacterActionType.Attack }));
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
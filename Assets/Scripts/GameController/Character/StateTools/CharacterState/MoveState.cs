using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class MoveState : State
{
    private Vector2 direction;
    public MoveState(CharaController characterController, Vector2 direction) : base(characterController)
    {
        this.direction = direction;
    }
    public override void Enter()
    {

    }

    public override void Execute()
    {
        this.SendCommand(new CharacterActionCommand(characterController, new ChacterActionParams { ActionType = CharacterActionType.Move, direction = direction } ));
    }

    public override void Exit()
    {
    }
}

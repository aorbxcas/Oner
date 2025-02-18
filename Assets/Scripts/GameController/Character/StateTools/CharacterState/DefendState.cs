using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class DefendState : State
{
    public DefendState(CharaController characterController) : base(characterController)
    {
    }

    public override void Enter()
    {
        characterController.isActionPlaying = true;
        this.SendCommand(new CharacterActionCommand(characterController, new ChacterActionParams { ActionType = CharacterActionType.Defend }));
    }

    public override void Execute()
    {
        characterController.isActionPlaying = true;
    }

    public override void Exit()
    {
        characterController.isActionPlaying = false;
        characterController.SetDefendBool(false);
    }
}

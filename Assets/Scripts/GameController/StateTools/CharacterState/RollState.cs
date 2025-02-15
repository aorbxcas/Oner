using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using System;

public class RollState : State
{
    private Vector2 direction;
    Vector3 moveDirection;
    public RollState(CharaController characterController, Vector2 direction) : base(characterController)
    {
        this.direction = direction;
        if (Math.Abs(direction.x) <= 0.01 && Math.Abs(direction.y) <= 0.01)
        {
            direction = new Vector2(characterController.transform.forward.x, characterController.transform.forward.z).normalized;
        }
        moveDirection = new Vector3(direction.x, 0, direction.y);
    }

    public override void Enter()
    {
        characterController.isActionPlaying = true;
        this.SendCommand(new CharacterActionCommand(characterController, new ChacterActionParams { ActionType = CharacterActionType.Roll }));
        
    }
    public override void Execute()
    {
        characterController.GetComponent<CharacterController>().Move(moveDirection * characterController.mCharacterInfo.moveSpeed * Time.deltaTime);
    }
    public override void Exit()
    {
        characterController.SetRollBool(false);
    }
}

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
        characterController.isActionPlaying = true;
        this.SendCommand(new CharacterActionCommand(characterController, new ChacterActionParams { ActionType = CharacterActionType.Attack }));
    }

    public override void Execute()
    {
        // 攻击状态执行中，等待动画完成
        // isActionPlaying会在动画事件中自动重置
    }

    public override void Exit()
    {
        // 攻击完成，重置标志（如果动画事件没有重置）
        characterController.isActionPlaying = false;
    }
}
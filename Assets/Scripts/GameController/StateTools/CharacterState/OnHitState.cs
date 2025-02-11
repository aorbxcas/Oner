using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class OnHitState : State
{
    float damage;
    public OnHitState(CharaController characterController, int damage) : base(characterController)
    {
        this.damage = damage;
    }

    public override void Enter()
    {
        characterController.isActionPlaying = true;
        this.SendCommand(new CharacterAnimationCommand(characterController, CharacterAnimationType.OnDamage));
        if (characterController.mCharacterInfo.HP > damage)
        {
            characterController.mCharacterInfo.HP -= (int)damage;
        }
        else
        {
            characterController.mCharacterInfo.HP = 0;
        }

    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }
}

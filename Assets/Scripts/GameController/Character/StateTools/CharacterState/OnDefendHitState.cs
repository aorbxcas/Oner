using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class OnDefendHitState : State
{
    float damage;
    public OnDefendHitState(CharaController characterController, int damage) : base(characterController)
    {
        this.damage = damage;
    }
    public override void Enter()
    {
        characterController.isActionPlaying = true;
        damage *= (1 - characterController.mCharacterInfo.defendRatio);
        this.SendCommand(new CharacterAnimationCommand(characterController, CharacterAnimationType.OnDefendDamage));
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

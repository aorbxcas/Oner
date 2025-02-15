using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AnimationTimes
{
    During,
    Once
}
public class CharacterAnimationCommand : AbstractCommand
{
    public CharaController character;
    public CharacterAnimationType animationType;
    public CharacterAnimationCommand(CharaController character, CharacterAnimationType animationType)
    {
        this.character = character;
        this.animationType = animationType;
    }
    protected override void OnExecute()
    {
        switch (animationType)
        {
            case CharacterAnimationType.Run:
                character.PlayAnimation("Run",AnimationTimes.During);
                break;
            case CharacterAnimationType.Idle:
                character.PlayAnimation("Idle", AnimationTimes.During);
                break;
            case CharacterAnimationType.Attack:
                character.PlayAnimation("Attack");
                break;
            case CharacterAnimationType.OnDamage:
                character.PlayAnimation("OnDamage");
                break;
            case CharacterAnimationType.Defend:
                character.PlayAnimation("Defend", AnimationTimes.During);
                break;
            case CharacterAnimationType.OnDefendDamage:
                character.PlayAnimation("OnDefendDamage");
                break;
            case CharacterAnimationType.Roll:
                character.PlayAnimation("Roll");
                break;
            default:
                break;
        }
    }
}

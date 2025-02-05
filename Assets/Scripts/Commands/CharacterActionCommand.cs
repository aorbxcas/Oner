using QFramework;
using UnityEngine;
public struct ChacterActionParams
{
    public CharacterActionType ActionType;
    public Vector3 direction;
    public int damage;
}

public class CharacterActionCommand : AbstractCommand
{
    private CharaController character;
    private ChacterActionParams param;
    public CharacterActionCommand(CharaController character, ChacterActionParams param)
    {
        this.character = character;
        this.param = param;
    }
    protected override void OnExecute()
    {
        switch (param.ActionType)
        {
            case CharacterActionType.Idle:
                this.SendCommand(new CharacterAnimationCommand(character, CharacterAnimationType.Idle));
                break;
            case CharacterActionType.Move:
                Vector3 moveDirection = new Vector3(param.direction.x, 0, param.direction.y);
                this.SendCommand(new CharacterAnimationCommand(character, CharacterAnimationType.Run));
                character.GetComponent<CharacterController>().Move(moveDirection * character.mCharacterInfo.moveSpeed * Time.deltaTime);
                break;
            case CharacterActionType.Attack:
                character.mWeapon.GetComponent<WeaponController>().Attack();
                this.SendCommand(new CharacterAnimationCommand(character, CharacterAnimationType.Attack));
                break;
            case CharacterActionType.OnDamage:
                character.GetComponent<CharaController>().OnCharacterDamage(param.damage);
                this.SendCommand(new CharacterAnimationCommand(character, CharacterAnimationType.OnDamage));
                break;
            default:
                break;
        }
    }


}

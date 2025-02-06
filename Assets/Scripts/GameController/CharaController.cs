using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.InputSystem;

public enum CharacterActionType 
{
    Idle,
    Move,
    Attack,
    OnDamage,
    Test
}
public enum CharacterAnimationType
{
    Idle,
    Run,
    Attack,
    OnDamage,
}

public abstract class CharaController : MonoBehaviour,IController
{
    public CharacterInfo mCharacterInfo;
    public WeaponController mWeapon;
    public Animator mAnimator;
    public StateMachine mStateMachine;
    protected Vector2 moveValue;
    public bool isActionPlaying = false;


    public IArchitecture GetArchitecture()
    {
        return Oner.Interface;
    }
    void Awake()
    {

    }
    protected virtual void Start()
    {
        mCharacterInfo = GetComponent<CharacterInfo>();
        if (mCharacterInfo == null)
        {
            gameObject.AddComponent<CharacterInfo>();
            mCharacterInfo = GetComponent<CharacterInfo>();
        }

        mStateMachine = new StateMachine();

        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected abstract void MoveInput(Vector2 direction);
    protected abstract void AttackInput();

    public void OnCharacterDamage(int damage)
    {
        Debug.Log("hurt");
        if (mCharacterInfo.HP > damage)
        {
            mCharacterInfo.HP -= damage;
        }
        else
        {
            mCharacterInfo.HP = 0;
        }
    }
    protected void Die()
    {
        Debug.Log(this.gameObject.name+"has Died.");
    }

    public void PlayAnimation(string AnimName, AnimationTimes animationTimes = AnimationTimes.Once)
    {
        if (mAnimator == null) return;
        if(animationTimes == AnimationTimes.Once)
        {
            mAnimator.SetTrigger(AnimName);
        }else if(animationTimes == AnimationTimes.During)
        {
            if (mCharacterInfo.currentAnimation == AnimName) return;
            if (mCharacterInfo.currentAnimation != null)
            {
                mAnimator.SetBool(mCharacterInfo.currentAnimation, false);
            }
            mAnimator.SetBool(AnimName, true);
            mCharacterInfo.currentAnimation = AnimName;
        }
    }

}

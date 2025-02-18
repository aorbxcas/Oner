using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.InputSystem;
using System;

public enum CharacterActionType 
{
    Idle,
    Move,
    Attack,
    OnDamage,
    Test,
    Defend,
    Roll
}
public enum CharacterAnimationType
{
    Idle,
    Run,
    Attack,
    OnDamage,
    Defend,
    OnDefendDamage,
    Roll
}

public abstract class CharaController : MonoBehaviour, IController
{
    public CharacterInfo mCharacterInfo;
    public WeaponController mWeapon;
    public Animator mAnimator;
    public StateMachine mStateMachine;
    protected Vector2 moveValue;
    public bool isActionPlaying = false;
    public bool isDefending = false;
    public bool isRolling = false;


    public IArchitecture GetArchitecture()
    {
        return Oner.Interface;
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
    public void IdleInput()
    {
        mStateMachine.ChangeState(new IdleState(this));
    }
    public void MoveInput(Vector2 direction)
    {
        if (isActionPlaying == true) return;
        if (Math.Abs(direction.x) < 0.01 && Math.Abs(direction.y) < 0.01)
        {
            IdleInput();
        }
        else
        {
            mStateMachine.ChangeState(new MoveState(this, direction));
        }
    }

    public void AttackInput()
    {
        if (isActionPlaying == true) return;
        if (mWeapon != null)
        {
            mStateMachine.ChangeState(new AttackState(this));
        }
    }

    public void DefendInput()
    {
        if (isActionPlaying == true) return;
        mStateMachine.ChangeState(new DefendState(this));
    }

    public void RollInput()
    {
        if (isActionPlaying == true) return;
        mStateMachine.ChangeState(new RollState(this, moveValue));
    }

    public void OnCharacterDamage(int damage)
    {
        if (isRolling) {
            Debug.Log("ÉÁ±Ü³É¹¦");
            return;
        }
        if(isDefending) mStateMachine.ChangeState(new OnDefendHitState(this,damage));
        else mStateMachine.ChangeState(new OnHitState(this,damage));
    }
    public void Die()
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
    public void SetDefendBool(bool value)
    {
        isDefending = value;
    }
    public bool GetDefendBool()
    {
        return isDefending; 
    }
    public void SetRollBool(bool value)
    {
        isRolling = value;
    }
    public bool GetRollBool()
    {
        return isRolling;
    }
}

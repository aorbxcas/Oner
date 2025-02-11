using UnityEngine;
using QFramework;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerController : CharaController
{

    public InputAction moveAction;
    public InputAction attackAction;
    public InputAction TestAction;
    public InputAction DefendAction;


    public TextMeshProUGUI debugText; // 调试用Text，显示状态机信息
    public GameObject testWeapon;


    private void OnEnable()
    {
        moveAction.Enable();
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        attackAction.Enable();
        attackAction.performed += OnAttackPerformed;
        TestAction.Enable();
        TestAction.performed += OnTestPerformed;
        DefendAction.Enable();
        DefendAction.started += OnDefendStarted;
        DefendAction.canceled += OnDefendCanceled;

    }
    private void OnDisable()
    {
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;
        moveAction.Disable();
        attackAction.performed -= OnAttackPerformed;
        attackAction.Disable();
        TestAction.performed -= OnTestPerformed;
        TestAction.Disable();
        DefendAction.started -= OnDefendStarted;
        DefendAction.canceled -= OnDefendCanceled;
        DefendAction.Disable();

    }

    protected override void Start()
    {
        base.Start();
    }
    void Update()
    {
        if (debugText != null&&mStateMachine.currentState != null)//  显示状态机信息
        {
            debugText.text = mStateMachine.currentState.GetType().ToString();
        }
        mStateMachine.Update();
        MoveInput(moveValue);
        LookAtMouse();
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveValue = new Vector2(0,0);
    }
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        AttackInput();
    }
    private void OnTestPerformed(InputAction.CallbackContext context)
    {
        //  测试用
        //this.SendCommand(new CharacterActionCommand(this, new ChacterActionParams { ActionType = CharacterActionType.OnDamage, damage = 10 }));
        testWeapon.GetComponent<WeaponController>().Attack();
    }
    private void OnDefendStarted(InputAction.CallbackContext context)
    {
        DefendInput();
    }
    private void OnDefendCanceled(InputAction.CallbackContext context)
    {
        IdleInput();
    }

    protected override void IdleInput()
    {
        mStateMachine.ChangeState(new IdleState(this));
    }
    protected override void MoveInput(Vector2 direction)
    {
        if (isActionPlaying == true) return;
        if (Math.Abs(direction.x) < 0.01 && Math.Abs(direction.y) < 0.01)
        {
            IdleInput();
        }
        else
        {
            mStateMachine.ChangeState(new MoveState(this,direction));
        }
    }
    
    protected override void AttackInput()
    {
        if (isActionPlaying == true) return;
        if (mWeapon != null)
        {
            mStateMachine.ChangeState(new AttackState(this));
        }
    }
    
    protected override void DefendInput()
    {
        if (isActionPlaying == true) return;
        mStateMachine.ChangeState(new DefendState(this));
    }

    
    private void LookAtMouse()
    {
        // 用屏幕坐标系计算角色面向
        Vector3 direction3D = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        Vector2 direction2D = new Vector2(direction3D.x, direction3D.y);
        float angle = -Vector2.SignedAngle(Vector2.up, direction2D);
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.x,angle, transform.rotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, mCharacterInfo.rotationSpeed * Time.deltaTime);

    }
}

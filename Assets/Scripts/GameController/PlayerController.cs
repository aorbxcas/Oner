using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.TextCore.Text;
using System.Linq.Expressions;
using UnityEngine.InputSystem;
using System;

public class PlayerController : CharaController
{

    public InputAction moveAction;
    public InputAction attackAction;
    public InputAction TestAction;
    private Vector2 moveValue;

    private void OnEnable()
    {
        moveAction.Enable();
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        attackAction.Enable();
        attackAction.performed += OnAttackPerformed;
        TestAction.Enable();
        TestAction.performed += OnTestPerformed;
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

    }

    protected override void Start()
    {
        base.Start();
    }
    void Update()
    {
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
        this.SendCommand(new CharacterAnimationCommand(this, CharacterAnimationType.Idle));
    }
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        AttackInput();
    }
    private void OnTestPerformed(InputAction.CallbackContext context)
    {
        //  测试用
        this.SendCommand(new CharacterActionCommand(this, new ChacterActionParams { ActionType = CharacterActionType.OnDamage, damage = 10 }));
    }

    protected override void MoveInput(Vector2 direction)
    {
        this.SendCommand(new CharacterActionCommand(this, new ChacterActionParams { ActionType = CharacterActionType.Move, direction = direction }));
    }

    protected override void AttackInput()
    {

        this.SendCommand(new CharacterActionCommand(this, new ChacterActionParams { ActionType = CharacterActionType.Attack }));
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

using MoonSharp.VsCodeDebugger.SDK;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : CharaController
{
    public GameObject target;
    public AIBehaviourMachine behaviourMachine;

    public TextMeshProUGUI debugText;

    [HideInInspector]
    /// <summary>导航代理</summary>
    private NavMeshAgent navAgent;
    
    /// <summary>寻路路径点</summary>
    private Vector3[] pathCorners = { };
    
    /// <summary>当前路径点索引</summary>
    private int currentCornerIndex = 0;
    
    /// <summary>路径更新计时</summary>
    private float lastUpdateTime;
    
    /// <summary>敌人信息</summary>
    private EnemyInfo enemyInfo;

    protected override void Start()
    {
        base.Start();

        enemyInfo = mCharacterInfo as EnemyInfo;
        
        // 初始化行为机
        behaviourMachine = new AIBehaviourMachine();
        
        // 根据配置选择初始行为
        if (enemyInfo != null && enemyInfo.enablePatrol)
        {
            behaviourMachine.ChangeBehaviour(new PatrolBehaviour(this));
        }
        else
        {
            behaviourMachine.ChangeBehaviour(new AlertBehaviour(this));
        }

        // 初始化导航代理
        navAgent = GetComponent<NavMeshAgent>();
        if (navAgent != null)
        {
            navAgent.enabled = true;
        }
        
        lastUpdateTime = Time.time;
        
        // 初始化状态机转换条件
        InitializeStateTransitions();
    }

    void Update()
    {
        // 检查死亡状态
        CheckDeath();
        
        // 如果已死亡，不执行其他逻辑
        if (mStateMachine.GetCurrentStateType() == typeof(DeathState))
        {
            return;
        }
        
        // 更新行为机
        behaviourMachine.Update();
        
        // 更新状态机
        mStateMachine.Update();
        
        // 检查状态转换
        mStateMachine.CheckTransitions(this);
        
        // 处理移动输入
        MoveInput(moveValue);
        
        // 更新调试信息
        if (debugText != null && behaviourMachine.currentBehaviour != null)
        {
            string stateName = mStateMachine.GetCurrentStateType()?.Name ?? "None";
            string behaviourName = behaviourMachine.currentBehaviour.GetType().Name;
            debugText.text = $"State: {stateName}\nBehaviour: {behaviourName}";
        }
    }

    /// <summary>
    /// 导航移动到目标对象
    /// </summary>
    public void NavMove(GameObject targetObj)
    {
        if (targetObj == null) return;
        NavMove(targetObj.transform.position);
    }
    
    /// <summary>
    /// 导航移动到目标位置
    /// </summary>
    public void NavMove(Vector3 targetPosition)
    {
        if (enemyInfo == null) return;
        
        if (Time.time - lastUpdateTime > enemyInfo.updateInterval)
        {
            UpdatePath(targetPosition);
            lastUpdateTime = Time.time;
        }
        
        if (pathCorners.Length > 0)
        {
            if (currentCornerIndex > pathCorners.Length - 1)
            {
                UpdatePath(targetPosition);
                return;
            }

            Vector3 nextCorner = pathCorners[currentCornerIndex];
            MoveToPos(nextCorner);
           
            if (Vector3.Distance(transform.position, nextCorner) < 0.1f)
            {
                currentCornerIndex++;
            }
        }
    }
    
    /// <summary>
    /// 更新路径
    /// </summary>
    private void UpdatePath(Vector3 targetPosition)
    {
        if (navAgent == null) return;
        
        NavMeshPath path = new NavMeshPath();
        navAgent.CalculatePath(targetPosition, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            pathCorners = path.corners;
            currentCornerIndex = 0;
        }
    }
    
    /// <summary>
    /// 移动到指定位置
    /// </summary>
    private void MoveToPos(Vector3 targetPos)
    {
        LookAtPos(targetPos);
        Vector3 targetDirection3 = targetPos - transform.position;
        Vector2 targetDirection2 = new Vector2(targetDirection3.x, targetDirection3.z);
        moveValue = targetDirection2.normalized;
    }

    /// <summary>
    /// 面向指定位置
    /// </summary>
    public void LookAtPos(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        if (direction.magnitude > 0.01f)
        {
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            float rotationSpeed = mCharacterInfo != null ? mCharacterInfo.rotationSpeed : 8.0f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    /// <summary>
    /// 检查死亡状态
    /// </summary>
    private void CheckDeath()
    {
        if (mCharacterInfo != null && mCharacterInfo.HP <= 0)
        {
            if (mStateMachine.GetCurrentStateType() != typeof(DeathState))
            {
                mStateMachine.ChangeState(new DeathState(this));
            }
        }
    }
    
    /// <summary>
    /// 初始化状态转换条件
    /// </summary>
    private void InitializeStateTransitions()
    {
        // 可以从Idle转换到Move
        mStateMachine.AddTransition(typeof(IdleState), 
            () => moveValue.magnitude > 0.1f && !isActionPlaying, 
            typeof(MoveState));
        
        // 可以从Move转换到Idle
        mStateMachine.AddTransition(typeof(MoveState), 
            () => moveValue.magnitude < 0.1f && !isActionPlaying, 
            typeof(IdleState));
        
        // 可以从任何状态转换到Death（如果HP <= 0）
        mStateMachine.AddTransition(typeof(IdleState), 
            () => mCharacterInfo != null && mCharacterInfo.HP <= 0, 
            typeof(DeathState));
        mStateMachine.AddTransition(typeof(MoveState), 
            () => mCharacterInfo != null && mCharacterInfo.HP <= 0, 
            typeof(DeathState));
        mStateMachine.AddTransition(typeof(AttackState), 
            () => mCharacterInfo != null && mCharacterInfo.HP <= 0, 
            typeof(DeathState));
    }
}

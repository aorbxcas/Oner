using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine
{
    public State currentState;
    private Dictionary<Type, List<StateTransition>> transitions = new Dictionary<Type, List<StateTransition>>();
    
    /// <summary>
    /// 状态转换条件
    /// </summary>
    public class StateTransition
    {
        public Func<bool> condition;
        public Type targetStateType;
        
        public StateTransition(Func<bool> condition, Type targetStateType)
        {
            this.condition = condition;
            this.targetStateType = targetStateType;
        }
    }
    
    /// <summary>
    /// 添加状态转换条件
    /// </summary>
    public void AddTransition(Type fromStateType, Func<bool> condition, Type toStateType)
    {
        if (!transitions.ContainsKey(fromStateType))
        {
            transitions[fromStateType] = new List<StateTransition>();
        }
        transitions[fromStateType].Add(new StateTransition(condition, toStateType));
    }
    
    /// <summary>
    /// 检查并执行状态转换
    /// </summary>
    public bool CheckTransitions(CharaController controller)
    {
        if (currentState == null) return false;
        
        Type currentStateType = currentState.GetType();
        if (transitions.ContainsKey(currentStateType))
        {
            foreach (var transition in transitions[currentStateType])
            {
                if (transition.condition != null && transition.condition())
                {
                    // 创建新状态实例
                    State newState = CreateState(transition.targetStateType, controller);
                    if (newState != null)
                    {
                        ChangeState(newState);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    
    /// <summary>
    /// 创建状态实例
    /// </summary>
    private State CreateState(Type stateType, CharaController controller)
    {
        // 根据状态类型创建对应的状态实例
        if (stateType == typeof(IdleState))
            return new IdleState(controller);
        else if (stateType == typeof(MoveState))
            return new MoveState(controller, Vector2.zero);
        else if (stateType == typeof(AttackState))
            return new AttackState(controller);
        else if (stateType == typeof(DefendState))
            return new DefendState(controller);
        else if (stateType == typeof(DeathState))
            return new DeathState(controller);
        else if (stateType == typeof(OnHitState))
            return new OnHitState(controller, 0);
        else if (stateType == typeof(OnDefendHitState))
            return new OnDefendHitState(controller, 0);
        else if (stateType == typeof(RollState))
            return new RollState(controller, Vector2.zero);
        
        return null;
    }
    
    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            // 如果新状态类型与当前状态相同，则不转换
            if (currentState == newState) return;
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }
    
    public void Update()
    {
        if(currentState != null)
        {
            currentState.Execute();
        }
    }
    
    /// <summary>
    /// 获取当前状态类型
    /// </summary>
    public Type GetCurrentStateType()
    {
        return currentState?.GetType();
    }
}

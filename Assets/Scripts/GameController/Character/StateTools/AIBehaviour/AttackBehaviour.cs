using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击行为 - 当敌人在攻击范围内时执行攻击
/// </summary>
public class AttackBehaviour : AIBehaviour
{
    private EnemyInfo info;

    public AttackBehaviour(CharaController agent) : base(agent)
    {
        info = (EnemyInfo)agent.mCharacterInfo;
    }
    
    public override void Start()
    {
        // 停止移动，准备攻击
        agent.IdleInput();
    }
    
    public override void Update()
    {
        if (agent.target == null || agent.target.GetComponent<CharacterInfo>().HP <= 0)
        {
            // 目标已死亡，返回警戒状态
            agent.behaviourMachine.ChangeBehaviour(new AlertBehaviour(agent));
            return;
        }
        
        // 检查是否还在攻击范围内
        if (!info.IsInAttackRange(agent.target.transform.position))
        {
            // 超出攻击范围，切换为追击
            agent.behaviourMachine.ChangeBehaviour(new ChaseBehaviour(agent));
            return;
        }
        
        // 检查是否丢失目标
        if (info.IsTargetLost(agent.target.transform.position))
        {
            // 丢失目标，返回警戒状态
            agent.behaviourMachine.ChangeBehaviour(new AlertBehaviour(agent));
            return;
        }
        
        // 面向目标
        agent.LookAtPos(agent.target.transform.position);
        
        // 攻击冷却计时
        // 执行攻击
        agent.AttackInput();
    }
    
    public override void End()
    {
    }
}


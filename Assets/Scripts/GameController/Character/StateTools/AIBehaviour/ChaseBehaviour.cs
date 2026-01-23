using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追击行为 - 追击目标直到进入攻击范围或丢失目标
/// </summary>
public class ChaseBehaviour : AIBehaviour
{
    private EnemyInfo info;
    
    public ChaseBehaviour(CharaController agent) : base(agent)
    {
        info = (EnemyInfo)agent.mCharacterInfo;
    }
    
    public override void Start()
    {
        
    }

    public override void Update()
    {
        // 检查目标是否有效
        if (agent.target == null || agent.target.GetComponent<CharacterInfo>().HP <= 0)
        {
            // 目标已死亡，返回警戒或巡逻
            if (info.enablePatrol)
            {
                agent.behaviourMachine.ChangeBehaviour(new PatrolBehaviour(agent));
            }
            else
            {
                agent.behaviourMachine.ChangeBehaviour(new AlertBehaviour(agent));
            }
            return;
        }
        
        // 检查是否在攻击范围内
        if (info.IsInAttackRange(agent.target.transform.position))
        {
            // 进入攻击范围，切换为攻击行为
            agent.behaviourMachine.ChangeBehaviour(new AttackBehaviour(agent));
            return;
        }
        
        // 检查是否丢失目标
        if (info.IsTargetLost(agent.target.transform.position))
        {
            // 丢失目标，返回出生点
            agent.behaviourMachine.ChangeBehaviour(new ReturnBehaviour(agent));
            return;
        }
        
        // 继续追击
        agent.NavMove(agent.target);
    }
    
    public override void End()
    {

    }
}

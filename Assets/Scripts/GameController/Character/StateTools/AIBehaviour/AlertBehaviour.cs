using ARPGSimpleDemo.Skill;
using System;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// 警戒行为 - 在出生点附近警戒，检测目标
/// </summary>
public class AlertBehaviour : AIBehaviour
{
    private EnemyInfo info;
    
    public AlertBehaviour(CharaController agent) : base(agent)
    {
        info = (EnemyInfo)agent.mCharacterInfo;
    }

    public override void Start()
    {
        agent.IdleInput();
    }

    public override void Update()
    {
        DetectTarget();
        
        // 如果启用了巡逻且没有目标，可以考虑切换到巡逻
        // 这里保持警戒状态，等待目标出现
    }
    
    public override void End()
    {
        
    }

    private void DetectTarget()
    {
        var colliders = Physics.OverlapSphere(agent.transform.position, info.alertDistance);
        if (colliders == null || colliders.Length == 0) return;
        
        var array = CollectionHelper.Select<Collider, GameObject>(colliders, p => p.gameObject);
        array = CollectionHelper.FindAll<GameObject>(array,
            p => Array.IndexOf(info.selectorTargetTags, p.tag) >= 0
                && p.GetComponent<CharaController>() != null 
                && p.GetComponent<CharacterInfo>().HP > 0);
        
        if (array != null && array.Length > 0)
        {
            agent.target = array[UnityEngine.Random.Range(0, array.Length)];
            
            // 检查目标是否在攻击范围内
            if (info.IsInAttackRange(agent.target.transform.position))
            {
                agent.behaviourMachine.ChangeBehaviour(new AttackBehaviour(agent));
            }
            else
            {
                agent.behaviourMachine.ChangeBehaviour(new ChaseBehaviour(agent));
            }
            return;
        }
    }
}

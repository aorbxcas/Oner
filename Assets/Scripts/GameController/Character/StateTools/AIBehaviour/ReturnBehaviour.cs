using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 返回行为 - 当丢失目标后返回出生点
/// </summary>
public class ReturnBehaviour : AIBehaviour
{
    private EnemyInfo info;
    private float returnThreshold = 1f; // 到达出生点的距离阈值
    
    public ReturnBehaviour(CharaController agent) : base(agent)
    {
        info = (EnemyInfo)agent.mCharacterInfo;
    }
    
    public override void Start()
    {
        // 清除目标
        agent.target = null;
    }
    
    public override void Update()
    {
        // 检查是否有新目标进入警戒范围
        DetectTarget();
        
        // 如果还没有目标，继续返回
        if (agent.target == null)
        {
            // 检查是否到达出生点
            float distanceToSpawn = Vector3.Distance(agent.transform.position, info.spawnPosition);
            
            if (distanceToSpawn <= returnThreshold)
            {
                // 到达出生点，根据配置决定是巡逻还是警戒
                if (info.enablePatrol)
                {
                    agent.behaviourMachine.ChangeBehaviour(new PatrolBehaviour(agent));
                }
                else
                {
                    agent.behaviourMachine.ChangeBehaviour(new AlertBehaviour(agent));
                }
            }
            else
            {
                // 继续返回出生点
                agent.NavMove(info.spawnPosition);
            }
        }
    }
    
    public override void End()
    {
        
    }
    
    /// <summary>
    /// 检测目标
    /// </summary>
    private void DetectTarget()
    {
        var colliders = Physics.OverlapSphere(agent.transform.position, info.alertDistance);
        if (colliders == null || colliders.Length == 0) return;
        
        var array = CollectionHelper.Select<Collider, GameObject>(colliders, p => p.gameObject);
        array = CollectionHelper.FindAll<GameObject>(array,
            p => System.Array.IndexOf(info.selectorTargetTags, p.tag) >= 0
                && p.GetComponent<CharaController>() != null 
                && p.GetComponent<CharacterInfo>().HP > 0);
        
        if (array != null && array.Length > 0)
        {
            agent.target = array[UnityEngine.Random.Range(0, array.Length)];
            agent.behaviourMachine.ChangeBehaviour(new ChaseBehaviour(agent));
        }
    }
}


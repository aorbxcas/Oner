using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巡逻行为 - 在出生点附近巡逻
/// </summary>
public class PatrolBehaviour : AIBehaviour
{
    private EnemyInfo info;
    private Vector3 patrolTarget;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    
    public PatrolBehaviour(CharaController agent) : base(agent)
    {
        info = (EnemyInfo)agent.mCharacterInfo;
    }
    
    public override void Start()
    {
        // 生成第一个巡逻点
        GeneratePatrolTarget();
        isWaiting = false;
        waitTimer = 0f;
    }
    
    public override void Update()
    {
        // 检查是否有目标进入警戒范围
        DetectTarget();
        
        // 如果不在等待状态，执行巡逻移动
        if (!isWaiting)
        {
            // 移动到巡逻点
            agent.NavMove(patrolTarget);
            
            // 检查是否到达巡逻点
            if (Vector3.Distance(agent.transform.position, patrolTarget) < 0.5f)
            {
                // 到达巡逻点，开始等待
                isWaiting = true;
                waitTimer = 0f;
                agent.IdleInput();
            }
        }
        else
        {
            // 等待中
            waitTimer += Time.deltaTime;
            if (waitTimer >= info.patrolWaitTime)
            {
                // 等待结束，生成新的巡逻点
                GeneratePatrolTarget();
                isWaiting = false;
            }
        }
    }
    
    public override void End()
    {
        isWaiting = false;
        waitTimer = 0f;
    }
    
    /// <summary>
    /// 生成巡逻目标点
    /// </summary>
    private void GeneratePatrolTarget()
    {
        // 在出生点周围随机生成巡逻点
        Vector2 randomCircle = Random.insideUnitCircle * info.patrolRange;
        patrolTarget = info.spawnPosition + new Vector3(randomCircle.x, 0, randomCircle.y);
        
        // 确保巡逻点在NavMesh上
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(patrolTarget, out hit, info.patrolRange, UnityEngine.AI.NavMesh.AllAreas))
        {
            patrolTarget = hit.position;
        }
        else
        {
            // 如果找不到有效点，使用出生点
            patrolTarget = info.spawnPosition;
        }
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


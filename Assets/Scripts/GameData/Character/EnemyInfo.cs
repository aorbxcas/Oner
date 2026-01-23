using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : CharacterInfo
{
    [Header("警戒设置")]
    /// <summary>警戒距离</summary>
    public float alertDistance = 10f;
    
    /// <summary>攻击距离</summary>
    public float attackDistance = 2f;
    
    /// <summary>丢失目标距离（超过此距离停止追击）</summary>
    public float loseTargetDistance = 15f;
    
    /// <summary>巡逻范围</summary>
    public float patrolRange = 5f;
    
    [Header("目标选择")]
    /// <summary>目标标签</summary>
    public string[] selectorTargetTags;
    
    [Header("行为设置")]
    /// <summary>追敌寻路更新间隔</summary>
    public float updateInterval = 0.5f;
    
    /// <summary>攻击冷却时间</summary>
    public float attackCooldown = 1.5f;
    
    /// <summary>攻击间隔计时器</summary>
    [HideInInspector]
    public float attackTimer = 0f;
    
    /// <summary>是否启用巡逻</summary>
    public bool enablePatrol = true;
    
    /// <summary>巡逻点停留时间</summary>
    public float patrolWaitTime = 2f;
    
    /// <summary>出生点位置（用于返回）</summary>
    [HideInInspector]
    public Vector3 spawnPosition;
    
    /// <summary>是否在攻击范围内</summary>
    public bool IsInAttackRange(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.position, targetPosition) <= attackDistance;
    }
    
    /// <summary>是否在警戒范围内</summary>
    public bool IsInAlertRange(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.position, targetPosition) <= alertDistance;
    }
    
    /// <summary>是否丢失目标</summary>
    public bool IsTargetLost(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.position, targetPosition) > loseTargetDistance;
    }
    
    void Start()
    {
        spawnPosition = transform.position;
        attackTimer = 0f;
    }
}

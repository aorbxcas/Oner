using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class DeathState : State
{
    private float deathTimer = 0f;
    private float destroyDelay = 3f; // 死亡后销毁延迟时间
    
    public DeathState(CharaController characterController) : base(characterController)
    {
    }

    public override void Enter()
    {
        characterController.isActionPlaying = true;
        
        // 停止所有移动
        characterController.MoveInput(Vector2.zero);
        
        // 播放死亡动画
        this.SendCommand(new CharacterAnimationCommand(characterController, CharacterAnimationType.OnDamage));
        
        // 禁用碰撞器和导航
        var collider = characterController.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        
        var navAgent = characterController.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navAgent != null)
        {
            navAgent.enabled = false;
        }
        
        // 调用死亡回调
        characterController.Die();
        
        deathTimer = 0f;
    }

    public override void Execute()
    {
        deathTimer += Time.deltaTime;
        
        // 延迟销毁对象
        if (deathTimer >= destroyDelay)
        {
            // 可以在这里添加对象池回收逻辑
            // GameObjectPool.Instance.Recycle(characterController.gameObject);
            // 或者直接销毁
            // Object.Destroy(characterController.gameObject);
        }
    }

    public override void Exit()
    {
        // 死亡状态不应该退出，但保留接口以防需要
    }
}

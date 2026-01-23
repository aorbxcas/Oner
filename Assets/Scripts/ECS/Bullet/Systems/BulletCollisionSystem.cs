using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;


/// <summary>
/// 子弹碰撞系统 - 使用距离检测处理子弹与目标的碰撞
/// </summary>
public partial class BulletCollisionSystem : SystemBase
{
    private const float COLLISION_CHECK_RADIUS = 0.5f;

    protected override void OnUpdate()
    {
        var entityManager = EntityManager;
        var deltaTime = SystemAPI.Time.DeltaTime;

        // 获取所有可能的碰撞目标（有ICanOnDamage接口的GameObject）
        // 由于这是Hybrid ECS，我们使用GameObject.FindObjectsOfType来查找目标
        var targets = Object.FindObjectsOfType<MonoBehaviour>();
        
        // 遍历所有子弹
        foreach (var (bullet, transform, entity) in SystemAPI.Query<RefRO<BulletComponent>, RefRO<LocalTransform>>().WithEntityAccess())
        {
            float3 bulletPos = transform.ValueRO.Position;
            
            // 检查与所有目标的碰撞
            foreach (var target in targets)
            {
                if (target is ICanOnDamage && target.transform != null)
                {
                    float3 targetPos = target.transform.position;
                    float distance = math.distance(bulletPos, targetPos);
                    
                    if (distance < COLLISION_CHECK_RADIUS)
                    {
                        // 处理碰撞
                        ProcessBulletCollision(entity, target as ICanOnDamage, bullet.ValueRO, entityManager);
                        break; // 子弹击中目标后销毁，跳出循环
                    }
                }
            }
        }
    }

    private void ProcessBulletCollision(Entity bulletEntity, ICanOnDamage target, BulletComponent bullet, EntityManager entityManager)
    {
        // 造成伤害
        target.OnDamage(bullet.damage);
        
        // 销毁子弹
        entityManager.DestroyEntity(bulletEntity);
    }
}


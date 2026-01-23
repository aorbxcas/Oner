using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

/// <summary>
/// 子弹移动系统 - 处理子弹的移动和生命周期
/// </summary>
// 保持 BulletMovementSystem 继承 SystemBase
public partial class BulletMovementSystem : SystemBase
{
    private EntityCommandBufferSystem m_CommandBufferSystem;

    protected override void OnCreate()
    {
        RequireForUpdate<BulletComponent>();
        m_CommandBufferSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        // float deltaTime = SystemAPI.Time.DeltaTime;
        //
        // // 获取命令缓冲区
        // var ecb = m_CommandBufferSystem.CreateCommandBuffer();
        //
        // // 遍历所有有BulletComponent的实体
        // foreach (var (bullet, transform, entity) in 
        //          SystemAPI.Query<RefRW<BulletComponent>, RefRW<LocalTransform>>().WithEntityAccess())
        // {
        //     // 更新年龄
        //     bullet.ValueRW.age += deltaTime;
        //
        //     // 检查生命周期 - 使用命令缓冲区删除实体
        //     if (bullet.ValueRO.age >= bullet.ValueRO.lifetime)
        //     {
        //         ecb.DestroyEntity(entity);  // 使用命令缓冲区而不是直接删除
        //         continue;
        //     }
        //
        //     // 更新位置
        //     float3 velocity = bullet.ValueRO.velocity;
        //     transform.ValueRW.Position += velocity * deltaTime;
        // }
        //
        // // 将命令缓冲区调度到下一帧执行
        // m_CommandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}


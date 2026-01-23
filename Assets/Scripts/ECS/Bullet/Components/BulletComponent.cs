using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// 子弹组件 - 存储子弹的基本数据
/// </summary>
public struct BulletComponent : IComponentData
{
    public float3 velocity;        // 速度向量
    public float speed;            // 速度大小
    public float lifetime;         // 生命周期
    public float age;              // 当前年龄
    public int damage;             // 伤害值
    public Entity ownerEntity;     // 拥有者实体（可选，用于避免自伤）
}

/// <summary>
/// 子弹标签组件 - 用于标识子弹实体
/// </summary>
public struct BulletTag : IComponentData
{
}


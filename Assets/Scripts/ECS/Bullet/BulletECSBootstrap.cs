using Unity.Entities;
using UnityEngine;

/// <summary>
/// ECS子弹系统初始化脚本
/// 将此脚本添加到场景中的GameObject上，用于初始化ECS子弹系统
/// </summary>
public class BulletECSBootstrap : MonoBehaviour
{
    private void Start()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
        {
            Debug.LogError("BulletECSBootstrap: 无法找到默认World");
            return;
        }

        // 确保系统已创建
        var bulletMovementSystem = world.GetOrCreateSystemManaged<BulletMovementSystem>();
        var bulletSpawnerSystem = world.GetOrCreateSystemManaged<BulletSpawnerSystem>();
        var bulletCollisionSystem = world.GetOrCreateSystemManaged<BulletCollisionSystem>();

        Debug.Log("Bullet ECS系统已初始化");
    }
}


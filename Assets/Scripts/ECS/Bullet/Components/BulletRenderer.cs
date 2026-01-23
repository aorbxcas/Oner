using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

/// <summary>
/// 子弹渲染器 - 用于在场景中可视化子弹
/// 这是一个Hybrid组件，将ECS位置同步到GameObject
/// </summary>
public class BulletRenderer : MonoBehaviour
{
    private Entity bulletEntity;
    private EntityManager entityManager;

    public void Initialize(Entity entity, EntityManager em)
    {
        bulletEntity = entity;
        entityManager = em;
    }

    private void Update()
    {
        if (entityManager == null || !entityManager.Exists(bulletEntity))
        {
            // 实体已被销毁，销毁GameObject
            Destroy(gameObject);
            return;
        }

        // 同步ECS位置到GameObject
        if (entityManager.HasComponent<LocalTransform>(bulletEntity))
        {
            var transform = entityManager.GetComponentData<LocalTransform>(bulletEntity);
            this.transform.position = transform.Position;
            this.transform.rotation = transform.Rotation;
        }
    }
}


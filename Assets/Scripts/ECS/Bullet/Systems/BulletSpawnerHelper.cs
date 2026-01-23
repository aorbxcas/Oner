using UnityEngine;
using Unity.Entities;

/// <summary>
/// 子弹生成辅助类 - MonoBehaviour，用于在ECS系统中创建GameObject
/// </summary>
public class BulletSpawnerHelper : MonoBehaviour
{
    private static BulletSpawnerHelper instance;

    public static BulletSpawnerHelper Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("BulletSpawnerHelper");
                instance = go.AddComponent<BulletSpawnerHelper>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public GameObject CreateBulletVisual(GameObject prefab, Vector3 position, Quaternion rotation, Entity bulletEntity, EntityManager entityManager)
    {
        GameObject bulletGO;
        
        if (prefab != null)
        {
            bulletGO = Instantiate(prefab, position, rotation);
        }
        else
        {
            // 创建默认球体
            bulletGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bulletGO.name = "Bullet";
            bulletGO.transform.position = position;
            bulletGO.transform.localScale = Vector3.one * 0.2f;
            bulletGO.transform.rotation = rotation;
            
            // 移除碰撞器
            var collider = bulletGO.GetComponent<Collider>();
            if (collider != null)
            {
                Destroy(collider);
            }
        }
        
        // 添加渲染器组件
        var renderer = bulletGO.GetComponent<BulletRenderer>();
        if (renderer == null)
        {
            renderer = bulletGO.AddComponent<BulletRenderer>();
        }
        renderer.Initialize(bulletEntity, entityManager);
        
        return bulletGO;
    }
}


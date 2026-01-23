using UnityEngine;
using QFramework;
using Unity.Entities;

public class BulletWeapon : WeaponController
{
    [Header("子弹设置")]
    public Mesh bulletMesh; // 子弹网格（用于Hybrid渲染）
    public Material bulletMaterial; // 子弹材质（用于Hybrid渲染）
    public Transform firePoint; // 发射点
    public float bulletSpeed = 10f;
    public float bulletLifetime = 5000f;
    
    private float lastFireTime = 0f;

    protected override void Start()
    {
        base.Start();
        
        // 如果没有指定发射点，使用武器自身位置
        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    public override void Attack()
    {
        // 检查攻击间隔
        if (Time.time - lastFireTime < weaponInfo.attackInterval)
        {
            return;
        }

        // 发射子弹
        FireBullet();
        lastFireTime = Time.time;
    }

    private void FireBullet()
    {
        if (firePoint == null || weaponInfo.owner == null)
        {
            Debug.LogWarning("BulletWeapon: 发射点或拥有者未设置");
            return;
        }

        // 计算发射方向（从角色朝向或鼠标位置）
        Vector3 direction = GetFireDirection();
        
        // 通过ECS系统创建子弹（使用Hybrid渲染）
        var world = World.DefaultGameObjectInjectionWorld;
        if (world != null)
        {
            var bulletSpawner = world.GetExistingSystemManaged<BulletSpawnerSystem>();
            if (bulletSpawner != null)
            {
                // 如果没有指定Mesh，使用默认球体
                Mesh meshToUse = bulletMesh;
                if (meshToUse == null)
                {
                    // 使用Unity内置的默认球体网格（这是共享资源，不会被销毁）
                    GameObject tempSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    var meshFilter = tempSphere.GetComponent<MeshFilter>();
                    // 使用sharedMesh确保引用的是共享资源，即使GameObject被销毁也有效
                    meshToUse = meshFilter.sharedMesh;
                    // 注意：sharedMesh是Unity的内置资源，即使GameObject被销毁，mesh引用仍然有效
                    Destroy(tempSphere);
                    
                    // 验证mesh是否有效
                    if (meshToUse == null)
                    {
                        Debug.LogError("BulletWeapon: 无法获取默认球体网格");
                        return;
                    }
                }

                // 如果没有指定Material，使用默认材质
                Material materialToUse = bulletMaterial;
                if (materialToUse == null)
                {
                    // 查找Standard着色器
                    Shader standardShader = Shader.Find("Standard");
                    if (standardShader == null)
                    {
                        // 如果Standard着色器不存在，尝试使用URP/HDRP的默认着色器
                        standardShader = Shader.Find("Universal Render Pipeline/Lit") ?? 
                                        Shader.Find("HDRP/Lit") ?? 
                                        Shader.Find("Sprites/Default");
                    }
                    
                    if (standardShader != null)
                    {
                        materialToUse = new Material(standardShader);
                        // 设置材质颜色以便可见
                        materialToUse.color = Color.white;
                    }
                    else
                    {
                        Debug.LogError("BulletWeapon: 无法找到有效的着色器来创建默认材质");
                        return;
                    }
                }
                
                // 验证材质和网格是否有效
                if (meshToUse == null || materialToUse == null)
                {
                    Debug.LogError($"BulletWeapon: 材质或网格无效 - Mesh: {meshToUse != null}, Material: {materialToUse != null}");
                    return;
                }

                bulletSpawner.SpawnBullet(
                    firePoint.position,
                    direction,
                    bulletSpeed,
                    bulletLifetime,
                    weaponInfo.damage,
                    Entity.Null,
                    meshToUse,
                    materialToUse
                    // 如果需要关联拥有者实体，可以在这里设置
                );
            }
            else
            {
                Debug.LogWarning("BulletWeapon: 无法找到BulletSpawnerSystem，请确保系统已注册");
            }
        }
        else
        {
            Debug.LogWarning("BulletWeapon: 无法找到默认World");
        }
    }

    private Vector3 GetFireDirection()
    {
        // 优先使用鼠标位置
        if (Camera.main != null)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = firePoint.position.z;
            Vector3 direction = (mouseWorldPos - firePoint.position).normalized;
            return direction;
        }
        
        // 否则使用角色朝向
        return weaponInfo.owner.forward;
    }
}


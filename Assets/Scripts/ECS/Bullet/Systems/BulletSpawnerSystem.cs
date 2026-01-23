using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Entities.Graphics;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Rendering;

/// <summary>
/// 子弹生成器系统 - 负责创建和管理子弹实体
/// </summary>
public partial class BulletSpawnerSystem : SystemBase
{
    protected override void OnCreate()
    {
        // 系统不需要每帧更新，只在需要时创建子弹
        RequireForUpdate<BulletComponent>();
    }

    protected override void OnUpdate()
    {
        // 这个系统主要用于提供SpawnBullet方法
        // 实际更新逻辑在BulletMovementSystem中
    }

    /// <summary>
    /// 生成子弹实体（使用Hybrid渲染）
    /// </summary>
    public void SpawnBullet(
        Vector3 position,
        Vector3 direction,
        float speed,
        float lifetime,
        int damage,
        Entity ownerEntity,
        Mesh mesh = null,
        Material material = null
        )
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
        {
            Debug.LogError("BulletSpawnerSystem: 无法找到默认World");
            return;
        }

        var entityManager = world.EntityManager;
        
        // 创建子弹实体
        var bulletEntity = entityManager.CreateEntity();

        // 添加基础组件
        entityManager.AddComponent<BulletComponent>(bulletEntity);
        entityManager.AddComponent<BulletTag>(bulletEntity);
        entityManager.AddComponent<LocalTransform>(bulletEntity);
        entityManager.AddComponent<LocalToWorld>(bulletEntity);

        // 计算旋转（朝向移动方向）
        quaternion rotation = quaternion.LookRotationSafe(direction.normalized, math.up());

        // 设置位置和旋转
        var transform = LocalTransform.FromPositionRotation(position, rotation);
        entityManager.SetComponentData(bulletEntity, transform);
        
        // 手动计算并设置 LocalToWorld 矩阵（因为 LocalToWorldSystem 可能还没有运行）
        // LocalToWorld 矩阵用于渲染，必须立即设置正确
        // 使用 LocalTransform.ToMatrix() 方法将 LocalTransform 转换为矩阵
        float4x4 localToWorldMatrix = transform.ToMatrix();
        var localToWorld = new LocalToWorld { Value = localToWorldMatrix };
        entityManager.SetComponentData(bulletEntity, localToWorld);

        // 设置子弹数据
        var bulletComponent = new BulletComponent
        {
            velocity = direction.normalized * speed,
            speed = speed,
            lifetime = lifetime,
            age = 0f,
            damage = damage,
            ownerEntity = ownerEntity
        };
        entityManager.SetComponentData(bulletEntity, bulletComponent);

        // 使用Hybrid渲染 - 通过RenderMeshUtility添加渲染组件
        if (mesh != null && material != null)
        {
            // 验证材质和网格是否有效（Unity的null检查会处理已销毁的对象）
            try
            {
                // 尝试访问mesh和material的属性来验证它们是否有效
                var meshName = mesh.name;
                var materialName = material.name;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"BulletSpawnerSystem: Mesh或Material无效: {e.Message}");
                return;
            }
            
            // 创建RenderMeshArray（包含mesh和material）
            var renderMeshArray = new RenderMeshArray(new[] { material }, new[] { mesh });
            
            // 验证RenderMeshArray是否正确创建
            if (renderMeshArray.MeshReferences == null || renderMeshArray.MeshReferences.Length == 0)
            {
                Debug.LogError("BulletSpawnerSystem: RenderMeshArray的MeshReferences为空");
                return;
            }
            
            if (renderMeshArray.MaterialReferences == null || renderMeshArray.MaterialReferences.Length == 0)
            {
                Debug.LogError("BulletSpawnerSystem: RenderMeshArray的MaterialReferences为空");
                return;
            }
            
            // 验证Mesh和Material引用是否有效
            if (!renderMeshArray.MeshReferences[0].IsValid())
            {
                Debug.LogError("BulletSpawnerSystem: RenderMeshArray中的Mesh引用无效");
                return;
            }
            
            if (!renderMeshArray.MaterialReferences[0].IsValid())
            {
                Debug.LogError("BulletSpawnerSystem: RenderMeshArray中的Material引用无效");
                return;
            }
            
            // 验证网格边界是否合理（不应该为空或无效）
            var meshBounds = mesh.bounds;
            if (meshBounds.size.x == 0 && meshBounds.size.y == 0 && meshBounds.size.z == 0)
            {
                Debug.LogWarning($"BulletSpawnerSystem: 网格边界为空 - Mesh: {mesh.name}");
            }
            
            // 验证材质着色器是否支持 Entities Graphics
            if (material.shader == null)
            {
                Debug.LogError($"BulletSpawnerSystem: 材质没有着色器 - Material: {material.name}");
                return;
            }
            
            // 检查材质是否使用兼容的渲染管线
            string shaderName = material.shader.name;
            bool isCompatibleShader = shaderName.Contains("Universal") || 
                                     shaderName.Contains("HDRP") || 
                                     shaderName.Contains("Standard") ||
                                     shaderName.Contains("Sprites");
            
            if (!isCompatibleShader)
            {
                Debug.LogWarning($"BulletSpawnerSystem: 材质着色器可能不兼容 Entities Graphics - Shader: {shaderName}");
            }
            
            // 创建MaterialMeshInfo（索引到RenderMeshArray）
            // 注意：索引0会被编码为-1（通过ArrayIndexToStaticIndex），这是正常的
            // Material = -1, Mesh = -1 表示使用RenderMeshArray中的索引0
            var materialMeshInfo = MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0);
            
            // 验证MaterialMeshInfo（-1是正常的，表示索引0）
            if (materialMeshInfo.Material != -1 || materialMeshInfo.Mesh != -1)
            {
                Debug.LogWarning($"BulletSpawnerSystem: MaterialMeshInfo索引异常 - Material: {materialMeshInfo.Material}, Mesh: {materialMeshInfo.Mesh} (期望都是-1)");
            }
            
            // 创建RenderMeshDescription（使用默认渲染设置）
            // 注意：使用默认层（0），确保相机可以看到
            var renderMeshDescription = new RenderMeshDescription(
                shadowCastingMode: ShadowCastingMode.Off,
                receiveShadows: false,
                layer: 0,  // 使用默认层
                renderingLayerMask: 0xffffffff  // 所有渲染层
            );
            
            // 使用RenderMeshUtility添加所有必需的渲染组件
            RenderMeshUtility.AddComponents(
                bulletEntity,
                entityManager,
                renderMeshDescription,
                renderMeshArray,
                materialMeshInfo
            );
            
            // 验证所有必需的渲染组件是否已添加
            bool hasMaterialMeshInfo = entityManager.HasComponent<MaterialMeshInfo>(bulletEntity);
            bool hasRenderMeshArray = entityManager.HasComponent<RenderMeshArray>(bulletEntity);
            bool hasRenderBounds = entityManager.HasComponent<RenderBounds>(bulletEntity);
            bool hasWorldRenderBounds = entityManager.HasComponent<WorldRenderBounds>(bulletEntity);
            bool hasRenderFilterSettings = entityManager.HasComponent<RenderFilterSettings>(bulletEntity);
            bool hasLocalToWorld = entityManager.HasComponent<LocalToWorld>(bulletEntity);
            
            if (!hasMaterialMeshInfo)
            {
                Debug.LogError("BulletSpawnerSystem: 添加渲染组件失败，MaterialMeshInfo未找到");
                return;
            }
            
            if (!hasRenderMeshArray)
            {
                Debug.LogError("BulletSpawnerSystem: 添加渲染组件失败，RenderMeshArray未找到");
                return;
            }
            
            if (!hasRenderBounds)
            {
                Debug.LogError("BulletSpawnerSystem: 添加渲染组件失败，RenderBounds未找到");
                return;
            }
            
            if (!hasWorldRenderBounds)
            {
                Debug.LogError("BulletSpawnerSystem: 添加渲染组件失败，WorldRenderBounds未找到");
                return;
            }
            
            if (!hasRenderFilterSettings)
            {
                Debug.LogError("BulletSpawnerSystem: 添加渲染组件失败，RenderFilterSettings未找到");
                return;
            }
            
            // 获取并验证渲染设置
            var filterSettings = entityManager.GetSharedComponent<RenderFilterSettings>(bulletEntity);
            var renderBounds = entityManager.GetComponentData<RenderBounds>(bulletEntity);
            var localsToWorld = entityManager.GetComponentData<LocalToWorld>(bulletEntity);
            var localTransform = entityManager.GetComponentData<LocalTransform>(bulletEntity);
            
            // 验证位置是否有效（不应该全是0或NaN）
            if (math.any(math.isnan(localsToWorld.Position)) || math.all(localsToWorld.Position == 0))
            {
                Debug.LogWarning($"BulletSpawnerSystem: 子弹位置异常 - LocalToWorld: {localsToWorld.Position}, LocalTransform: {localTransform.Position}");
            }
            
            // 检查 WorldRenderBounds 是否正确设置
            WorldRenderBounds worldRenderBounds = default;
            bool hasWorldRenderBoundsData = entityManager.HasComponent<WorldRenderBounds>(bulletEntity);
            if (hasWorldRenderBoundsData)
            {
                worldRenderBounds = entityManager.GetComponentData<WorldRenderBounds>(bulletEntity);
            }
            
            // 手动计算 WorldRenderBounds（从 LocalToWorld 和 RenderBounds 计算）
            var transformedBounds = AABB.Transform(localsToWorld.Value, renderBounds.Value);
            
            // 如果 WorldRenderBounds 为空或无效，手动设置它
            // 检查 AABB 是否有效（中心不为0或大小不为0）
            bool isBoundsValid = !math.all(transformedBounds.Center == 0) || 
                                !math.all(transformedBounds.Extents == 0);
            
            if (!hasWorldRenderBoundsData || !isBoundsValid)
            {
                worldRenderBounds = new WorldRenderBounds { Value = transformedBounds };
                entityManager.SetComponentData(bulletEntity, worldRenderBounds);
                Debug.Log($"BulletSpawnerSystem: 手动设置 WorldRenderBounds: {worldRenderBounds.Value}");
            }
            
            // 检查材质属性
            bool isTransparent = material.IsKeywordEnabled("_SURFACE_TYPE_TRANSPARENT") || 
                               material.renderQueue >= 2500; // 透明渲染队列通常是 2500+
            Color materialColor = material.HasProperty("_Color") ? material.color : Color.white;
            
            Debug.Log($"BulletSpawnerSystem: 成功创建子弹实体\n" +
                     $"  - MaterialMeshInfo: Material={materialMeshInfo.Material}, Mesh={materialMeshInfo.Mesh}\n" +
                     $"  - 位置: LocalTransform={localTransform.Position}, LocalToWorld={localsToWorld.Position}\n" +
                     $"  - 渲染层: {filterSettings.Layer}, 渲染层遮罩: {filterSettings.RenderingLayerMask}\n" +
                     $"  - RenderBounds: {renderBounds.Value} (中心: {renderBounds.Value.Center}, 大小: {renderBounds.Value.Extents})\n" +
                     $"  - WorldRenderBounds: {worldRenderBounds.Value} (中心: {worldRenderBounds.Value.Center}, 大小: {worldRenderBounds.Value.Extents})\n" +
                     $"  - Mesh: {mesh.name} (顶点数: {mesh.vertexCount}), Material: {material.name}\n" +
                     $"  - 着色器: {material.shader.name}, 渲染队列: {material.renderQueue}\n" +
                     $"  - 材质颜色: {materialColor}, 是否透明: {isTransparent}");
            
            // 检查EntitiesGraphicsSystem是否存在
            var entitiesGraphicsSystem = world.GetExistingSystemManaged<EntitiesGraphicsSystem>();
            if (entitiesGraphicsSystem == null)
            {
                Debug.LogWarning("BulletSpawnerSystem: 未找到EntitiesGraphicsSystem，子弹可能无法渲染。请确保场景中有Entities Graphics系统。");
            }
            else
            {
                Debug.Log("BulletSpawnerSystem: EntitiesGraphicsSystem已找到，渲染系统正常");
            }
            
            // 检查相机是否能看到这个位置
            if (Camera.main != null)
            {
                float distanceToCamera = math.distance(localsToWorld.Position, (float3)Camera.main.transform.position);
                var cameraPos = Camera.main.transform.position;
                var cameraForward = Camera.main.transform.forward;
                var toBullet = (localsToWorld.Position - (float3)cameraPos);
                float angleToBullet = math.degrees(math.acos(math.dot(math.normalize(toBullet), cameraForward)));
                
                // 检查是否在相机视野内
                bool inCameraView = distanceToCamera < Camera.main.farClipPlane && 
                                   distanceToCamera > Camera.main.nearClipPlane &&
                                   angleToBullet < Camera.main.fieldOfView / 2f;
                
                Debug.Log($"BulletSpawnerSystem: 相机信息\n" +
                         $"  - 距离: {distanceToCamera}\n" +
                         $"  - 角度: {angleToBullet}° (FOV: {Camera.main.fieldOfView}°)\n" +
                         $"  - 在视野内: {inCameraView}\n" +
                         $"  - 相机层级遮罩: {Camera.main.cullingMask} (子弹层级: {filterSettings.Layer}, 层级位: {1 << filterSettings.Layer})");
                
                // 检查相机层级遮罩
                int layerBit = 1 << filterSettings.Layer;
                bool layerVisible = (Camera.main.cullingMask & layerBit) != 0;
                if (!layerVisible)
                {
                    Debug.LogWarning($"BulletSpawnerSystem: 警告！相机看不到层级 {filterSettings.Layer}。请检查相机的 Culling Mask 设置。");
                }
            }
        }
        else
        {
            Debug.LogWarning("BulletSpawnerSystem: Mesh或Material为空，子弹将不可见。请提供有效的Mesh和Material。");
        }
    }
}


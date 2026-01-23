using UnityEngine;
using Unity.Entities;

/// <summary>
/// 子弹Authoring组件 - 用于在编辑器中配置子弹
/// </summary>
public class BulletAuthoring : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public int damage = 10;
}


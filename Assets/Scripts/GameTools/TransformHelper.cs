using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Transform组件助手类
/// </summary>
class TransformHelper
{
    /// <summary>
    /// 面向目标方向
    /// </summary>
    /// <param name="targetDirection">目标方向</param>
    /// <param name="transform">需要转向的对象</param>
    /// <param name="rotationSpeed">转向速度</param>
    public static void LookAtTarget(Vector3 targetDirection, Transform transform,float rotationSpeed)
    {
        if (targetDirection != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
        }
    }

    /// <summary>
    /// 查找子物体（递归查找）
    /// </summary>
    /// <param name="trans">父物体</param>
    /// <param name="goName">子物体的名称</param>
    /// <returns>找到的相应子物体</returns>
    public static Transform FindChild(Transform trans, string goName)
    {
        Transform child = trans.Find(goName);
        if (child != null)
            return child;

        Transform go = null;
        for (int i = 0; i < trans.childCount; i++)
        {
            child = trans.GetChild(i);
            go = FindChild(child, goName);
            if (go != null)
                return go;
        }
        return null;
    }
    // 查找包含特定组件的父Transform
    public static Transform FindParentWithComponent<T>(Transform trans) where T : Component
    {
        // 如果当前Transform为空，返回空
        if (trans == null)
            return null;

        // 获取当前Transform的父Transform
        Transform parentTransform = trans.parent;

        // 如果父Transform为空，返回空
        if (parentTransform == null)
            return null;

        // 获取父Transform的GameObject并检查是否包含特定组件
        GameObject parentObject = parentTransform.gameObject;
        T component = parentObject.GetComponent<T>();

        // 如果包含特定组件，返回父Transform
        if (component != null)
            return parentTransform;
        else
            // 递归查找父Transform的父Transform
            return FindParentWithComponent<T>(parentTransform);
    }
}

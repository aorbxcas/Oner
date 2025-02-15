using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfoTools
{
    public static void GetAllComponentsInfo(GameObject gameObject)
    {
        // 获取当前游戏对象的所有组件
        Component[] components = gameObject.GetComponents<Component>();

        // 遍历所有组件并输出信息
        foreach (Component component in components)
        {
            // 获取组件的类型名称
            string componentName = component.GetType().Name;

            // 输出组件的类型名称
            Debug.Log("Component: " + componentName);

            // 如果需要，还可以输出组件的更多详细信息
            if (component is Transform transform)
            {
                Debug.Log("  Position: " + transform.position);
                Debug.Log("  Rotation: " + transform.rotation.eulerAngles);
                Debug.Log("  Scale: " + transform.localScale);
            }
            else if (component is Rigidbody rigidbody)
            {
                Debug.Log("  Mass: " + rigidbody.mass);
                Debug.Log("  Is Kinematic: " + rigidbody.isKinematic);
            }
            // 可以根据需要继续添加其他组件类型的详细信息输出
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfoTools
{
    public static void GetAllComponentsInfo(GameObject gameObject)
    {
        // ��ȡ��ǰ��Ϸ������������
        Component[] components = gameObject.GetComponents<Component>();

        // ������������������Ϣ
        foreach (Component component in components)
        {
            // ��ȡ�������������
            string componentName = component.GetType().Name;

            // ����������������
            Debug.Log("Component: " + componentName);

            // �����Ҫ���������������ĸ�����ϸ��Ϣ
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
            // ���Ը�����Ҫ�����������������͵���ϸ��Ϣ���
        }
    }
    public static GameObject FindChild(Transform trans, string childName)
    {
        Transform child = trans.Find(childName);
        if (child != null)
        {
            return child.gameObject;
        }
        int count = trans.childCount;
        GameObject go = null;
        for (int i = 0; i < count; ++i)
        {
            child = trans.GetChild(i);
            go = FindChild(child, childName);
            if (go != null)
                return go;
        }
        return null;
    }
    public static T FindChild<T>(Transform trans, string childName) where T : Component
    {
        GameObject go = FindChild(trans, childName);
        if (go == null)
            return null;
        return go.GetComponent<T>();
    }
}

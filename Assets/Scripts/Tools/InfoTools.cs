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
}

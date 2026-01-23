using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

/// <summary>
/// 背包UI测试脚本 - 用于测试背包功能
/// </summary>
public class BackpackUITest : MonoBehaviour
{
    [Header("测试按钮")]
    public KeyCode openBackpackKey = KeyCode.B;      // 打开背包按键
    public KeyCode addTestItemKey = KeyCode.T;       // 添加测试物品按键
    public KeyCode clearInventoryKey = KeyCode.C;    // 清空背包按键
    
    void Update()
    {
        // 打开/关闭背包
        if (Input.GetKeyDown(openBackpackKey))
        {
            bool isVisible = UIManager.Instance.IsVisible(UIName.UIBackpack);
            UIManager.Instance.SetVisible(UIName.UIBackpack, !isVisible);
        }
        
        // 添加测试物品
        if (Input.GetKeyDown(addTestItemKey))
        {
            InventoryHelper.AddTestItems();
        }
        
        // 清空背包
        if (Input.GetKeyDown(clearInventoryKey))
        {
            InventoryHelper.ClearInventory();
        }
    }
}


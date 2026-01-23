using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using QFramework;

/// <summary>
/// 物品槽位组件
/// </summary>
public class ItemSlotWidget : UISceneWidget, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI组件")]
    public Image itemIcon;           // 物品图标
    public TextMeshProUGUI countText; // 数量文本
    public Image background;        // 背景图片
    
    private int slotIndex;          // 槽位索引
    private ItemInfo currentItem;   // 当前物品
    private BackpackUI backpackUI;   // 背包UI引用
    
    /// <summary>
    /// 初始化槽位
    /// </summary>
    public void Initialize(int index, BackpackUI backpack)
    {
        slotIndex = index;
        backpackUI = backpack;
        currentItem = null;
        
        // 初始化UI
        if (itemIcon != null)
        {
            itemIcon.gameObject.SetActive(false);
        }
        
        if (countText != null)
        {
            countText.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// 更新槽位显示
    /// </summary>
    public void UpdateSlot(ItemInfo item)
    {
        currentItem = item;
        
        if (item == null)
        {
            // 空槽位
            if (itemIcon != null)
            {
                itemIcon.gameObject.SetActive(false);
            }
            
            if (countText != null)
            {
                countText.gameObject.SetActive(false);
            }
        }
        else
        {
            // 有物品
            if (itemIcon != null)
            {
                itemIcon.gameObject.SetActive(true);
                if (item.icon != null)
                {
                    itemIcon.sprite = item.icon;
                }
            }
            
            if (countText != null)
            {
                if (item.count > 1)
                {
                    countText.gameObject.SetActive(true);
                    countText.text = item.count.ToString();
                }
                else
                {
                    countText.gameObject.SetActive(false);
                }
            }
        }
    }
    
    /// <summary>
    /// 点击事件
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItem == null) return;
        
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // 左键：使用物品
            if (currentItem.itemType == ItemType.Consumable)
            {
                backpackUI?.UseItem(slotIndex);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // 右键：移除物品（可以改为显示详情或其他操作）
            backpackUI?.RemoveItem(slotIndex, 1);
        }
    }
    
    /// <summary>
    /// 鼠标进入
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            // 可以显示物品详情提示
            Debug.Log($"物品: {currentItem.itemName}\n描述: {currentItem.description}");
        }
    }
    
    /// <summary>
    /// 鼠标离开
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        // 隐藏物品详情提示
    }
}


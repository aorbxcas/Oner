using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QFramework;

/// <summary>
/// 背包UI
/// </summary>
public class BackpackUI : UIScene
{
    [Header("UI组件")]
    public GameObject itemSlotPrefab;          // 物品槽位预制体
    public Transform itemGridParent;            // 物品网格父对象
    public Button closeButton;                  // 关闭按钮
    public TextMeshProUGUI capacityText;        // 容量文本
    
    private InventoryModel inventoryModel;
    private List<ItemSlotWidget> itemSlots = new List<ItemSlotWidget>();
    private const int SLOTS_PER_ROW = 6;        // 每行槽位数量
    
    protected override void Start()
    {
        base.Start();
        
        inventoryModel = this.GetModel<InventoryModel>();
        
        // 注册事件
        this.RegisterEvent<ItemAddedEvent>(OnItemAdded).UnRegisterWhenGameObjectDestroyed(gameObject);
        this.RegisterEvent<ItemRemovedEvent>(OnItemRemoved).UnRegisterWhenGameObjectDestroyed(gameObject);
        this.RegisterEvent<ItemUsedEvent>(OnItemUsed).UnRegisterWhenGameObjectDestroyed(gameObject);
        this.RegisterEvent<InventoryUpdatedEvent>(OnInventoryUpdated).UnRegisterWhenGameObjectDestroyed(gameObject);
        
        // 初始化UI
        InitializeSlots();
        UpdateUI();
        
        // 绑定关闭按钮
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => {
                SetVisible(false);
            });
        }
    }
    
    /// <summary>
    /// 初始化物品槽位
    /// </summary>
    private void InitializeSlots()
    {
        if (itemGridParent == null) return;
        
        // 清空现有槽位
        foreach (Transform child in itemGridParent)
        {
            Destroy(child.gameObject);
        }
        itemSlots.Clear();
        
        // 创建槽位
        for (int i = 0; i < inventoryModel.maxCapacity; i++)
        {
            GameObject slotObj;
            if (itemSlotPrefab != null)
            {
                slotObj = Instantiate(itemSlotPrefab, itemGridParent);
            }
            else
            {
                // 如果没有预制体，创建一个简单的槽位
                slotObj = new GameObject($"ItemSlot_{i}");
                slotObj.transform.SetParent(itemGridParent);
                slotObj.AddComponent<RectTransform>();
                slotObj.AddComponent<Image>();
                slotObj.AddComponent<ItemSlotWidget>();
            }
            
            ItemSlotWidget slot = slotObj.GetComponent<ItemSlotWidget>();
            if (slot == null)
            {
                slot = slotObj.AddComponent<ItemSlotWidget>();
            }
            
            slot.Initialize(i, this);
            itemSlots.Add(slot);
        }
    }
    
    /// <summary>
    /// 更新UI显示
    /// </summary>
    private void UpdateUI()
    {
        if (inventoryModel == null) return;
        
        // 更新容量显示
        if (capacityText != null)
        {
            int usedSlots = inventoryModel.maxCapacity - inventoryModel.GetEmptySlotCount();
            capacityText.text = $"{usedSlots}/{inventoryModel.maxCapacity}";
        }
        
        // 更新所有槽位
        for (int i = 0; i < itemSlots.Count; i++)
        {
            ItemInfo item = inventoryModel.GetItemAtSlot(i);
            itemSlots[i].UpdateSlot(item);
        }
    }
    
    /// <summary>
    /// 物品添加事件处理
    /// </summary>
    private void OnItemAdded(ItemAddedEvent e)
    {
        UpdateUI();
    }
    
    /// <summary>
    /// 物品移除事件处理
    /// </summary>
    private void OnItemRemoved(ItemRemovedEvent e)
    {
        UpdateUI();
    }
    
    /// <summary>
    /// 物品使用事件处理
    /// </summary>
    private void OnItemUsed(ItemUsedEvent e)
    {
        UpdateUI();
    }
    
    /// <summary>
    /// 背包更新事件处理
    /// </summary>
    private void OnInventoryUpdated(InventoryUpdatedEvent e)
    {
        UpdateUI();
    }
    
    /// <summary>
    /// 使用物品
    /// </summary>
    public void UseItem(int slotIndex)
    {
        this.SendCommand(new UseItemCommand(slotIndex));
    }
    
    /// <summary>
    /// 移除物品
    /// </summary>
    public void RemoveItem(int slotIndex, int count = 1)
    {
        this.SendCommand(new RemoveItemAtSlotCommand(slotIndex, count));
    }
}


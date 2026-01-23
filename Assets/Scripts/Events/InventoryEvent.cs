using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using QFramework;

/// <summary>
/// 物品添加事件
/// </summary>
public class ItemAddedEvent : AbstractEvent
{
    public ItemInfo item;
    
    public ItemAddedEvent(ItemInfo item)
    {
        this.item = item;
    }
}

/// <summary>
/// 物品移除事件
/// </summary>
public class ItemRemovedEvent : AbstractEvent
{
    public int itemId;
    public int slotIndex;
    public int count;
    public bool isSlotIndex; // 是否为槽位索引
    
    public ItemRemovedEvent(int itemId, int count)
    {
        this.itemId = itemId;
        this.count = count;
        this.isSlotIndex = false;
    }
    
    public ItemRemovedEvent(int slotIndex, int count, bool isSlotIndex = true)
    {
        this.slotIndex = slotIndex;
        this.count = count;
        this.isSlotIndex = true;
    }
}

/// <summary>
/// 物品使用事件
/// </summary>
public class ItemUsedEvent : AbstractEvent
{
    public int slotIndex;
    public ItemInfo item;
    
    public ItemUsedEvent(int slotIndex, ItemInfo item)
    {
        this.slotIndex = slotIndex;
        this.item = item;
    }
}

/// <summary>
/// 背包更新事件
/// </summary>
public class InventoryUpdatedEvent : AbstractEvent
{
    
}

/// <summary>
/// 玩家状态改变事件
/// </summary>
public class PlayerStatusChangedEvent : AbstractEvent
{
    
}


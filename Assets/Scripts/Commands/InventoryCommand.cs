using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

/// <summary>
/// 添加物品命令
/// </summary>
public class AddItemCommand : AbstractCommand
{
    private ItemInfo item;
    
    public AddItemCommand(ItemInfo item)
    {
        this.item = item;
    }
    
    protected override void OnExecute()
    {
        var inventoryModel = this.GetModel<InventoryModel>();
        bool success = inventoryModel.AddItem(item);
        
        if (success)
        {
            this.SendEvent(new ItemAddedEvent(item));
        }
        else
        {
            Debug.LogWarning($"背包已满，无法添加物品: {item.itemName}");
        }
    }
}

/// <summary>
/// 移除物品命令
/// </summary>
public class RemoveItemCommand : AbstractCommand
{
    private int itemId;
    private int count;
    
    public RemoveItemCommand(int itemId, int count = 1)
    {
        this.itemId = itemId;
        this.count = count;
    }
    
    protected override void OnExecute()
    {
        var inventoryModel = this.GetModel<InventoryModel>();
        bool success = inventoryModel.RemoveItem(itemId, count);
        
        if (success)
        {
            this.SendEvent(new ItemRemovedEvent(itemId, count));
        }
    }
}

/// <summary>
/// 移除指定槽位物品命令
/// </summary>
public class RemoveItemAtSlotCommand : AbstractCommand
{
    private int slotIndex;
    private int count;
    
    public RemoveItemAtSlotCommand(int slotIndex, int count = 1)
    {
        this.slotIndex = slotIndex;
        this.count = count;
    }
    
    protected override void OnExecute()
    {
        var inventoryModel = this.GetModel<InventoryModel>();
        bool success = inventoryModel.RemoveItemAtSlot(slotIndex, count);
        
        if (success)
        {
            this.SendEvent(new ItemRemovedEvent(slotIndex, count));
        }
    }
}

/// <summary>
/// 使用物品命令
/// </summary>
public class UseItemCommand : AbstractCommand
{
    private int slotIndex;
    
    public UseItemCommand(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }
    
    protected override void OnExecute()
    {
        var inventoryModel = this.GetModel<InventoryModel>();
        ItemInfo item = inventoryModel.GetItemAtSlot(slotIndex);
        
        if (item == null) return;
        
        // 根据物品类型执行不同效果
        var playerDataModel = this.GetModel<PlayerDataModel>();
        
        if (item.hpRestore > 0)
        {
            playerDataModel.HP = Mathf.Min(playerDataModel.HP + item.hpRestore, playerDataModel.MaxHP);
            this.SendEvent(new PlayerStatusChangedEvent());
        }
        
        if (item.spRestore > 0)
        {
            playerDataModel.SP = Mathf.Min(playerDataModel.SP + item.spRestore, playerDataModel.MaxSP);
            this.SendEvent(new PlayerStatusChangedEvent());
        }
        
        // 消耗品使用后移除
        if (item.itemType == ItemType.Consumable)
        {
            inventoryModel.RemoveItemAtSlot(slotIndex, 1);
            this.SendEvent(new ItemUsedEvent(slotIndex, item));
        }
    }
}


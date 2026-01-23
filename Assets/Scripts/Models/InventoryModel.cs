using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

/// <summary>
/// 背包数据模型
/// </summary>
public class InventoryModel : AbstractModel
{
    /// <summary>
    /// 背包容量
    /// </summary>
    public int maxCapacity = 30;
    
    /// <summary>
    /// 物品列表（使用字典存储，key为物品ID，value为物品信息）
    /// </summary>
    private Dictionary<int, ItemInfo> items = new Dictionary<int, ItemInfo>();
    
    /// <summary>
    /// 物品槽位列表（按顺序存储）
    /// </summary>
    private List<ItemInfo> itemSlots = new List<ItemInfo>();
    
    protected override void OnInit()
    {
        // 初始化背包槽位
        for (int i = 0; i < maxCapacity; i++)
        {
            itemSlots.Add(null);
        }
    }
    
    /// <summary>
    /// 添加物品
    /// </summary>
    public bool AddItem(ItemInfo item)
    {
        if (item == null) return false;
        
        // 如果是可堆叠物品，尝试堆叠
        if (item.maxStack > 1)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i] != null && itemSlots[i].itemId == item.itemId)
                {
                    int canAdd = itemSlots[i].maxStack - itemSlots[i].count;
                    if (canAdd > 0)
                    {
                        int addCount = Mathf.Min(canAdd, item.count);
                        itemSlots[i].count += addCount;
                        item.count -= addCount;
                        
                        if (item.count <= 0)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        
        // 寻找空槽位
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i] == null)
            {
                itemSlots[i] = item;
                items[item.itemId] = item;
                return true;
            }
        }
        
        return false; // 背包已满
    }
    
    /// <summary>
    /// 移除物品
    /// </summary>
    public bool RemoveItem(int itemId, int count = 1)
    {
        if (!items.ContainsKey(itemId)) return false;
        
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i] != null && itemSlots[i].itemId == itemId)
            {
                if (itemSlots[i].count >= count)
                {
                    itemSlots[i].count -= count;
                    if (itemSlots[i].count <= 0)
                    {
                        items.Remove(itemId);
                        itemSlots[i] = null;
                    }
                    return true;
                }
            }
        }
        
        return false;
    }
    
    /// <summary>
    /// 移除指定槽位的物品
    /// </summary>
    public bool RemoveItemAtSlot(int slotIndex, int count = 1)
    {
        if (slotIndex < 0 || slotIndex >= itemSlots.Count) return false;
        if (itemSlots[slotIndex] == null) return false;
        
        ItemInfo item = itemSlots[slotIndex];
        if (item.count >= count)
        {
            item.count -= count;
            if (item.count <= 0)
            {
                items.Remove(item.itemId);
                itemSlots[slotIndex] = null;
            }
            return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// 获取物品
    /// </summary>
    public ItemInfo GetItem(int itemId)
    {
        return items.ContainsKey(itemId) ? items[itemId] : null;
    }
    
    /// <summary>
    /// 获取指定槽位的物品
    /// </summary>
    public ItemInfo GetItemAtSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= itemSlots.Count) return null;
        return itemSlots[slotIndex];
    }
    
    /// <summary>
    /// 获取所有物品
    /// </summary>
    public List<ItemInfo> GetAllItems()
    {
        List<ItemInfo> result = new List<ItemInfo>();
        foreach (var item in itemSlots)
        {
            if (item != null)
            {
                result.Add(item);
            }
        }
        return result;
    }
    
    /// <summary>
    /// 获取物品数量
    /// </summary>
    public int GetItemCount(int itemId)
    {
        int total = 0;
        foreach (var item in itemSlots)
        {
            if (item != null && item.itemId == itemId)
            {
                total += item.count;
            }
        }
        return total;
    }
    
    /// <summary>
    /// 检查是否有空槽位
    /// </summary>
    public bool HasEmptySlot()
    {
        return itemSlots.Contains(null);
    }
    
    /// <summary>
    /// 获取空槽位数量
    /// </summary>
    public int GetEmptySlotCount()
    {
        int count = 0;
        foreach (var slot in itemSlots)
        {
            if (slot == null) count++;
        }
        return count;
    }
    
    /// <summary>
    /// 清空背包
    /// </summary>
    public void Clear()
    {
        items.Clear();
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i] = null;
        }
    }
}


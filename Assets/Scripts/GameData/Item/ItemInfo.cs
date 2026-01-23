using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品类型枚举
/// </summary>
public enum ItemType
{
    Weapon,      // 武器
    Armor,       // 护甲
    Consumable,  // 消耗品
    Material,    // 材料
    Other        // 其他
}

/// <summary>
/// 物品信息类
/// </summary>
[System.Serializable]
public class ItemInfo
{
    public int itemId;              // 物品ID
    public string itemName;          // 物品名称
    public string description;       // 物品描述
    public ItemType itemType;        // 物品类型
    public int maxStack;             // 最大堆叠数量
    public Sprite icon;              // 物品图标
    public int count;                // 当前数量
    
    // 物品属性（根据类型不同而不同）
    public int attackBonus;          // 攻击力加成
    public int defenseBonus;         // 防御力加成
    public int hpRestore;            // 生命值恢复
    public int spRestore;            // 魔法值恢复
    
    public ItemInfo()
    {
        itemId = 0;
        itemName = "";
        description = "";
        itemType = ItemType.Other;
        maxStack = 1;
        count = 1;
        attackBonus = 0;
        defenseBonus = 0;
        hpRestore = 0;
        spRestore = 0;
    }
    
    public ItemInfo(int id, string name, ItemType type, int stack = 1)
    {
        itemId = id;
        itemName = name;
        itemType = type;
        maxStack = stack;
        count = 1;
        description = "";
        attackBonus = 0;
        defenseBonus = 0;
        hpRestore = 0;
        spRestore = 0;
    }
    
    /// <summary>
    /// 复制物品信息
    /// </summary>
    public ItemInfo Clone()
    {
        ItemInfo clone = new ItemInfo();
        clone.itemId = this.itemId;
        clone.itemName = this.itemName;
        clone.description = this.description;
        clone.itemType = this.itemType;
        clone.maxStack = this.maxStack;
        clone.icon = this.icon;
        clone.count = this.count;
        clone.attackBonus = this.attackBonus;
        clone.defenseBonus = this.defenseBonus;
        clone.hpRestore = this.hpRestore;
        clone.spRestore = this.spRestore;
        return clone;
    }
}


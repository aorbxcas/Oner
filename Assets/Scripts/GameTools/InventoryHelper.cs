using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

/// <summary>
/// 背包工具类 - 用于测试和初始化
/// </summary>
public class InventoryHelper : MonoBehaviour
{
    /// <summary>
    /// 添加测试物品
    /// </summary>
    public static void AddTestItems()
    {
        // 创建一些测试物品
        ItemInfo healthPotion = new ItemInfo(1, "生命药水", ItemType.Consumable, 10);
        healthPotion.description = "恢复50点生命值";
        healthPotion.hpRestore = 50;
        
        ItemInfo manaPotion = new ItemInfo(2, "魔法药水", ItemType.Consumable, 10);
        manaPotion.description = "恢复30点魔法值";
        manaPotion.spRestore = 30;
        
        ItemInfo sword = new ItemInfo(3, "铁剑", ItemType.Weapon, 1);
        sword.description = "增加10点攻击力";
        sword.attackBonus = 10;
        
        ItemInfo armor = new ItemInfo(4, "皮甲", ItemType.Armor, 1);
        armor.description = "增加5点防御力";
        armor.defenseBonus = 5;
        
        // 添加物品到背包
        Oner.Interface.SendCommand(new AddItemCommand(healthPotion));
        Oner.Interface.SendCommand(new AddItemCommand(manaPotion));
        Oner.Interface.SendCommand(new AddItemCommand(sword));
        Oner.Interface.SendCommand(new AddItemCommand(armor));
        
        // 添加多个生命药水测试堆叠
        for (int i = 0; i < 5; i++)
        {
            ItemInfo potion = healthPotion.Clone();
            potion.count = 1;
            Oner.Interface.SendCommand(new AddItemCommand(potion));
        }
    }
    
    /// <summary>
    /// 清空背包
    /// </summary>
    public static void ClearInventory()
    {
        var inventoryModel = Oner.Interface.GetModel<InventoryModel>();
        inventoryModel.Clear();
        Oner.Interface.SendEvent(new InventoryUpdatedEvent());
    }
}


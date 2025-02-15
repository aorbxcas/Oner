using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class PlayerDataModel : AbstractModel
{
    public int HP;
    public int MaxHP;
    public int SP;
    public int MaxSP;
    public int damage;
    public int attackSpeed;
    public int moveSpeed;
    protected override void OnInit()
    {
        
    }
    public void SetPlayerData(PlayerInfo playerData)
    {
        HP=playerData.HP;
        MaxHP=playerData.MaxHP;
        SP=playerData.SP;
        MaxSP=playerData.MaxSP;
        damage=playerData.damage;
        attackSpeed=playerData.attackSpeed;
        moveSpeed=playerData.moveSpeed;
    }
}

using UnityEngine;
using System.Collections;
using System;

//策略模式 将选择算法进行抽象
namespace ARPGSimpleDemo.Skill
{
    /// <summary>攻击目标选择算法</summary>
    public interface IAttackSelector
    {
        ///<summary>目标选择算法</summary>
        GameObject[] SelectTarget(WeaponInfo weaponInfo);
    }

}
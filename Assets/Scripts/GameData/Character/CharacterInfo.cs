using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class CharacterInfo:MonoBehaviour
{
    /// <summary>生命 </summary>
    public int HP = 100;
    /// <summary>生命 </summary>
    public int MaxHP = 100;
    /// <summary>当前魔法 </summary>
    public int SP = 100;
    /// <summary>最大魔法 </summary>
    public int MaxSP = 100;
    /// <summary>伤害基数</summary>
    public int damage = 100;
    /// <summary>攻击速度 </summary>
    public int attackSpeed = 5;
    /// <summary>移动速度 </summary>
    public int moveSpeed = 5;

    public float defendRatio = 0.8f;

    public float rotationSpeed = 8.0f;

    public string currentAnimation = "Idle";
    //public Dictionary<CharacterActionType, string> actionDictionary;
    //public Dictionary<CharacterAnimationType, string> animationDictionary;

    [HideInInspector]
    public Transform HitFxPos;

}

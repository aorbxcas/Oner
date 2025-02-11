using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class CharacterInfo:MonoBehaviour
{
    /// <summary>���� </summary>
    public int HP = 100;
    /// <summary>���� </summary>
    public int MaxHP = 100;
    /// <summary>��ǰħ�� </summary>
    public int SP = 100;
    /// <summary>���ħ�� </summary>
    public int MaxSP = 100;
    /// <summary>�˺�����</summary>
    public int damage = 100;
    /// <summary>�����ٶ� </summary>
    public int attackSpeed = 5;
    /// <summary>�ƶ��ٶ� </summary>
    public int moveSpeed = 5;

    public float defendRatio = 0.8f;

    public float rotationSpeed = 8.0f;

    public string currentAnimation = "Idle";
    //public Dictionary<CharacterActionType, string> actionDictionary;
    //public Dictionary<CharacterAnimationType, string> animationDictionary;

    [HideInInspector]
    public Transform HitFxPos;

}

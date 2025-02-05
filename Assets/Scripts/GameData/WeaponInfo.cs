using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    CloseWeapon,
    BulletWeapon,
    MagicWeapon
 }
public enum SelectorType
{
    Circle,
    Sector
}
public enum SelectorTag
{
    Self,
    Enemy,
    Teammate
}
public class WeaponInfo : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public Transform owner;
    public float attackInterval;
    public WeaponType weaponType { get; set; }

    public int distance;

    //  近战武器特有
    public SelectorType selectorType;
    public string[] selectorTags;
    public int attackAngle;
    private void Start()
    {
        owner = TransformHelper.FindParentWithComponent<CharaController>(this.transform);
    }
}

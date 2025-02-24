using ARPGSimpleDemo.Skill;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : WeaponController
{
    //  ����Ŀ��ѡ����
    private IAttackSelector selector;
    protected override void Start()
    {
        base.Start();
        switch (weaponInfo.selectorType)
        {
            case SelectorType.Circle:
                selector = SelectorFactory.CreateSelector(weaponInfo.selectorType);
                break;
            case SelectorType.Sector:
                selector = SelectorFactory.CreateSelector(weaponInfo.selectorType);
                break;
            default:
                selector = null;
                break;
        }
    }
    public override void Attack()
    {
        if (selector == null) return;
        //  ��Ŀ��ѡ������ȡ����Ŀ��
        var enemyArray = selector.SelectTarget(weaponInfo);

        if (enemyArray == null) return;
        foreach(var enemy in enemyArray)
        {
            if (enemy.GetComponent<ICanOnDamage>() != null)
            {
                enemy.GetComponent<ICanOnDamage>().OnDamage(weaponInfo.damage);
                //  this.SendCommand(new CharacterActionCommand(enemy.GetComponent<CharaController>(), new ChacterActionParams { ActionType = CharacterActionType.OnDamage, damage = weaponInfo.damage }));

            }
        }

    }
}

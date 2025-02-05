using System;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGSimpleDemo.Skill
{
	class CircleAttackSelector : IAttackSelector 
	{
        public GameObject[] SelectTarget(WeaponInfo weaponInfo)
        {
            //发一个球形射线，找出所有碰撞体
            var colliders = Physics.OverlapSphere(weaponInfo.owner.position, weaponInfo.distance);
            if (colliders == null || colliders.Length == 0) return null;
   
            //从碰撞体列表中挑出所有的敌人
            var array = CollectionHelper.Select<Collider, GameObject>(colliders, p => p.gameObject);
            array = CollectionHelper.FindAll<GameObject>(array,
                p => Array.IndexOf(weaponInfo.selectorTags, p.tag) >= 0
                    && p.GetComponent<CharaController>()!=null && p.GetComponent<CharacterInfo>().HP > 0);

            if (array == null || array.Length == 0) return null;

            //GameObject[] targets = null;
            ////根据技能是单体还是群攻，决定返回多少敌人对象
            //if (skillData.attackType == SkillAttackType.Single)
            //{
            //    //将所有的敌人，按与技能的发出者之间的距离升序排列，
            //    CollectionHelper.OrderBy<GameObject, float>(array,
            //    p => Vector3.Distance(skillData.Owner.transform.position, p.transform.position));
            //    targets = new GameObject[] { array[0] };
            //}
            //else if (skillData.attackType == SkillAttackType.Group)
            //    targets = array;

            GameObject[] targets = array;

            foreach(var i in targets)
            {
                Debug.Log(i.name);
            }
            return targets;

        }
    }
}

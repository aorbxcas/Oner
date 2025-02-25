﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//简单工厂  
namespace ARPGSimpleDemo.Skill
{
    //创建敌人选择器
    public class SelectorFactory
    {
        //攻击目标选择器缓存
        private static  Dictionary<string, IAttackSelector> cache = new Dictionary<string, IAttackSelector>();

        public static IAttackSelector CreateSelector(SelectorType selectorType)
        {
            //没有缓存则创建
            if (!cache.ContainsKey(selectorType.ToString()))
            {
                var nameSpace = typeof(SelectorFactory).Namespace;
                string classFullName = string.Format("{0}AttackSelector", selectorType.ToString());

                if (!String.IsNullOrEmpty(nameSpace))
                    classFullName = nameSpace + "." + classFullName;

                Type type = Type.GetType(classFullName);
                cache.Add(selectorType.ToString(), Activator.CreateInstance(type) as IAttackSelector);
            }
            //从缓存中取得创建好的选择器对象
            return cache[selectorType.ToString()];
        }
    }
}

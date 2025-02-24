using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : CharacterInfo
{
    // 警戒距离
    public int alertDistance;

    public string[] selectorTargetTags;

    // 追逐寻路点更新间隔
    public float updateInterval = 0.5f;
    void Start()
    {
        Debug.Log(selectorTargetTags);
    }
}

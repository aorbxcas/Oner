using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : CharacterInfo
{
    // �������
    public int alertDistance;

    public string[] selectorTargetTags;

    // ׷��Ѱ·����¼��
    public float updateInterval = 0.5f;
    void Start()
    {
        Debug.Log(selectorTargetTags);
    }
}

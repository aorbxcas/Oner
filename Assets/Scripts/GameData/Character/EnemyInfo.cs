using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : CharacterInfo
{
    // æØΩ‰æ‡¿Î
    public int alertDistance;

    public string[] selectorTargetTags;
    void Start()
    {
        Debug.Log(selectorTargetTags);
    }
}

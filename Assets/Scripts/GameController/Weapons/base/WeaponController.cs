using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour,IController
{
    public WeaponInfo weaponInfo;

    public IArchitecture GetArchitecture()
    {
        return Oner.Interface;
    }
    public abstract void Attack();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        weaponInfo = GetComponent<WeaponInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

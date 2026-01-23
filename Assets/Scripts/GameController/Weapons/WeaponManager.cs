using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class WeaponManager : MonoBehaviour, IController
{
    [Header("武器列表")]
    public List<WeaponController> weapons = new List<WeaponController>();
    
    private int currentWeaponIndex = 0;
    private CharaController characterController;
    public IArchitecture GetArchitecture()
    {
        return Oner.Interface;
    }

    private void Start()
    {
        characterController = GetComponent<CharaController>();
        
        // 初始化时激活第一把武器
        if (weapons.Count > 0)
        {
            SwitchWeapon(0);
        }
    }

    private void Update()
    {
        // 检测数字键输入（1-9）
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                int weaponIndex = i - 1; // 1键对应索引0，2键对应索引1，以此类推
                if (weaponIndex < weapons.Count)
                {
                    SwitchWeapon(weaponIndex);
                }
            }
        }
    }

    public void SwitchWeapon(int index)
    {
        if (index < 0 || index >= weapons.Count)
        {
            Debug.LogWarning($"WeaponManager: 无效的武器索引 {index}");
            return;
        }

        // 禁用当前武器
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }

        // 激活新武器
        currentWeaponIndex = index;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
        
        // 更新角色控制器的武器引用
        if (characterController != null)
        {
            characterController.mWeapon = weapons[currentWeaponIndex];
        }

        Debug.Log($"切换到武器: {weapons[currentWeaponIndex].weaponInfo.weaponName}");
    }

    public void AddWeapon(WeaponController weapon)
    {
        if (weapon != null && !weapons.Contains(weapon))
        {
            weapons.Add(weapon);
            weapon.gameObject.SetActive(false); // 默认禁用，等待切换时激活
        }
    }

    public WeaponController GetCurrentWeapon()
    {
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            return weapons[currentWeaponIndex];
        }
        return null;
    }
}


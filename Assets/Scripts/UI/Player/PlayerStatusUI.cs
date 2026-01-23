using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QFramework;

/// <summary>
/// 人物状态UI
/// </summary>
public class PlayerStatusUI : UIScene
{
    [Header("生命值相关")]
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    
    [Header("魔法值相关")]
    public Slider spSlider;
    public TextMeshProUGUI spText;
    
    [Header("属性显示")]
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI moveSpeedText;
    
    private PlayerDataModel playerDataModel;
    
    protected override void Start()
    {
        base.Start();
        
        playerDataModel = this.GetModel<PlayerDataModel>();
        
        // 注册事件
        this.RegisterEvent<PlayerStatusChangedEvent>(OnPlayerStatusChanged).UnRegisterWhenGameObjectDestroyed(gameObject);
        
        // 初始化UI
        UpdateUI();
    }
    
    /// <summary>
    /// 更新UI显示
    /// </summary>
    private void UpdateUI()
    {
        if (playerDataModel == null) return;
        
        // 更新HP
        if (hpSlider != null)
        {
            hpSlider.value = playerDataModel.MaxHP > 0 ? (float)playerDataModel.HP / (float)playerDataModel.MaxHP : 0;
        }
        
        if (hpText != null)
        {
            hpText.text = $"{playerDataModel.HP}/{playerDataModel.MaxHP}";
        }
        
        // 更新SP
        if (spSlider != null)
        {
            spSlider.value = playerDataModel.MaxSP > 0 ? (float)playerDataModel.SP / (float)playerDataModel.MaxSP : 0;
        }
        
        if (spText != null)
        {
            spText.text = $"{playerDataModel.SP}/{playerDataModel.MaxSP}";
        }
        
        // 更新属性
        if (attackText != null)
        {
            attackText.text = $"攻击: {playerDataModel.damage}";
        }
        
        if (defenseText != null)
        {
            defenseText.text = $"防御: {playerDataModel.attackSpeed}"; // 这里可能需要根据实际属性调整
        }
        
        if (moveSpeedText != null)
        {
            moveSpeedText.text = $"移速: {playerDataModel.moveSpeed}";
        }
    }
    
    /// <summary>
    /// 玩家状态改变事件处理
    /// </summary>
    private void OnPlayerStatusChanged(PlayerStatusChangedEvent e)
    {
        UpdateUI();
    }
    
    void Update()
    {
        // 每帧更新（也可以改为事件驱动）
        UpdateUI();
    }
}


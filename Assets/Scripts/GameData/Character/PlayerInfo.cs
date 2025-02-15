using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class PlayerInfo : CharacterInfo, IController
{
    private PlayerDataModel playerDataModel;
    void Start()
    {
        playerDataModel = this.GetModel<PlayerDataModel>();
    }
    void Update()
    {
        playerDataModel.SetPlayerData(this);
    }

    public IArchitecture GetArchitecture()
    {
        return Oner.Interface;
    }
}

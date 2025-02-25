using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Oner : Architecture<Oner>
{
    protected override void Init()
    {
        this.RegisterModel(new PlayerDataModel());

        this.RegisterSystem(new GameStateSystem());

        this.RegisterSystem(new AudioManagerSystem());
    }
}

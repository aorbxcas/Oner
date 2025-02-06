using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public abstract class State : IController
{
    protected CharaController characterController;
    public State(CharaController characterController)
    {
        this.characterController = characterController;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    public IArchitecture GetArchitecture()
    {
        return Oner.Interface;
    }
}

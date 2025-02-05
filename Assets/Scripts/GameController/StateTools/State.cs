using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected CharaController characterController;
    public State(CharaController characterController)
    {
        this.characterController = characterController;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}

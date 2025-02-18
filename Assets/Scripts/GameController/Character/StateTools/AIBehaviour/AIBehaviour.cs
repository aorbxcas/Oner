using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using QFramework;

public abstract class AIBehaviour : IController
{
    protected CharaController agent;
    protected StateMachine stateMachine;
    public AIBehaviour(CharaController agent)
    {
        this.agent = agent;
        stateMachine = agent.mStateMachine;
    }
    public abstract void Start();
    public abstract void Update();
    public abstract void End();

    public IArchitecture GetArchitecture()
    {
        return Oner.Interface;
    }
}

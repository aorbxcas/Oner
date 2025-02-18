using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : AIBehaviour
{
    public ChaseBehaviour(CharaController agent) : base(agent)
    {
    }
    public override void Start()
    {
        
    }

    public override void Update()
    {
        agent.MoveToTarget(agent.target);
    }
    public override void End()
    {

    }
}

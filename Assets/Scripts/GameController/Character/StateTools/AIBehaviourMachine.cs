using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviourMachine
{
    public AIBehaviour currentBehaviour;
    public void ChangeBehaviour(AIBehaviour newBehaviour)
    {
        if (currentBehaviour != null)
        {
            currentBehaviour.End();
        }
        currentBehaviour = newBehaviour;
        currentBehaviour.Start();
    }
    public void Update()
    {
        if (currentBehaviour != null)
        {
            currentBehaviour.Update();
        }
    }
}

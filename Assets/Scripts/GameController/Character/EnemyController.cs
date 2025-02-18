using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : CharaController
{

    public GameObject target;
    public AIBehaviourMachine behaviourMachine;

    public TextMeshProUGUI debugText;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        behaviourMachine = new AIBehaviourMachine();
        behaviourMachine.ChangeBehaviour(new AlertBehaviour(this));
    }

    // Update is called once per frame
    void Update()
    {
        if (debugText != null && behaviourMachine.currentBehaviour != null)//  显示行为机信息
        {
            debugText.text = behaviourMachine.currentBehaviour.GetType().ToString();
        }
        behaviourMachine.Update();
        mStateMachine.Update();
        MoveInput(moveValue);
    }

    public void MoveToTarget(GameObject targetObj)
    {
        LookAtTarget(targetObj);
        Vector3 targetDirection3 = targetObj.transform.position - transform.position;
        Vector2 targetDirection2 = new Vector2(targetDirection3.x, targetDirection3.z);
        moveValue = targetDirection2.normalized;
    }

    private void LookAtTarget(GameObject targetObj)
    {
        var targetRotation = Quaternion.LookRotation((targetObj.transform.position - transform.position).normalized, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.01f);
    }
    
}

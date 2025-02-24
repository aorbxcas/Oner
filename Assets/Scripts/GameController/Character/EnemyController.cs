using MoonSharp.VsCodeDebugger.SDK;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : CharaController
{
    public GameObject target;
    public AIBehaviourMachine behaviourMachine;

    public TextMeshProUGUI debugText;

    [HideInInspector]
    // 追逐寻路相关
    private NavMeshAgent navAgent;
    private Vector3[] pathCorners = { }; // 追逐寻路点
    private int currentCornerIndex = 0;
    private float lastUpdateTime; // 追逐间隔更新计算点

    protected override void Start()
    {
        base.Start();

        behaviourMachine = new AIBehaviourMachine();
        behaviourMachine.ChangeBehaviour(new AlertBehaviour(this));

        // 寻路相关
        navAgent = GetComponent<NavMeshAgent>();
        lastUpdateTime = Time.time;
    }

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

    public void NavMove(GameObject targetObj)
    {
        if (Time.time - lastUpdateTime > (mCharacterInfo as EnemyInfo).updateInterval)
        {
            UpdatePath(targetObj);
            lastUpdateTime = Time.time;
        }
        if (pathCorners.Length > 0)
        {
            if(currentCornerIndex>pathCorners.Length - 1)
            {
                UpdatePath(targetObj);
                return;
            }

            Vector3 nextCorner = pathCorners[currentCornerIndex];
            MoveToPos(nextCorner);
           
            if (Vector3.Distance(transform.position, nextCorner) < 0.1f)
            {
                currentCornerIndex++;
            }
        }
    }
    private void UpdatePath(GameObject targetObj)
    {
        NavMeshPath path = new NavMeshPath();
        navAgent.CalculatePath(targetObj.transform.position, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            pathCorners = path.corners;
            currentCornerIndex = 0;
        }
    }
    private void MoveToPos(Vector3 targetPos)
    {
        LookAtPos(targetPos);
        Vector3 targetDirection3 = targetPos - transform.position;
        Vector2 targetDirection2 = new Vector2(targetDirection3.x, targetDirection3.z);
        moveValue = targetDirection2.normalized;
    }

    private void LookAtPos(Vector3 targetPos)
    {
        var targetRotation = Quaternion.LookRotation((targetPos - transform.position).normalized, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.01f);
    }
    

}

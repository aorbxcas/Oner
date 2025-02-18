using ARPGSimpleDemo.Skill;
using System;
using System.Diagnostics;
using UnityEngine;
public class AlertBehaviour : AIBehaviour
{
    private EnemyInfo info;
    public AlertBehaviour(CharaController agent) : base(agent)
    {
        info = (EnemyInfo)agent.mCharacterInfo;
    }

    public override void Start()
    {
        agent.IdleInput();
    }

    public override void Update()
    {
        DetectTarget();
    }
    public override void End()
    {
        
    }

    private void DetectTarget()
    {
        var colliders = Physics.OverlapSphere(agent.transform.position, info.alertDistance);
        if (colliders == null || colliders.Length == 0) return;
        else
        {
            var array = CollectionHelper.Select<Collider, GameObject>(colliders, p => p.gameObject);
            array = CollectionHelper.FindAll<GameObject>(array,
                p => Array.IndexOf(info.selectorTargetTags, p.tag) >= 0
                    && p.GetComponent<CharaController>() != null && p.GetComponent<CharacterInfo>().HP > 0);
            if (array == null || array.Length == 0) return;
            else
            {
                agent.target = array[UnityEngine.Random.Range(0, array.Length - 1)];
                agent.behaviourMachine.ChangeBehaviour(new ChaseBehaviour(agent));
                return;
            }
                
        }
    }

}

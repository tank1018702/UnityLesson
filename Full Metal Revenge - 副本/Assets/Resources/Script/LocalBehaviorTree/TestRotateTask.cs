using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using MoenenGames.VoxelRobot;

public class TestRotateTask : Action
{
    public SharedGameObject target;

    EnemyTurret turret;

    public override void OnAwake()
    {
        base.OnAwake();

        turret = transform.GetChild(0).GetChild(0).GetComponent<EnemyTurret>();
    }
    public override TaskStatus OnUpdate()
    {
        if(target.Value)
        {
            turret.RotateToTarget(target.Value);

            return TaskStatus.Running;
        }
        
        return TaskStatus.Failure;
    }

}

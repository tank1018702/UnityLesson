using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoenenGames.VoxelRobot;
using BehaviorDesigner.Runtime;

public class TestFireTask : Action
{

    public SharedGameObject Target;

    Weapon[] weapons;

    public override void OnAwake()
    {
        base.OnAwake();

        weapons = transform.GetChild(0).GetChild(0).GetComponentsInChildren<Weapon>(true);
    }

    public override void OnStart()
    {
        base.OnStart();

    }

    public override TaskStatus OnUpdate()
    {
        if (Target.Value)
        {
            foreach (var weapon in weapons)
            {
                weapon.AttackUpdate();
            }

            return TaskStatus.Running;
           
        }
        return TaskStatus.Failure;
    }




}

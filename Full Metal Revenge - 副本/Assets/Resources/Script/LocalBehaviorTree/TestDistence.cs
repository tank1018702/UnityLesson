using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;


public class TestDistence : Conditional
{
    public SharedGameObject target;

    public SharedFloat attackrange;

    public override TaskStatus OnUpdate()
    {
        if(target.Value)
        {
            if(Vector3.Distance(target.Value.transform.position,transform.position)<attackrange.Value)
            {
                return TaskStatus.Failure;
            }
        }
        return TaskStatus.Success;
    }



}

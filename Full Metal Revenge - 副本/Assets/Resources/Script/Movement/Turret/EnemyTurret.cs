using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoenenGames.VoxelRobot;

public class EnemyTurret : Turret
{
    [SerializeField]
    private float RotateSpeed = 720f;
   protected override void Start()
    {
        transform.rotation = transform.parent.parent.rotation;
    }


    //protected override void Update()
    //{
    //    base.Update();
    //    RotateToTarget(Target);
    //}

    public void RotateToTarget(GameObject target)
    {
        if (target)
        {
            Quaternion aimRot = Quaternion.RotateTowards(transform.rotation,
           Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up), Time.deltaTime * RotateSpeed);
            Rotate(aimRot.eulerAngles.y);
        }
        transform.forward = transform.parent.forward;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoenenGames.VoxelRobot;

public class EnemyTurret : Turret
{
    [SerializeField]
    private float RotateSpeed = 720f;

    public GameObject Target;

    public float DetectionDistance;


    protected override void Update ()
    {
        base.Update();
        if(Target)
        {
            Quaternion aimRot = Quaternion.RotateTowards(transform.rotation,
           Quaternion.LookRotation(Target.transform.position - transform.position, Vector3.up), Time.deltaTime * RotateSpeed);
            Rotate(aimRot.eulerAngles.y);
        }
	}

    
}

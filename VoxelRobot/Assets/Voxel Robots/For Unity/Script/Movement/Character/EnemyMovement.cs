using MoenenGames.VoxelRobot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : RobotMovement
{
    public GameObject Target;

    private GameObject targetparent;
    NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        targetparent = GameObject.Find("Player");
    }

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.angularSpeed = RotateSpeed;
        agent.speed = MoveSpeed;
        if(targetparent)
        {
            Target = targetparent.transform.GetChild(0).gameObject;

            transform.GetChild(0).transform.GetChild(0).GetComponent<EnemyTurret>().Target = Target;
            
            
        }
        else
        {
            Debug.Log("target miss");
        }
	}
    protected override void Update()
    {
        base.Update();
        

        if(Target)
        {
            agent.SetDestination(Target.transform.position);
        }
    }

    
    
}

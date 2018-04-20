using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayColtrol : MonoBehaviour
{
    private NavMeshAgent Agent;

    public GameObject target;

	
	void Start ()
    {
        Agent = GetComponent<NavMeshAgent>();
	}
	
	
	void Update ()
    {
        
        Agent.SetDestination(target.transform.position);
	}
}

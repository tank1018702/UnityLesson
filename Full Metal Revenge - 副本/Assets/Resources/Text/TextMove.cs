using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TextMove : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;
	
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Right_1");
        agent.SetDestination(target.transform.position);
	}
	
	
	void Update ()
    {
		
	}
}

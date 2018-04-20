using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class RobotDemo : MonoBehaviour
{
   public NavMeshAgent NavMesh;

    Transform point;
    private void Awake()
    {
        NavMesh = GetComponent<NavMeshAgent>();
    }
    void Start ()
    {
        
        
        
        

    }
	
	
	void Update ()
    {
		if(NavMesh.remainingDistance<=0.5)
        {
            Destroy(gameObject);
        }
	}
}

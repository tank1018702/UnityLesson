using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public GameObject Missile;


	
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        Launch();
		
	}
    private void Launch()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           GameObject missile= Instantiate(Missile, transform.position, transform.rotation);
            missile.gameObject.SetActive(true);
        
        }
    }
}

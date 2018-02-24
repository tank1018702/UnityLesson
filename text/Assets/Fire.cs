using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bullet;


	
	
	
	void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            GameObject bulletgo = Instantiate(bullet, transform.position, transform.rotation);
            Vector3 dir = bulletgo.transform.localToWorldMatrix * Vector3.up;
            bulletgo.GetComponent<Rigidbody>().AddForce(dir * 200);
        
        }
		
	}
}

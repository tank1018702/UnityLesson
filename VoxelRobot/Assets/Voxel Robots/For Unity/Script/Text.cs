using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{

	
	void Start () {
		
	}
	
	
	void Update ()
    {
		 if(Input.GetMouseButton(1))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,2000f))
            {
                
                Collider[] col = Physics.OverlapSphere(hit.point, 10f);
                Debug.Log(col.Length);
                for (int i=0;i<col.Length;i++)
                {
                    if(col[i].GetComponent<Rigidbody>())
                    {
                        col[i].GetComponent<Rigidbody>().AddExplosionForce(400, hit.point, 10f);
                    }
                    
                }
            }
        }

	}
}

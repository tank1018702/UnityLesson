using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject Target;
    public float MoveSpeed=3;
    Vector3 Angle;
	
	void Start ()
    {
		
	}
	
	
	//void Update ()
 //   {
 //       Angle = transform.position - Target.transform.position;
 //       if(Angle!=Vector3.zero)
 //       {
 //           transform.Rotate(0, 0, 1);
 //       }
        
 //       transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
	//}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killyou1000 : MonoBehaviour
{
    public float AttackSpeed = 0.1f;

    public GameObject Bullet;
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        AttackSpeed -= Time.deltaTime;
        if(AttackSpeed<=0)
        {
            AttackSpeed = 0.1f;
            GameObject fire=Instantiate(Bullet, transform.position, Quaternion.Euler(transform.eulerAngles));
            fire.SetActive(true);
        }
	}
}

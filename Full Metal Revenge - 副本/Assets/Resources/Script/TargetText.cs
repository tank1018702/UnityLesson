using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetText : MonoBehaviour
{
    public int MaxHP;

    int currentHp;

	
	void Start ()
    {
        currentHp = MaxHP;
	}
	
	
	void Update ()
    {
		if(currentHp==0)
        {

        }
	}
}

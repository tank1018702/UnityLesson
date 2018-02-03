using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float BoomTime = 1.5f;
	// Use this for initialization
	void Start ()
    {
        GameObject.Destroy(gameObject,this.BoomTime);	
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissle : MonoBehaviour
{
    public float MoveSpeed = 1 ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(MoveSpeed * Time.deltaTime * Vector3.up);
    }
}

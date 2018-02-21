using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGAngleController : MonoBehaviour
{
    public bool fire;

    public GameObject AAG;

    public Vector3 position;
    

    private void Start()
    {
        fire = false;
        position = Vector3.zero;
    }

    private void Update()
    {
        if(position!=Vector3.zero)
        {
            fire = true;
            

            Vector3 Direction = (position - transform.position).normalized;

            transform.up = Direction;
        }
        else
        {
            fire = false;
        }
        AAG.GetComponent<DoubleAAG>().Canfire = fire;

    }

    public void SetDate(Vector3 pos)
    {
        position = pos;
    }







}

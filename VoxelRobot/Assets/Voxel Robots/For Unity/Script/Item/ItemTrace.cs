using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrace : MonoBehaviour
{
    public GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            target = other.gameObject;
        }
    }




}

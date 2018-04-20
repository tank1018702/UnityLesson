using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    //[SerializeField]
    //private ContorlType contorltype;

    //public float RotateSpeed;

    //[HideInInspector]
    //public bool BeSelected;
    //private bool IsInRotation;

    void Start()
    {
        //BeSelected = false;
        //IsInRotation = false;
        //RotateSpeed = 1f;
    }
    void Update()
    {
        //MapRotateUpdate();
    }
    //IEnumerator MapRotate(float angle)
    //{
    //    IsInRotation = true;
    //    while(angle!=0)
    //    {
    //        if(angle>0)
    //        {
    //            transform.Rotate(transform.up, Mathf.Min(RotateSpeed, angle));
    //            angle -= RotateSpeed;
    //        }
    //        else
    //        {
    //            transform.Rotate(transform.up, Mathf.Max(-RotateSpeed, angle));
    //            angle += RotateSpeed;
    //        }           
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }
    //    IsInRotation = false;
    //}

    //void MapRotateUpdate()
    //{
    //    if (BeSelected && !IsInRotation)
    //    {
    //        float angle = 0;
    //        if (Input.GetKeyDown(KeyCode.A))
    //        {
    //            angle = -90f;
    //        }
    //        else if (Input.GetKeyDown(KeyCode.D))
    //        {
    //            angle = 90f;
    //        }
    //        StartCoroutine(MapRotate(angle));
    //    }
    //}
}



public enum ContorlType
{
    PlayerControl,
    EnemyControl
}

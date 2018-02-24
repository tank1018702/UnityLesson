using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float TurretSpeed=10f;


    private void Update()
    {
        transform.Rotate(DirectionAngle().eulerAngles);
    }

    private void FixedUpdate()
    {
        //Vector2 mousepos = Input.mousePosition;

        //Vector2 objpos = Camera.main.WorldToScreenPoint(transform.position);

        //Vector2 direciton = (mousepos - objpos).normalized;
     

        ////float Angle = Vector3.Angle(transform.up,direciton);

            
        //Quaternion rotation = Quaternion.LookRotation(direciton);

        
        

        ////transform.rotation = Quaternion.Slerp(transform.rotation,rotation, 1.0f * Time.deltaTime*10);
        //transform.GetComponent<Transform>().rotation= Quaternion.Slerp(transform.rotation, rotation, 1.0f * Time.deltaTime * 10);
        //Debug.Log("1111");

        ////if(direciton.y>=-0.7f)
        ////{
        ////    transform.up = direciton;
        ////}



        //float angle = Vector3.Angle(transform.up, direciton);
        //if (angle != 0)
        //{
        //    //Vector3 normal = Vector3.Cross(transform.up, direciton);




        //    //angle *= Mathf.Sign(Vector3.Dot(normal, transform.up));

        //    transform.Rotate(0, 0, angle);

        //}



    }

    private Quaternion DirectionAngle()
    {
        
        Vector2 Direcition = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        float _DirectionAngle = Vector3.Angle(transform.up, Direcition);

        //float _DirectionAngle = Mathf.Atan2(Direcition.y, Direcition.x) * Mathf.Rad2Deg;

        float Dot = Vector3.Dot(Direcition, transform.right);
        if (Dot >= 0)
        {
            _DirectionAngle = (360 - _DirectionAngle);
        }
        return Quaternion.Euler(0, 0,_DirectionAngle /*Mathf.Lerp(0, _DirectionAngle, TurretSpeed*Time.deltaTime)*/);
    }








}

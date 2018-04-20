using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text2 : MonoBehaviour
{


    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            Vector3 Direction = (GetMousePositionInWorld() - transform.position).normalized;
            float angle;
            Vector3 axis;
            //if (Vector3.Dot(transform.forward, Direction) <= 0)
            //{
            //    angle = Vector3.Angle(transform.forward, Direction) + 180;
            //}
            //else
            //{
            //    angle = Vector3.Angle(transform.forward, Direction);

            //}
            //Debug.Log(angle);

            //transform.Rotate(Vector3.Cross(transform.forward, Direction), Mathf.Min(1, angle));


            
            var b = Quaternion.Euler(Direction.);
            transform.rotation= Quaternion.RotateTowards(transform.rotation, b, 1f)
           /* transform.rotation =*/ ;

              //Quaternion.ToAngleAxis(out angle, out axis);

            Debug.Log("========" + Vector3.Cross(transform.forward, Direction).y);
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.yellow);
        }




    }
    public Vector3 GetMousePositionInWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(Camera.main.transform.position, hit.point - Camera.main.transform.position, Color.blue);
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}

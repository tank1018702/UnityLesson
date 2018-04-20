using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{

    public Vector3 targetposition;
    public Vector2 Direction_xz;
    public Vector2 Driection_zy;

    void Start()
    {

    }


    void Update()
    {
        targetposition = GetMousePositionInWorld();


        Vector2 position_xz = new Vector2(targetposition.x, targetposition.z);
        Direction_xz = (position_xz - new Vector2(transform.position.x, transform.position.z)).normalized;
        Vector3 direction_1 = new Vector3(Direction_xz.x, 0, Direction_xz.y);
        //=======================================================================================================================
        Vector2 position_zy = new Vector2(targetposition.z, targetposition.y);
        Driection_zy = (position_zy - new Vector2(transform.position.z, transform.position.y)).normalized;
        Vector3 direction_2 = new Vector3(0, Driection_zy.y, Driection_zy.x);



        //Vector2 Direction_XZ = (new Vector2(targetposition.x, targetposition.z) - new Vector2(transform.position.x, transform.position.z)).normalized;
        //Vector2 Direction_ZY = (new Vector2(targetposition.z, targetposition.y) - new Vector2(transform.position.z, transform.position.y)).normalized;
        Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
        Debug.DrawRay(transform.GetChild(0).transform.position, transform.GetChild(0).transform.forward * 1000, Color.green);
        if (transform.tag == "leftright")
        {
            float dot = Vector3.Dot(transform.forward,direction_1);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            //Direction.y = transform.position.y;
            Debug.Log("xz:" + dot + "=====" + angle);
            transform.Rotate(Vector3.Cross(transform.forward, direction_1), Mathf.Min(1,angle));

        }
        if (transform.tag == "updown")
        {

            float dot = Vector3.Dot(transform.forward, direction_2);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            transform.Rotate(Vector3.Cross(transform.forward, Driection_zy), Mathf.Min(1, angle));
            Debug.Log("zy:" + dot + "=====" + angle);
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
    void LeftRight_Movement()
    {

    }

}

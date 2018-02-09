using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Debug.Log("x: " + x + "y:" + y);

        Vector3 m = new Vector3(x, y, 0);
        Vector3 mg = transform.InverseTransformPoint(m);
        //GetComponent<Rigidbody>().velocity = mg;
        transform.GetComponent<Rigidbody>().AddForce(m);


    }
}

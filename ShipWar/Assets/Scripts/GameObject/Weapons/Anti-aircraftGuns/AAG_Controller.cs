using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAG_Controller : MonoBehaviour
{

    public GameObject Radar;

    public Vector3 TargetPosition;

    public GameObject[] Slots = new GameObject[6];



    private void Start()
    {

    }


    private void Update()
    {
        List<GameObject> TargetList = Radar.GetComponent<Radar>().Targets;
        if (TargetList.Count != 0)
        {
            if (TargetList[0] != null)
            {
                TargetPosition = TargetList[0].transform.position;
            }
        }
        else
        {
            TargetPosition = Vector3.zero;
        }
        for (int i = 0; i < 6; i++)
        {
            Slots[i].GetComponent<AAGAngleController>().SetDate(TargetPosition);

        }


    }


}

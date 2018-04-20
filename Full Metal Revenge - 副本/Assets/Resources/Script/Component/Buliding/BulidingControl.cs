using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulidingControl : MonoBehaviour
{
    [HideInInspector]
    public GameObject CurrentBuilding = null;

    public GameObject UI_View;

    BuildingInfo _info;





    void Start()
    {
        _info = new BuildingInfo();

    }

    

    //public void SetData()
    //{
        
    //    if(CurrentBuilding)
    //    {
    //        var RobotScript = CurrentBuilding.GetComponent<Incubator>().Robot.GetComponent<RobotInfo>();
    //    }
        

    //}


}

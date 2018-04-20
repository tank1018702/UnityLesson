using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Incubator : MonoBehaviour
{
    public GameObject Robot;
    [HideInInspector]
    public GameObject UI_View;
    public int ResourceCost;
    public GameObject NextLevel;


    float BuildTime;
    Transform _bornPoint;
    BuildingInfo _info;
    Image _image;



    void Start()
    {
        _bornPoint = transform.GetChild(1);
        if (UI_View)
        {
            _image = UI_View.transform.GetChild(1).GetComponent<Image>();
        }
        if (Robot)
        {
            BuildTime = Robot.GetComponent<RobotInfo>().BuildingTime;
            Robot.GetComponent<RobotInfo>().Resources = ResourceCost;
            StartCoroutine(BuildingRobot());
        }
    }


    void Update()
    {

    }
    void SetData()
    {
        _info = new BuildingInfo
        {
            BuildTime = BuildTime
        };
    }

    private int enemyIndex = 0;
    private int playerIndex = 0;

    IEnumerator BuildingRobot()
    {
        while (true)
        {
            yield return GetSliderValue();
            GameObject go = Instantiate(Robot, _bornPoint.position, Quaternion.identity);
            if (tag == "PlayerControl")
            {
                playerIndex++;
                go.tag = "Player";
                go.name = this.transform.parent.name + "_Player_" + playerIndex.ToString();
            }
            else
            {
                enemyIndex++;
                go.tag = "Enemy";
                go.name = this.transform.parent.name + "_Enemy_" + enemyIndex.ToString();
            }
        }
    }
    IEnumerator GetSliderValue()
    {
        float temptime = 0f;
        while (temptime < BuildTime)
        {
            temptime += Time.deltaTime;
            _image.fillAmount = temptime / BuildTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}

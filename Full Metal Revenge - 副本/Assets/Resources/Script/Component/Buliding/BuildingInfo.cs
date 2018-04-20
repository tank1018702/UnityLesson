using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo
{
    public float BuildTime;
    //robot
    public string name;
    public int HP;
    public float MoveSpeed;
    public int Attack;

    public BuildingInfo()
    {
        BuildTime = -1;
        name = "";
        HP = -1;
        MoveSpeed = -1;
        Attack = -1;
    }



}

public enum BuildingType
{
    Melee,
    Remote,
    healer
}
public enum ControlType
{
    Player,
    Enemy
}

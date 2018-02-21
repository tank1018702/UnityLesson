using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ItemInfo
{
    //道具大致分为三类

    //主武器:火炮,鱼雷,导弹

    //副武器:战列舰副炮,近防机炮,

    //功能组件:引擎,雷达(?),火控系统,
    public string ItemName { get; set; }

    public string ItemDescribe { get; set; }

    public string PrefabName { get; set; }





    //主武器:火炮,鱼雷,导弹

    public WeaponType weapontype;

    


    

    

}
public enum WeaponType
{
    Artillery,
    Torpedo,
    Missile,

}

public enum Multiple
{
    Single,
    Double,
    Triple

}




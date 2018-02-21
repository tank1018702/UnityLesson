using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShip : MonoBehaviour
{
    public ItemInfo[] MainWeaponSlots=new ItemInfo[5];

    [ContextMenu("ApplyItems")]
    public void AddText()
    {
        Transform parent;
        for(int i=0;i<MainWeaponSlots.Length;i++)
        {
            ItemInfo info = new ItemInfo();
            info.ItemName = "测试炮塔:" + (i + 1) + "号";
            info.ItemDescribe = "就是写写表达存在感";
            info.PrefabName = "SingleTorret";
            MainWeaponSlots[i] = info;
        }
        for(int i = 0; i < MainWeaponSlots.Length; i++)
        {
            GameObject ItemTemp = Resources.Load<GameObject>("Prefabs/Weapons/"+MainWeaponSlots[i].PrefabName);


            parent = transform.Find("ShipItems/MainWeapons/Slot_" + (i + 1)).transform;
            if(parent!=null)
            {
                Debug.Log(parent);
            }
            
            GameObject _Item = Instantiate(ItemTemp,parent);
        }
       
    }

    private void Start()
    {
      
    }
    private void FixedUpdate()
    {
        
    }




}

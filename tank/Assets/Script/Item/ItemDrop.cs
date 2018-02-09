using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System.Text;
using System.IO;
using LitJson;


public class ItemData
{

    private static List<ItemInfo> DropItemList;

    public static List<ItemInfo> GetItemDropList()
    {
        if (DropItemList == null)
        {
            DropItemList = new List<ItemInfo>();
        }
        JsonData itemdata = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Resources/Config/ItemConfig.json", Encoding.GetEncoding("GB2312")));
        if(itemdata==null)
        {
            Debug.Log("itemdata is empty");
        }
        for(int i=0;i<itemdata.Count;i++)
        {
            ItemInfo info = new ItemInfo();
            info.ItemName = itemdata[i]["name"].ToString();
            info.ItemDescribe = itemdata[i]["describe"].ToString();
            info.ItemCount = (int)itemdata[i]["count"];
            info.ItemIconName = itemdata[i]["icon_name"].ToString();

            DropItemList.Add(info);
        }
        
        return DropItemList;
    }


}


public class ItemDropManager
{

    



    public static void ItemDrop(Vector3 position,Vector3 scale,Transform parenttransform=default(Transform))
    {    
        //SpriteAtlas iconatlas = Resources.Load<SpriteAtlas>("Atlas/ItemAtlas");
        //if(iconatlas==null)
        //{
        //    Debug.Log("iconatlas is null");
        //    return;
        //}
        //int r = Random.Range(0, 6);
        //Sprite icon = iconatlas.GetSprite("Bonus_" + r.ToString());
        //if(icon==null)
        //{
        //    return;
        //}
        GameObject tempitem = Resources.Load<GameObject>("Prefab/Item/DropItem");
        GameObject item = GameObject.Instantiate(tempitem);
        if (parenttransform != null)
        {
            item.transform.SetParent(parenttransform);
        }
        item.transform.position = position;
        item.transform.localScale = scale;
        
        Item itemscript = item.GetComponent<Item>();
        if (itemscript != null)
        {
            itemscript.SetData(GetItemInfo());

            
           
        }

    }
    private static ItemInfo GetItemInfo()
    {
        //ItemInfo info = new ItemInfo();
        //info.ItemCount = Random.Range(1, 10);
        //info.ItemDescribe = "测试道具,编号" + Random.Range(1, 10) + ",后面的都是废话,我就是想把描述信息写长一点好测试一下组件工作是否正常";
        //info.ItemName = "测试" + Random.Range(1, 101);
        //info.ItemIconName = "Bonus_" + Random.Range(0, 6);
        //return info;
        return ItemData.GetItemDropList()[Random.Range(0, 6)];
        

    }





}

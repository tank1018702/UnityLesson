using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ItemBagPanel : MonoBehaviour
{
    public static List<ItemInfo> ItemInfoList;

    public Transform ItemParent;

    public GameObject detial;


    [ContextMenu("AddItemText")]
    public void AddText()
    {
        for (int i = 0; i < 5; i++)
        {
            ItemInfo info = new ItemInfo();
            info.ItemName = "测试道具";
            info.ItemDescribe = "这是一个测试道具,描述个蛋" + "(" + i + ")";
            info.ItemCount = i + 1;
            info.ItemIconName = "Bonus_" + Random.Range(0, 6);
            if (ItemInfoList == null)
            {
                ItemInfoList = new List<ItemInfo>();
            }
            ItemInfoList.Add(info);
        }
        ShowBagContent();
    }
    public void Update()
    {
        
    }

    public void PickUpItem()
    {

        ShowBagContent();
    }

    private void ShowBagContent()
    {
        GameObject itemtemp = Resources.Load<GameObject>("Prefab/Item/DropItem");
        for (int i = 0; i < ItemInfoList.Count; i++)
        {

            GameObject _item = Instantiate(itemtemp, ItemParent);
            Item itemscript = _item.GetComponent<Item>();

            if (itemscript != null)
            {
                itemscript.SetData(ItemInfoList[i]);

                itemscript.itembag = gameObject;
            }
        }
    }

}



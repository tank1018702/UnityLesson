using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : Singleton<ItemDropManager>
{
    private ItemDropManager()
    {

    }
    public  void ItemDrop(Vector3 dropPos,Transform dorper=null)
    {
        int r = Random.Range(0, 5);
        GameObject DropItem = ResuorceManager.Instance.GetPrefabByName("item");

        Sprite ItemIcon = ResuorceManager.Instance.GetImageSpriteByName(r.ToString());

        if(DropItem==null)
        {
            return;
        }
        Item sprite = DropItem.GetComponent<Item>();
        sprite.SetData(ItemIcon, dropPos);
    }
	
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ItemDropManager
{
    public static void ItemDrop(Vector3 position,Vector3 scale,Transform parenttransform=default(Transform))
    {    
        SpriteAtlas iconatlas = Resources.Load<SpriteAtlas>("Atlas/ItemAtlas");
        if(iconatlas==null)
        {
            Debug.Log("iconatlas is null");
            return;
        }
        int r = Random.Range(0, 6);
        Sprite icon = iconatlas.GetSprite("Bonus_" + r.ToString());
        if(icon==null)
        {
            return;
        }
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
            itemscript.SetData(icon);
        }
    }






}

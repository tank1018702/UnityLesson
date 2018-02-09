using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemicon;
    public ItemInfo iteminfo;
    public GameObject itembag;
  

    bool open_detailinfo=false;


    //private void Start()
    //{
    //    bagscript = itembag.GetComponent<ItemBagPanel>();

    //    bagscript.detial.SetActive(open_detailinfo);
    //}


    public void SetData(ItemInfo info)
    {
        iteminfo = info;
        SpriteAtlas itematlas = Resources.Load<SpriteAtlas>("Atlas/ItemAtlas");
        itemicon.sprite = itematlas.GetSprite(info.ItemIconName);



        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemInfo info = eventData.pointerEnter.GetComponent<Item>().iteminfo;
        ItemBagPanel bagscript = itembag.GetComponent<ItemBagPanel>();
        ItemDetail detailscript = bagscript.detial.GetComponent<ItemDetail>();
        detailscript.itemname.GetComponent<Text>().text = info.ItemName;
        detailscript.itemdescribe.GetComponent<Text>().text = info.ItemDescribe;
        detailscript.itemcount.GetComponent<Text>().text = "数量:"+info.ItemCount.ToString();






        open_detailinfo = !open_detailinfo;
        
        bagscript.detial.SetActive(open_detailinfo);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        open_detailinfo = !open_detailinfo;

        ItemBagPanel bagscript = itembag.GetComponent<ItemBagPanel>();

        bagscript.detial.SetActive(open_detailinfo);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag=="player")
        {
            
            if (ItemBagPanel.ItemInfoList==null)
            {
                ItemBagPanel.ItemInfoList = new List<ItemInfo>();
            }
            itembag = collision.gameObject.GetComponent<Player>().itembag;
            ItemBagPanel.ItemInfoList.Add(iteminfo);
            ItemBagPanel bagscript = itembag.GetComponent<ItemBagPanel>();
            bagscript.PickUpItem();
            Destroy(gameObject);
        }
    }

}




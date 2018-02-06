using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Image itemicon;
    public  void SetData(Sprite icon)
    {
        
        itemicon.sprite = icon;
    }	
}

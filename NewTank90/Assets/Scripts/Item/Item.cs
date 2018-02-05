using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Image Icon;

	public void SetData(Sprite icon,Vector3 pos)
    {
        if(icon==null)
        {
            return;
        }
        Icon.sprite = icon;
        transform.position = pos;
        transform.localScale = Vector3.one;

    }
}

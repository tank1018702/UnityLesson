using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{


    void Start()
    {
        var text1 = DataTable.Get<ItemData>(10001);
        Debug.Log(text1.id+text1.name+text1.icon+text1.atlas);
        Item item = new Item();
        item.id = 1;
        item.name = "111";
        item.icon = "222";
        item.atlas = "333";
        DataTable.Set(item);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

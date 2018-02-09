using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagControl : MonoBehaviour
{
    public GameObject itembag;

    bool bag_is_open = false;

    bool game_active = true;

    void Start()
    {
        itembag.SetActive(bag_is_open);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //ItemBagPanel bagscript = itembag.GetComponent<ItemBagPanel>();
            //bagscript.ShowBagContent();
            bag_is_open = !bag_is_open;
            itembag.SetActive(bag_is_open);
            
            if (game_active == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            game_active = !game_active;
        }

    }
}

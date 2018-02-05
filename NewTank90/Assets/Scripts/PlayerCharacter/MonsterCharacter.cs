using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MonsterCharacter:Character
{
    public MonsterCharacter(GameObject obj)
    {
        this.CharacObj = obj;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        ItemDropManager.Instance.ItemDrop(transform.position, transform);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MonsterManager:Singleton<MonsterManager>
{
    public List<MonsterCharacter> MonsterList;

    public void AddMonsterTolist( MonsterCharacter mon)
    {
        if (MonsterList != null || MonsterList.Contains(mon)) return;
        MonsterList.Add(mon);
    }

    public void RemoveMonster(MonsterCharacter mon)
    {
        if (MonsterList.Contains(mon)) MonsterList.Remove(mon);
    }

    public MonsterCharacter FindMon(MonsterCharacter mon)
    {
        if (!MonsterList.Contains(mon)) return null;
        return MonsterList.Find(x => x.Equals(mon));
    }
}
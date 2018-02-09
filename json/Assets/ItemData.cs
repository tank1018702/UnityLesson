using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;

public class ItemData : MonoBehaviour
{
    private JsonData itemdata;
    private List<Item> deatbase = new List<Item>();

	void Start ()
    {
        itemdata = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/itembase/items.json", Encoding.GetEncoding("GB2312")));
        ConstructItemDatabase();
        Debug.Log(deatbase[0].Desp);
        Debug.Log(FetchItemBy(0).Name + FetchItemBy(0).Value);
	}
	

	void Update ()
    {
		
	}

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemdata.Count; i++)
        {
            deatbase.Add(new Item((int)itemdata[i]["id"], itemdata[i]["name"].ToString(),
                (int)itemdata[i]["value"], itemdata[i]["describe"].ToString(), itemdata[i]["madeby"].ToString()));
        }
    }

    public Item FetchItemBy(int _id)
    {
        for (int i = 0; i < deatbase.Count; i++)
        {
            if (_id == deatbase[i].ID)
            {
                return deatbase[i];
            }
        }
        return null;
    }
}

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
    public string Desp { get; set; }
    public string MadeBy { get; set; }

    public Item(int _id, string _name, int _value, string _desp, string _madeby)
    {
        this.ID = _id;
        this.Name = _name;
        this.Value = _value;
        this.Desp = _desp;
        this.MadeBy = _madeby;

    }
}

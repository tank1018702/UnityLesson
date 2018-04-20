using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.IO;
using System.Text;
// <id, <key, value>>
//解析文件出来的中间结构
using CSVTable = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;
// <table type，<id, table object>>
using CacheTable = System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.Dictionary<int, object>>;

public class DataTable
{

    static CacheTable cache = new CacheTable();
                                                             //反射可以根据泛型获得的类的实例化对象来获取该对象的类的方法,属性,字段等..
    public static T Get<T>(int id)
    {
        //反射获取泛型函数的返回值类型
        var type = typeof(T);
        //cache的键类型为System.Type,如果cache的键与反射获取的T的Type并不相同,则是一张新的配置表
        if (!cache.ContainsKey(type))
        {
            //调用读取函数,读取函数中将CSV文件进行解析.
            //CSV中的数据本没有指定类型,使用者在解析的时候将数据类型作为转换类型调用泛型函数(Get(T)),解析出来的数据即为调用时的泛型;
            cache[type] = Load<T>("DataTable/" + type.Name);
        }
        //返回解析后的数据,Load<T>的返回值是Dictionary;
        T data = (T)cache[type][id];
        return data;
    }
    //读取函数,参数为CSV文件路径名
    private static Dictionary<int, object> Load<T>(string path)
    {
        //NEW一个<int, object>的字典?
        Dictionary<int, object> datas = new Dictionary<int, object>();
        //根据路径名读取CSV文件
        var textAsset = Resources.Load<TextAsset>(path);
        //对CSV文件进行编码,此时读取的CSV文件内的数据全为String类型
        //Decode函数的返回值为CSVTable类型,CSVTable的结构为Dirtionary<string,Dirtionary<String,String>>
        var table = Decode(textAsset.text);
        //反射获取泛型的所有字段生成一个数组
        FieldInfo[] fields = typeof(T).GetFields();

        foreach (var id in table.Keys)
        {
            var row = table[id];
            //相当于new了一个T类型的对象
            var obj = Activator.CreateInstance<T>();

            foreach (FieldInfo fi in fields)
            {
                fi.SetValue(obj, Convert.ChangeType(row[fi.Name], fi.FieldType));
            }
            datas[Convert.ToInt32(id)] = obj;
        }

        return datas;
    }
    /// <summary>
    /// 解析函数
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static CSVTable Decode(string content) {

        CSVTable result = new CSVTable();
        
        string[] row = content.Replace("\r", "").Split(new char[] { '\n' });
        string[] columnHeads = row[0].Split(new char[] { ',' });


        for (int i = 1; i < row.Length; i++) {
            string[] line = row[i].Split(new char[] { ',' });
            var id = line[0];
            if (String.IsNullOrEmpty(id)) break;

            result[id] = new Dictionary<string, string>();
            for(int j = 0; j < line.Length; j++) {
                result[id][columnHeads[j]] = line[j];
            }
        }
        return result;
    }
    public static void Set<T>(T Data)
    {
        

        string data_header = "";
        string data_content="";
        var datatype = typeof(T);
        FieldInfo[] fields = datatype.GetFields(); 
        for(int i=0;i<fields.Length;i++)
        {
            data_header += fields[i].Name;
            data_content += fields[i].GetValue(Data);
        }
        //Encode(data_header+ data_content);

        File.WriteAllText("Assets/Resources/DataTable/" + datatype.Name,data_header+data_content,Encoding.UTF8);
        Debug.Log(data_header + data_content);

    }
    private static void Save<T>(string path)
    {

    }

    private static void Encode(string [] content)
    {


    }
}

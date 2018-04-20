using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericClass<T>
{
    T data;
    public GenericClass(T newData)
    {


        System.Type t = typeof(T);
        Debug.Log(t.BaseType);

        data = newData;
        Debug.Log(data);
    }
}

public class GenericFunction
{
    public static void Func<T>(T param)
    {
        Debug.Log(param);
    }
}

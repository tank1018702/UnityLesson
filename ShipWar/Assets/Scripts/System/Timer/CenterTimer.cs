using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTimer : MonoBehaviour
{
    static List<Timer> Timers = new List<Timer>();


    public  static void AddTimer(Timer timer)
    {
        Timers.Add(timer);
    }

    public static void RemoveTimer(Timer timer)
    {
        Timers.Remove(timer);
    }

    private void Update()
    {
        for (int i = 0; i < Timers.Count; i++)
        {
            Timers[i].Update();
        }         
    }

}

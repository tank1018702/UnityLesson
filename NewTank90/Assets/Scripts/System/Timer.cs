using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Timer : SingletonBehaviour<Timer>
{
    public Dictionary<GameObject, float> GamObjectTimer;

    public List<TimeEvent> TimerList;

    private List<TimeEvent>.Enumerator evetList;
    private List<TimeEvent> CompleteList;

    private Timer()
    {
        this.GamObjectTimer = new Dictionary<GameObject, float>();
        this.TimerList = new List<TimeEvent>();
        this.CompleteList = new List<TimeEvent>();
    }

    private void Start()
    {
       
    }


    private void FixedUpdate()
    {
        // cycle
        evetList = this.TimerList.GetEnumerator();
        while (evetList.MoveNext())
        {
            if (evetList.Current != null)
            {
                evetList.Current.SetBeginTime();
            }

            //if (evetList.Current.IsDelete)
            //{
            //    this.CompleteList.Add(evetList.Current);
            //}
        }
        //// delte

        //while (this.CompleteList.GetEnumerator().MoveNext())
        //{
        //    if (this.CompleteList.GetEnumerator().Current != null)
        //    {
        //        this.RemoveTimer(this.CompleteList.GetEnumerator().Current);
        //    }
        //}

        //this.CompleteList.Clear();
    }

    public void AddTimer(TimeEvent evet,float time,Action CallBack)
    {
        if (!this.TimerList.Contains(evet))
        {
            evet.OnCompleteEvent = CallBack;
            evet.SetData(evet.gameObject, time);
            this.TimerList.Add(evet);
        }
        else
        {
            DestroyImmediate(evet);
        }
    }

    public void RemoveTimer(TimeEvent evet)
    {
        if (evet != null)
        {
            this.TimerList.Remove(evet);
        }
    }
    
}

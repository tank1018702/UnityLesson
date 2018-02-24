using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer
{
    //开始时间
    public float StartTime { get; private set; }
    //持续时间
    public float Duration { get; private set; }
    //结束时间
    public float EndTime { get; private set; }
    //当前时间
    public float CurTime { get; private set; }

    //运行标识
    public bool IsStart { get; private set; }


    public Action OnStart { get; set; }
    public Action OnEnd { get; set; }
    public Action OnUpdate { get; set; }

    public Timer(float duration)
    {
        Duration = duration;
    }

    public void Start()
    {
        IsStart = true;
        StartTime = Time.time;
        CurTime = StartTime;
        EndTime = StartTime + Duration;
        CenterTimer.AddTimer(this);
        if (OnStart != null)
        {
            OnStart();
        }
         

    }

    public void Update()
    {
        if(!IsStart)
        {
            return;
        }
        CurTime += 0.02f;
        if(CurTime>EndTime)
        {
            End();
        }
        else if(OnUpdate!=null)
        {
            OnUpdate();
        }
    }

    void End()
    {
        IsStart = false;
        if (OnEnd != null) OnEnd();
        CenterTimer.RemoveTimer(this);
    }


}

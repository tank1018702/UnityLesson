using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeEvent : MonoBehaviour
{
    private float beginTime = 0;
    public bool IsDelete = false;
    public float BeginTime
    {
        get
        {
            return this.beginTime;
        }
        set
        {
            this.beginTime = value;
            if (this.beginTime >= this.TillTime && this.OnCompleteEvent != null)
            {
                //倒计时到了
                this.OnCompleteEvent();
                this.OnCompleteEvent = null;
                this.IsDelete = true;
            }
        }
    }
    public float TillTime = 0;


    public GameObject OBJ;
    public Action OnCompleteEvent;


    public void SetData(GameObject oBJ, float tillTime)
    {
        this.OBJ = oBJ;
        this.TillTime = tillTime;

        this.Invoke("InovekeCallBack",tillTime);
    }
    public void SetBeginTime()
    {
        this.BeginTime += Time.deltaTime;
    }

    public void InovekeCallBack()
    {
        if (this.OnCompleteEvent != null)
        {
            this.OnCompleteEvent();
            this.OnCompleteEvent = null;
        }
    }
}
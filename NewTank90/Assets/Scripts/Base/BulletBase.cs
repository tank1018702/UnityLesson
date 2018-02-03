using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class BulletBase : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public Image BulletImage;
    public float MoveSpeed = 1.0f;

    public Vector3 MoveMent;
    public bool IsDoMove = false;
    /// <summary>
    /// 子弹发送者
    /// </summary>
    public Transform Sender;
    public Vector3 MoveDirection = Vector3.up;

    public Action<Collision2D> CollisionEnterCallBack;
    public Action<Collision2D> CollisionExitCallBack;
    public Action<Collision2D> CollisionStayCallBack;

    public Timer TimerManager {
        get {
            return Timer.Instance;
        }
    }

    public ResuorceManager ResuorceManagers { get {
            return ResuorceManager.Instance;
        } }
    public Transform UICanvas { get {
            return SelfGameManager.Instance.UICanvas.transform;
        } }

    public virtual void Start()
    {
        
    }

    public virtual void SetSenderAndDirection(Transform sender,Vector3 direction)
    {
        this.Sender = sender;
        this.MoveDirection = direction;
    }


    public virtual void FixedUpdate()
    {
       // this.DoMove();
    }

    public virtual void DoMove()
    {
        //朝着自己的  
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollisionEnterCallBack != null)
        {
            CollisionEnterCallBack(collision);
        }
    }

    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (CollisionStayCallBack != null)
        {
            CollisionStayCallBack(collision);
        }
    }

    public virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (CollisionExitCallBack != null)
        {
            CollisionExitCallBack(collision);
        }
    }

    


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Character :MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public Vector3 MoveMent;

    [Range(0,9)]
    public float MoveSpeed = 5.0f;

    [HideInInspector]
    public float CurrentMoveSpeed = 5.0f;
    /// <summary>
    /// 角色死亡的回掉
    /// </summary>
    public Action OnDieCallBack;
    /// <remarks>角色唯一ID</remarks>
    public int uid;
    public GameObject CharacObj;

    public Image TankImage;
    
    public BulletBase SelfBullet;

    public Transform BulletUpSendPos;
    public Transform BulletLeftSendPos;
    public Transform BulletDwonSendPos;
    public Transform BulletRightSendPos;

    public string CharacterName = string.Empty;


    public void SetCharacterName(string charaterName)
    {
        this.CharacterName = charaterName;
    }
    public ResuorceManager ResuorceManagers {
        get {
            return ResuorceManager.Instance;
        }
    }

    public virtual void Awake()
    {
        this.CurrentMoveSpeed = this.MoveSpeed;
    }

    public virtual void DoMove(){
    }

    public virtual void OnEquip() {
    }

    public virtual void OnDisable()
    {
        if (OnDieCallBack != null)
        {
            OnDieCallBack();
            OnDieCallBack = null;
        }
    }


    public virtual void Attack()
    {
        if (this.SelfBullet as NormalBullet)
        {
            //Debug.LogError("this is normal bullet");
        }
        else
        {

        }
    }
    
}

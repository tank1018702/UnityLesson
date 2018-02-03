using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class PlayerCharacter : Character
{
    public int Dirction = 0;
    private int CurrentDirction = 0;
    private Vector3 BulletMoveDirection = Vector3.up;
    private Transform BulletSendPos ;

    public override void Awake() {
        base.Awake();
        Rigidbody = this.GetComponent<Rigidbody2D>();
        Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.BulletSendPos = this.BulletUpSendPos;
    }

    /// <summary>
    /// 为什么 要在这里 做更新位置呢？
    /// </summary>
    public  void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = 0;
        if (h != 0)
        {
            v = 0;
        }
        else
        {
            h = 0;
            v = Input.GetAxis("Vertical");
        }


        if (h < 0)
        {
            this.SetPlayerImage(180);
            this.BulletMoveDirection = Vector3.left;
            this.BulletSendPos = this.BulletLeftSendPos;
        }
        else if (h > 0)
        {
            this.SetPlayerImage(360);
            this.BulletMoveDirection = Vector3.right;
            this.BulletSendPos = this.BulletRightSendPos;
        }
        else if (v > 0)
        {
            this.SetPlayerImage(90);
            this.BulletMoveDirection = Vector3.up;
            this.BulletSendPos = this.BulletUpSendPos;
        }
        else if (v < 0)
        {
            this.SetPlayerImage(270);
            this.BulletMoveDirection = Vector3.down;
            this.BulletSendPos = this.BulletDwonSendPos;
        }
        else
        {

        }

        this.MoveMent = h * Vector3.right + v * Vector3.up;
        this.MoveMent *= this.CurrentMoveSpeed;

        this.DoMove();
    }

    public void SetPlayerImage(int direction)
    {
        switch (direction)
        {
            case 90: //up
                this.TankImage.sprite = this.ResuorceManagers.GetImageSpriteByName(this.CharacterName + "U");
                break;
            case 180: //left
                this.TankImage.sprite = this.ResuorceManagers.GetImageSpriteByName(this.CharacterName + "L");
                break;
            case 270: //down
                this.TankImage.sprite = this.ResuorceManagers.GetImageSpriteByName(this.CharacterName + "D");
                break;
            case 360: //right
                this.TankImage.sprite = this.ResuorceManagers.GetImageSpriteByName(this.CharacterName + "R");
                break;
            default :
                break;
        }
    }


    public void Update()
    {
        //this.Dirction = (int)(Mathf.Acos(Vector3.Dot(Vector3.right, this.transform.up)) * Mathf.Rad2Deg);
        //if (this.CurrentDirction != this.Dirction)
        //{
        //    set player image
        //    Debug.LogError("this.Dirction " + this.Dirction);
        //    this.SetPlayerImage(this.Dirction);
        //    if (this.Dirction > 360)
        //    {
        //        this.Dirction = 90;
        //    }
        //    this.CurrentDirction = this.Dirction;

        //}
        

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Joystick1Button2) || Input.GetKeyUp(KeyCode.JoystickButton2))
        {
            this.Attack();
        }
    }
    



    public override void Attack()
    {
        BulletBase bulletebase = ResuorceManager.Instance.GetElseCreateBullet("Bullet");//,this.transform).GetComponent<BulletBase>();
        this.SelfBullet = bulletebase;
        bulletebase.SetSenderAndDirection(this.transform,this.BulletMoveDirection);
        bulletebase.gameObject.SetActive(true);
        bulletebase.gameObject.transform.parent = bulletebase.UICanvas;
        bulletebase.transform.position = this.BulletSendPos.position;
        bulletebase.transform.localScale =  (1.5f * Vector3.one);
        bulletebase.MoveSpeed = (this.CurrentMoveSpeed + 1);

        bulletebase.IsDoMove = true;

        base.Attack();
    }
    

    public override void DoMove() {
        base.DoMove();
        this.Rigidbody.velocity = this.MoveMent;
    }
    
}

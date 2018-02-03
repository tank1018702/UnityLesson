using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer Tank;
    public float MoveSpeed = 1;
    public List<Sprite> SpriteList;
    public GameObject EnemyMissile;

    
    private float TillTime = 1.0f;
    private int tankDirection = 0;
    private float AttackTillTime = 0.5f;

    /// <summary>
    ///  1 right 2 up  3 left  4 down
    /// </summary>
    private int TankDirection {
         get {
            tankDirection = Random.Range(1, 5);
            return tankDirection; }
    }

    private void Awake()
    {
        this.Tank = this.GetComponent<SpriteRenderer>();
        this.Tank.sprite = this.SpriteList[1];
        this.v = 1;
    }

    private float h = 0;
    private float v = 0;
    private void Update()
    {
        TillTime -= Time.deltaTime;
        
        
        if (TillTime <= 0 && this.SpriteList != null)
        {
            //1 right 2 up  3 left  4 down
            //
            this.TillTime = 1.0f;
            if (TankDirection == 1)
            {
                this.h = 1;
                this.v = 0;
                Tank.transform.rotation = Quaternion.Euler(new Vector3(0,0,-90));

              //  this.Tank.sprite = this.SpriteList[0];
            }
            else if (TankDirection == 2)
            {
                this.h = 0;
                this.v = 1;
                // this.Tank.sprite = this.SpriteList[1];
                Tank.transform.rotation = Quaternion.Euler(new Vector3(0, 0,0));
            }
            else if (TankDirection == 3)
            {
                this.h = -1;
                this.v = 0;
                //this.Tank.sprite = this.SpriteList[2];
                Tank.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            else if (TankDirection == 4)
            {
                this.h = 0;
                this.v = -1;
                //this.Tank.sprite = this.SpriteList[3];
                Tank.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
        }

        transform.Translate( this.MoveSpeed * Time.deltaTime * new Vector3(h, v, 0),Space.World);
        this.Attack();
    }


    private void Attack()
    {
        AttackTillTime -= Time.deltaTime;
        if(AttackTillTime <= 0)// 
        {
            AttackTillTime = 0.5f;

            GameObject missil = GameObject.Instantiate(this.EnemyMissile,transform.position,Quaternion.Euler(transform.eulerAngles));
            missil.SetActive(true);
        }
    }
}

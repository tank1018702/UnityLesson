using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Sprite> SpriteList;

    public float MoveSpeed = 5;

    public float AttackSpeed = 0.5f;

    public GameObject Bullet;

    public SpriteRenderer tank;

    public AudioClip FireAudio;

    int h = 0;
    int v = 1;

    public float PlayerDetectionDistance = 10;
    public float WallDetectionDistance = 2;



    private void ControlMethod()
    {
        RaycastHit2D FoundPlayer = Physics2D.Raycast(transform.position, transform.up, PlayerDetectionDistance, 1 << LayerMask.NameToLayer("player"));
        RaycastHit2D FoundWall = Physics2D.Raycast(transform.position, transform.up, WallDetectionDistance, 1 << LayerMask.NameToLayer("wall"));
        
        if(FoundPlayer)
        {         
            Attack();
        }
        if(FoundWall)
        {     
            DirectionChange();
            
        }
        //gameObject.GetComponent<Rigidbody2D>().velocity =new Vector2(h,v) * MoveSpeed;
        transform.Translate(new Vector3(h,v,0)*Time.deltaTime*MoveSpeed);
            

    }
    private void Attack()
    {
        AttackSpeed -= Time.deltaTime;
        if(AttackSpeed<=0)
        {
            AttackSpeed = 0.5f;
            CreateBullet();
        }
    }
    private void CreateBullet()
    {
        AudioSource.PlayClipAtPoint(FireAudio, transform.position);
        GameObject go = GameObject.Instantiate(Bullet, transform.position, Quaternion.Euler(transform.eulerAngles));
        go.SetActive(true);
    }
    private void DirectionChange()
    {
        int r = Random.Range(1, 3);
        if(r==1)
        {
            transform.Rotate(0, 0, 90);
        }
        else
        {
            transform.Rotate(0, 0, -90);
        }

        //if (h ==0&&v==1)
        //{
        //    if(r==1)
        //    {
        //        //h = -1;
        //        //v = 0;
        //        transform.Rotate(0, 0, 90);
        //    }
        //    else
        //    {
        //        //h = 1;
        //        //v = 0;
        //        transform.Rotate(0, 0, -90);
        //    }         
        //    return;
        //}
        //if(h==-1&&v==0)
        //{
        //    if(r==1)
        //    {
        //        //h = 0;
        //        //v = -1;
        //        transform.Rotate(0, 0, 90);
        //    }
        //    else
        //    {
        //        //h = 0;
        //        //v = 1;
        //        transform.Rotate(0, 0, -90);
        //    }
           
        //    return;
        //}
        //if(h==0&&v==-1)
        //{
        //    if(r==1)
        //    {
        //        //h = 1;
        //        //v = 0;
        //        transform.Rotate(0, 0, 90);
        //    }
        //    else
        //    {
        //        //h = -1;
        //        //v = 0;
        //        transform.Rotate(0, 0, -90);
        //    }          
        //    return;
        //}
        //if(h==1&&v==0)
        //{
        //    if(r==1)
        //    {
        //        //h = 0;
        //        //v = 1;
        //        transform.Rotate(0, 0, 90);
        //    }
        //    else
        //    {
        //        //h = 0;
        //        //v = -1;
        //        transform.Rotate(0, 0, -90);
        //    }        
        //    return;
        //}

    }

    void Start ()
    {
		
	}
	
	
	void Update ()
    {
        
    }
    private void FixedUpdate()
    {
        ControlMethod();
    }
}

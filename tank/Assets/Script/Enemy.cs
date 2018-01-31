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

    public float PlayerDetectionDistance = 10;
    public float WallDetectionDistance = 2;



    private void ControlMethod()
    {
        RaycastHit2D FoundPlayer = Physics2D.Raycast(transform.position, transform.up, PlayerDetectionDistance, 1 << LayerMask.NameToLayer("player"));
        RaycastHit2D FoundWall = Physics2D.Raycast(transform.position, transform.up, WallDetectionDistance, 1 << LayerMask.NameToLayer("wall"));
        
        if(FoundPlayer)
        {
            Debug.LogError("2222");
            Attack();
        }
        if(FoundWall)
        {
            Debug.LogError("2222");
            DirectionChange();
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * MoveSpeed;

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
        GameObject go = GameObject.Instantiate(Bullet, transform.position, Quaternion.Euler(transform.eulerAngles));
        go.SetActive(true);
    }
    private void DirectionChange()
    {
        Debug.LogError("11111");
        int r = Random.Range(1, 3);
        if(r==1)
        {
            transform.Rotate(0, 0, 90);
        }
        transform.Rotate(0, 0, -90);
    }

    void Start ()
    {
		
	}
	
	
	void Update ()
    {
        ControlMethod();
    }
    private void FixedUpdate()
    {
        
    }
}

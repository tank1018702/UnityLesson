using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float MoveSpeed = 1;
    public SpriteRenderer Tank;
    public List<Sprite> SpriteList;

    [HideInInspector]
    public float H;
    public float V;
    /// <summary>
    /// weapon
    /// </summary>
    public GameObject MissileOBJ;

    public virtual void Awake()
    {
        if (this.Tank == null)
        {
            this.Tank = GetComponent<SpriteRenderer>();
        }
    }
    public virtual void Start()
    {
    }
    public virtual void OnEable()
    {

    }

    public virtual void OnDisable()
    {
    }

    public virtual void OnDestory()
    {

    }
    public virtual void Die()
    {

    }

    public virtual void Move()
    {
        transform.Translate(this.MoveSpeed * Time.deltaTime * new Vector3(this.H, this.V, 0), Space.World);
    }

    public virtual void Attack()
    {
        //this.MissileOBJ = GameObject.Instantiate<>();
    }


    

}

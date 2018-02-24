using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAAG : MonoBehaviour
{
    public bool Canfire;

    //public GameObject bullet;

    public GameObject muzzle_1;
    public GameObject muzzle_2;

    public float offset=20;
    public float FiringInterval;
    public int Damage;
    private bool OnAttack;

    Timer timer;



    void Start()
    {
        OnAttack = true;
        Canfire = true;
        Damage = 5;
        FiringInterval = 0.1f;

    }


    void Update()
    {

      
        



    }
    private void FixedUpdate()
    {
        
        if(Canfire==true)
        {
            Fire();
            if(OnAttack==true)
            {
                Attack();
            }
            
        }
        
       

    }
    private void Attack()
    {
        RaycastHit2D FoundTarget = Physics2D.Raycast(transform.position,transform.up,20f, 1 << LayerMask.NameToLayer("Missile"));
        if(FoundTarget)
        {
            FoundTarget.transform.GetComponent<Missile>().HP -= Damage;  
        }
        timer = new Timer(FiringInterval);
        timer.OnStart = () =>
        {
            OnAttack = false;
        };
        timer.OnEnd = () =>
        {
            OnAttack = true;
        };
        timer.Start();
    }

    private void Fire()
    {
        Vector3 OffsetAngle = new Vector3(0, 0, Random.Range(-offset, offset));
        GameObject BulletGo_1 = AAGBulletPool.BulletPool.GetAAGBullets(muzzle_1.transform.position,transform.rotation);
        BulletGo_1.SetActive(true);
        GameObject BulletGo_2 = AAGBulletPool.BulletPool.GetAAGBullets(muzzle_2.transform.position,transform.rotation);
        BulletGo_2.SetActive(true);
    }
}

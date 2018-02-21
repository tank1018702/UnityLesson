using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAAG : MonoBehaviour
{
    public bool Canfire;

    public GameObject bullet;

    public GameObject muzzle_1;
    public GameObject muzzle_2;

    public float offset;
    public float FiringInterval;

    private Timer timer;



    void Start()
    {
        Canfire = false;
        FiringInterval = 0.1f;
    }


    void Update()
    {

        
       
    }
    private void FixedUpdate()
    {

        if (Canfire == true)
        {
            Fire();
        }

    }

    private void Fire()
    {
        Vector3 OffsetAngle = new Vector3(0, 0, Random.Range(-offset, offset));
        GameObject BulletGo_1 = Instantiate(bullet, muzzle_1.transform.position, Quaternion.Euler(transform.eulerAngles + OffsetAngle));
        BulletGo_1.SetActive(true);
        GameObject BulletGo_2 = Instantiate(bullet, muzzle_2.transform.position, Quaternion.Euler(transform.eulerAngles + OffsetAngle));
        BulletGo_2.SetActive(true);
    }

  

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelFire : MonoBehaviour
{
    public GameObject Bullet;
    /// <summary>
    /// 炮口
    /// </summary>
    public GameObject BarrelMuzzle;
    /// <summary>
    /// 子弹角度偏移
    /// </summary>
    public float Offset;
    /// <summary>
    /// 射击间隔时间
    /// </summary>
    public float FiringInterval;

    private bool Canfire = true;
    Timer timer;



    private void Update()
    {
        if (Input.GetMouseButton(0) && Canfire)
        {
            OnFire();
        }



    }

   
   
    void OnFire()
    {
        Vector3 OffsetAngle = new Vector3(0, 0, Random.Range(-Offset, Offset));
        GameObject BulletGo = Instantiate(Bullet, BarrelMuzzle.transform.position, Quaternion.Euler(transform.eulerAngles + OffsetAngle));
        BulletGo.SetActive(true);

        timer = new Timer(FiringInterval);
        timer.OnStart = () => { Canfire = false; };
        timer.OnEnd = () => { Canfire = true; };
        timer.Start();
    }





}

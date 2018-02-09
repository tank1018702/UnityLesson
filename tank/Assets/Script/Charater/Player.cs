using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed = 1;
    /// <summary>
    /// 坦克炮弹
    /// </summary>
    public GameObject Bullet;

    public AudioClip FireAudio;

    public GameObject itembag;
    
    private void CreateBullet()
    {
        AudioSource.PlayClipAtPoint(FireAudio, transform.position);
        GameObject go = GameObject.Instantiate(Bullet, transform.position, Quaternion.Euler(transform.eulerAngles));
        go.SetActive(true);
    }
    private void ControlMethod()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0)
        {
            v = 0;
        }
        if (h > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,270));
            
        }
        else if (h < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            
        }
        else if (v < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            
        }
        else if (v > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateBullet();
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(h, v) * MoveSpeed;
    }
    void Start()
    {

    }
    void Update()
    {

    }
    private void FixedUpdate()
    {
        ControlMethod();
    }
}

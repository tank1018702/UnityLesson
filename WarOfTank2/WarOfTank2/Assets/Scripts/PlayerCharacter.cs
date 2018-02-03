using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public SpriteRenderer Tank;

    public float MoveSpeed = 200;

    public Vector3 OffsetRotation = Vector3.zero;

    public Vector3 OrignalRotation = new Vector3(0,0,90);

    /// <summary>
    ///  0 = up 1 = left 2 = down 3 = right
    /// </summary>
    public List<Sprite> SpriteList;
    // tank 炮弹
    public GameObject Missile;

    
    private void MyMethod()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0)
        {
            v = 0;
        }

        if (SpriteList != null)
        {
            if (h > 0)
            {
                Tank.sprite = SpriteList[3];
                OffsetRotation = Vector3.zero ;
            }
            else if (h < 0)
            {
                Tank.sprite = SpriteList[1];
                OffsetRotation = new Vector3(0, 0, 180);
            }
            else if (v < 0)
            {
                Tank.sprite = SpriteList[2];
                OffsetRotation = new Vector3(0, 0, 270) ;
            }
            else if (v > 0)
            {
                Tank.sprite = SpriteList[0];
                OffsetRotation = new Vector3(0, 0, 90);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.CreateMissile();
        }
        transform.Translate(this.MoveSpeed * new Vector3(h, v, 0) * Time.deltaTime, Space.World);
    }



    private void CreateMissile()
    {
        //Debug.LogError(" this.OrignalRotation  " + this.OrignalRotation  + 
        //    " this.OffsetRotation  " + this.OffsetRotation  + "   "
        //    + this.OrignalRotation + this.OffsetRotation);
        GameObject go = GameObject.Instantiate(this.Missile,this.transform.position,Quaternion.Euler(this.OffsetRotation));
        go.SetActive(true);
    }

    private void FixedUpdate()
    {
        this.MyMethod();
    }

    private void Update()
    {
        // Value Range (-1 1)
        //MyMethod();
    }
}

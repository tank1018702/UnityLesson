using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGBulletPool : MonoBehaviour
{
    public static AAGBulletPool BulletPool;

    public GameObject _AAGBullet;
    public GameObject ExplosionAnimation1;

    private List<GameObject> AAGBullets;
    private List<GameObject> ExplosionAnimationList;

    public Transform AAGBulletparent;
    public Transform ExpAnimParent;
    
    [ContextMenu("listCount")]
    void GetListCount()
    {
        Debug.Log(AAGBullets.Count);
    }


    private void Awake()
    {
        BulletPool = this;
    }
    void Start ()
    {
        AAGBullets = new List<GameObject>();
        GameObject bullet = Instantiate(_AAGBullet);
        bullet.SetActive(false);
        AAGBullets.Add(bullet);

        ExplosionAnimationList = new List<GameObject>();
        GameObject Animation = Instantiate(ExplosionAnimation1);
        Animation.SetActive(false);
        ExplosionAnimationList.Add(Animation);
	}
    public GameObject GetExplosionAnimation1(Vector3 position,Quaternion rotation)
    {
        for (int i = 0; i < ExplosionAnimationList.Count; i++)
        {
            if (!ExplosionAnimationList[i].activeInHierarchy)
            {
                ExplosionAnimationList[i].transform.position = position;
                ExplosionAnimationList[i].transform.rotation = rotation;
                ExplosionAnimationList[i].transform.parent = ExpAnimParent;
                return ExplosionAnimationList[i];
            }
        }

        GameObject Animation = Instantiate(ExplosionAnimation1, position, rotation, ExpAnimParent);
        ExplosionAnimationList.Add(Animation);
        return Animation;
    }
	
    public GameObject GetAAGBullets(Vector3 position,Quaternion rotation)
    {
        for(int i=0; i<AAGBullets.Count; i++)
        {
            if(!AAGBullets[i].activeInHierarchy)
            {
                AAGBullets[i].transform.position = position;
                AAGBullets[i].transform.rotation = rotation;
                AAGBullets[i].transform.parent = AAGBulletparent;
                return AAGBullets[i];
            }
        }
        
        GameObject bullet = Instantiate(_AAGBullet,position,rotation,AAGBulletparent);
        AAGBullets.Add(bullet);
        return bullet;
    }
}

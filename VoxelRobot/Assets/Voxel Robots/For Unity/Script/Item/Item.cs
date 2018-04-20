using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoenenGames.VoxelRobot;

public class Item : MonoBehaviour
{
    public ItemInfo info;

    private GameObject target;

    public int Rate;

    public int hp;

    public int money;



    
	
	void Start ()
    {
        info = new ItemInfo();
        info.hp = hp;
        if(money!=0)
        {
            info.Money = Random.Range(money-100, money+100);
        }
        
	}
	
	
	void Update ()
    {
        FindTarget();

		if(target)
        {
            MoveToTarget();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {
            collision.gameObject.GetComponent<CharatherInfo>().GetItem(hp,money);




            Destroy(gameObject);

        }
    }

    void FindTarget()
    {
        target = transform.GetChild(0).GetComponent<ItemTrace>().target;
    }
    void MoveToTarget()
    {
        Vector3 v = target.transform.position - transform.position;
        
        GetComponent<Rigidbody>().velocity = v * 5 ;
    }
}

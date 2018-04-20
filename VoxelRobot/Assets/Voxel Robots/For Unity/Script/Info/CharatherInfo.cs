using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharatherInfo : MonoBehaviour
{
    [SerializeField]
    public int Max_hp;
    [SerializeField]
    public List<GameObject> DropItemList;
    [SerializeField]
    public ParticleSystem DeathAnim;

    bool isAlive;


    private int Cur_hp;

    public int Coin;

    void Start()
    {
        Cur_hp = Max_hp;
        isAlive = true;


        GetComponent<ShowHP>().maxHP = Max_hp;


    }


    void Update()
    {
        if (transform.tag == "Player")
        {
            Debug.Log(Cur_hp);
        }
        if (Cur_hp == 0)
        {
            Die();
        }
        Cur_hpUpdate();


    }

    void Cur_hpUpdate()
    {


        GetComponent<ShowHP>().curHP = Cur_hp;

    }

    void DestoryCharacter()
    {

        if (transform.tag == "Enemy")
        {
            Destroy(GetComponent<ShowHP>().maxHPSlider.gameObject);
            GetComponent<NavMeshAgent>().updatePosition = false;
            GetComponent<NavMeshAgent>().updateRotation = false;
        }
        if(transform.tag=="Player")
        {
            Destroy(GetComponent<ShowHP>().maxHPSlider.gameObject);
        }
        Destroy(gameObject);
    }
    public void Cost_hp(int damage)
    {
        Cur_hp -= damage;
        if (Cur_hp <= 0)
        {
            Cur_hp = 0;
            isAlive = false;
        }
    }
    public void GetItem(int hp, int coin)
    {
        Cur_hp += hp;
        if (Cur_hp > Max_hp)
        {
            Cur_hp = Max_hp;
        }
        Coin += coin;

    }
    void DropItem()
    {
        int r = Random.Range(0, 101);

        if (DropItemList.Count != 0)
        {
            for (int i = 0; i < DropItemList.Count; i++)
            {
                if (DropItemList[i] != null && r < DropItemList[i].GetComponent<Item>().Rate)
                {
                    GameObject temp = Instantiate(DropItemList[i], transform.position, transform.rotation);

                    break;
                }
            }
        }
    }
    void Die()
    {
        DestoryCharacter();
    }
    private void OnDestroy()
    {
        if (gameObject.tag == "Enemy")
        {
            DropItem();
        }
        if (DeathAnim)
        {
            ParticleSystem temp = Instantiate(DeathAnim, transform.position, transform.rotation);
            temp.Play();
        }

    }
}

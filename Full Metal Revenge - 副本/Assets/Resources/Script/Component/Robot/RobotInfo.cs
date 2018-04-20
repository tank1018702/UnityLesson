using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoenenGames.VoxelRobot;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BehaviorTree))]
public class RobotInfo : MonoBehaviour
{
    public string Name;
    public int Attack;
    public int MaxHP;
    public int _current_hp;
    public float MoveSpeed;
    public float BuildingTime;
    public int Resources;

    public Sprite Icon;
    public GameObject Flag;

    public Material red;
    public Material blue;




    BehaviorTree _behaviorTree;
    NavMeshAgent _agent;

    void Start()
    {
        Weapon[] weapons = transform.GetChild(0).GetChild(0).GetComponentsInChildren<Weapon>();

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Damage = Attack;
        }
        _current_hp = MaxHP;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = MoveSpeed;
        _agent.acceleration = 100;

        _behaviorTree = GetComponent<BehaviorTree>();
        MeshRenderer _flag = Flag.GetComponent<MeshRenderer>();
       

        if (transform.tag == "Enemy")
        {
            _behaviorTree.GetVariable("WayPoint").SetValue(GameObject.Find("PlayerMainBase"));
            _behaviorTree.GetVariable("TargetTag").SetValue("Player");
            _flag.material = blue;
            GameManager.EnemyList.Add(gameObject);
            
        }
        else if (transform.tag == "Player")
        {
            _behaviorTree.GetVariable("WayPoint").SetValue(GameObject.Find("EnemyMainBase"));
            _behaviorTree.GetVariable("TargetTag").SetValue("Enemy");
            _flag.material = red;
            GameManager.PlayerList.Add(gameObject);
        }


    }
    void RotationInit()
    {
        if (_behaviorTree.GetVariable("Target").GetValue() as GameObject == null)
        {
            transform.GetChild(0).GetChild(0).GetComponent<EnemyTurret>().RotateToTarget(_behaviorTree.GetVariable("WayPoint").GetValue() as GameObject);
        }
    }
    public void GetDamage(int Damage)
    {
        _current_hp = _current_hp <= 0 ? 0 : _current_hp - Damage;
    }

    void CurrentHp_Update()
    {
        
        if(_current_hp==0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        CurrentHp_Update();
        RotationInit();
    }


}

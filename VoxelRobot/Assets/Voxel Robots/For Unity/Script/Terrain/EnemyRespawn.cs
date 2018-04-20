using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour {

    public List<GameObject> enemyList;
    public List<Transform> respawnPosList;

    private static EnemyRespawn _instance;

    public static EnemyRespawn Instance
    {
        get
        {
            if (_instance ==null)
            {
                _instance = new EnemyRespawn();
            }
            return _instance;
        }
    }

    private float lastTime;
    private float curTime;
    public int intervals=10;

    private int enemyAmount =5;

    // Use this for initialization
    void Start() {
        lastTime = Time.time;
        Wave();

    }

    // Update is called once per frame
    void Update() {
        curTime = Time.time;
        if (curTime - lastTime >= intervals)
        {
            Wave();
            lastTime = curTime;
            
        }
    }
    private void RespawnEnemy ()
    {
        int enemyindex = Random.Range(0, 4);
        int posindex = Random.Range(0, 8);
        GameObject.Instantiate(enemyList[enemyindex], respawnPosList[posindex].position+new Vector3(0,5,0), respawnPosList [posindex ].rotation, respawnPosList[posindex].transform);
    }

    private void Wave()
    {
        for (int i=0; i<enemyAmount;i++)
        {
            RespawnEnemy();
        }
        enemyAmount++;
    }

 
}

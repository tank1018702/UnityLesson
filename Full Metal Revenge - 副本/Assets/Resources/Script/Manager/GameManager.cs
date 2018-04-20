using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] EnemyBuildingControl;

    public GameObject UI_Control;

    public static List<GameObject> PlayerList = new List<GameObject>();
    public static List<GameObject> EnemyList = new List<GameObject>();
    public static List<GameObject> NeutralList = new List<GameObject>();

    public static int PlayerResources;
    public static int EnemyResources;

    public static int PlayerResouresPerSec;
    public static int EnemyResourcesPerSec;

    public static int PlayerControlResources
    {
        get
        {
            return _playerpersec;
        }
        set
        {
            _playerpersec = value;
            PlayerPerSecResources_Update();
        }
    }
    static int _playerpersec;
    public static int EnemyControlResources
    {
        get
        {
            return _enemypersec;
        }
        set
        {
            _enemypersec = value;
            EnemyPerSecResources_Update();
        }
    }
    static int _enemypersec;

    [HideInInspector]
    public static int PlayerBaseHP
    {
        get
        {
            return _playerbase <= 0 ? 0 : _playerbase;
        }
        set
        {
            _playerbase = value;
        }
    }
    static int _playerbase;
    [HideInInspector]
    public static int EnemyBaseHP
    {
        get
        {
            return _enemybase <= 0 ? 0 : _enemybase;
        }
        set
        {
            _enemybase = value;
        }
    }
    static int _enemybase;

    public static int PlayerMaxBase;
    public static int EnemyMaxBase;

    NavMeshModifier[] PlayerUp_Road;
    NavMeshModifier[] PlayerDown_Road;
    NavMeshModifier[] EnemyUp_Road;
    NavMeshModifier[] EnemyDown_Road;

    public GameObject PlayerUP;
    public GameObject PlayerDown;
    public GameObject EnemyUP;
    public GameObject EnemyDown;


    MainUIControl UIscript;

    GlobalVariables GlobalVaule;
    void Start()
    {

        GlobalVaule = GlobalVariables.Instance;
        UIscript = UI_Control ? UI_Control.GetComponent<MainUIControl>() : null;
        PlayerUp_Road = PlayerUP ? PlayerUP.GetComponentsInChildren<NavMeshModifier>() : null;
        PlayerDown_Road = PlayerDown ? PlayerDown.GetComponentsInChildren<NavMeshModifier>() : null;
        EnemyUp_Road = EnemyUP ? EnemyUP.GetComponentsInChildren<NavMeshModifier>() : null;
        EnemyDown_Road = EnemyDown ? EnemyDown.GetComponentsInChildren<NavMeshModifier>() : null;

        PlayerResources = 200;
        EnemyResources = 200;

        PlayerMaxBase = 3000;
        EnemyMaxBase = 3000;

        PlayerBaseHP = PlayerMaxBase;
        EnemyBaseHP = EnemyMaxBase;


        PlayerControlResources = 0;
        EnemyControlResources = 0;


        StartCoroutine(GetTargetListInSence());
        StartCoroutine(ResouresControl());
        StartCoroutine(EnemyRoadControl());

        StartCoroutine(EnemyControl_AI());
    }

    void Ending_update()
    {
        if (PlayerBaseHP == 0)
        {
            Time.timeScale = 0;
            UIscript.EndingUI.gameObject.SetActive(true);
            UIscript.EndingUI.GetChild(1).GetChild(0).GetComponent<Text>().text = "失败";
            //player lose
            Debug.Log("PlayerLose");
        }
        else if (EnemyBaseHP == 0)
        {
            Time.timeScale = 0;
            UIscript.EndingUI.gameObject.SetActive(true);
            UIscript.EndingUI.GetChild(1).GetChild(0).GetComponent<Text>().text = "胜利";
            //enemy lose
            Debug.Log("EnemyLose");
        }
    }
    void Update()
    {
        Ending_update();
    }


    IEnumerator EnemyControl_AI()
    {
        yield return new WaitForSeconds(1f);
        var UIscript = UI_Control.GetComponent<MainUIControl>();
        for (int i = 0; i < EnemyBuildingControl.Length; i++)
        {
            GameObject current = null;
            int r = Random.Range(0, 3);
            UIscript.SetBuildingData(EnemyBuildingControl[i]);
            while (!current)
            {
                UIscript.EnemyBuilding(r, out current);
                yield return new WaitForSeconds(0.5f);
            }
        }
        for (int i = 0; i < EnemyBuildingControl.Length; i++)
        {
            GameObject current = null;
            UIscript.SetBuildingData(EnemyBuildingControl[i]);
            while (!current)
            {
                UIscript.EnemyBuilding_LevelUp(out current);
                yield return new WaitForSeconds(0.5f);
            }
        }
        for (int i = 0; i < EnemyBuildingControl.Length; i++)
        {
            GameObject current = null;
            UIscript.SetBuildingData(EnemyBuildingControl[i]);
            while (!current)
            {
                UIscript.EnemyBuilding_LevelUp(out current);
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return null;
    }
    IEnumerator EnemyRoadControl()
    {
        while (true)
        {
            int r = Random.Range(0, 3);

            switch (r)
            {
                case 0:
                    Enemy_RoadUp();
                    break;
                case 1:
                    Enemy_RoadMiddle();
                    break;
                case 2:
                    Enemy_RoadDown();
                    break;
            }
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator GetTargetListInSence()
    {
        while (true)
        {

            List<GameObject> _PlayerList = MergeList(PlayerList, NeutralList);
            List<GameObject> _EnemyList = MergeList(EnemyList, NeutralList);

            GlobalVaule.GetVariable("AllPlayer").SetValue(_PlayerList);
            GlobalVaule.GetVariable("AllEnemy").SetValue(_EnemyList);


            yield return new WaitForSeconds(0.1f);
        }
    }


    List<GameObject> MergeList(List<GameObject> list1, List<GameObject> list2)
    {
        List<GameObject> _list = new List<GameObject>();
        for (int i = 0; i < list1.Count; i++)
        {
            _list.Add(list1[i]);
        }
        for (int i = 0; i < list2.Count; i++)
        {
            _list.Add(list2[i]);
        }

        return _list;
    }

    IEnumerator ResouresControl()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            PlayerResources += PlayerResouresPerSec;
            EnemyResources += EnemyResourcesPerSec;
        }
    }

    public void Player_RoadInit()
    {
        for (int i = 0; i < 2; i++)
        {
            PlayerUp_Road[i].area = 0;
            PlayerDown_Road[i].area = 0;

        }
    }
    public void Enemy_RoadInit()
    {
        for (int i = 0; i < 2; i++)
        {

            EnemyUp_Road[i].area = 0;
            EnemyDown_Road[i].area = 0;
        }
    }
    public void Enemy_RoadUp()
    {
        Enemy_RoadInit();
        for (int i = 0; i < 2; i++)
        {
            EnemyDown_Road[i].area = 1;
        }
    }



    public void Enemy_RoadMiddle()
    {
        for (int i = 0; i < 2; i++)
        {
            EnemyUp_Road[i].area = 1;
            EnemyDown_Road[i].area = 1;
        }
    }

    public void Enemy_RoadDown()
    {
        Enemy_RoadInit();
        for (int i = 0; i < 2; i++)
        {
            EnemyUp_Road[i].area = 1;
        }

    }
    public void RoadUp()
    {
        Player_RoadInit();
        for (int i = 0; i < 2; i++)
        {
            PlayerDown_Road[i].area = 1;
        }
    }

    public void RoadMiddle()
    {
        for (int i = 0; i < 2; i++)
        {
            PlayerUp_Road[i].area = 1;
            PlayerDown_Road[i].area = 1;
        }
    }

    public void RoadDown()
    {
        Player_RoadInit();
        for (int i = 0; i < 2; i++)
        {
            PlayerUp_Road[i].area = 1;
        }

    }

    static void PlayerPerSecResources_Update()
    {
        switch (PlayerControlResources)
        {
            case 0:
                PlayerResouresPerSec = 10;
                break;
            case 1:
                PlayerResouresPerSec = 20;
                break;
            case 2:
                PlayerResouresPerSec = 35;
                break;
            case 3:
                PlayerResouresPerSec = 60;
                break;
            case 4:
                PlayerResouresPerSec = 90;
                break;
            case 5:
                PlayerResouresPerSec = 150;
                break;


        }
    }

    static void EnemyPerSecResources_Update()
    {
        switch (EnemyControlResources)
        {
            case 0:
                EnemyResourcesPerSec = 15;
                break;
            case 1:
                EnemyResourcesPerSec = 30;
                break;
            case 2:
                EnemyResourcesPerSec = 45;
                break;
            case 3:
                EnemyResourcesPerSec = 75;
                break;
            case 4:
                EnemyResourcesPerSec = 110;
                break;
            case 5:
                EnemyResourcesPerSec = 180;
                break;
        }
    }
    public  void Replay()
    {
        SceneManager.LoadScene("Start");
    }
    public void ESC()
    {
        Application.Quit();
    }


}

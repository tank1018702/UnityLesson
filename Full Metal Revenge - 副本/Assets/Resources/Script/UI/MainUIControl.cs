using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUIControl : MonoBehaviour
{
    public RectTransform StartButton;

    public RectTransform BuildingControlButton;

    public RectTransform BuildingView;

    public RectTransform BuildingType;

    public RectTransform BuildingInfo;

    public RectTransform BuildingRemove;

    public RectTransform [] ToggleList;

    public RectTransform PlayerResourcesText;

    public RectTransform EnemyResourcesText;

    public RectTransform WarningParent;

    public RectTransform PlayerBaseUI;

    public RectTransform EnemyBaseUI;

    public RectTransform EndingUI;

    public GameObject[] ToggleList_Control;

    public GameObject BuildingProgressBar = null;

    public GameObject WarningInfo;

   


    bool IsUIAnim = true;

    public GameObject Player_CurrentBuildingBase = null;

    public GameObject Enemy_CurrentBuildingBase = null;



    //_____________________________________________________________________

    GameObject Building1;
    GameObject Building2;
    GameObject Building3;



    void Start()
    {
        EndingUI.gameObject.SetActive(false);
        Tweener startBtn = StartButton.DOLocalRotate(new Vector3(0, 0, 45), 0.5f, RotateMode.Fast);
        startBtn.SetAutoKill(false);
        startBtn.Pause();

        Tweener BuildingBtn = BuildingControlButton.DOLocalMove(new Vector3(0, 5, 0), 0.5f);
        BuildingBtn.SetAutoKill(false);
        BuildingBtn.Pause();

        Tweener BuildingUI = BuildingView.DOLocalMove(new Vector3(0, 100, 0), 0.5f);
        BuildingUI.SetAutoKill(false);
        BuildingUI.Pause();

        ToggleList_Init();

        Building1 = Resources.Load("Prefab/Building/Final/Killer_Building") as GameObject;
        Building2 = Resources.Load("Prefab/Building/Final/GunBoy_Building") as GameObject;
        Building3 = Resources.Load("Prefab/Building/Final/ElcBall_Building") as GameObject;

    }

    public void UIAnimationCall()
    {
        if (IsUIAnim)
        {
            StartButton.DOPlayForward();
            BuildingControlButton.DOPlayForward();
            IsUIAnim = false;
        }
        else
        {
            StartButton.DOPlayBackwards();
            BuildingControlButton.DOPlayBackwards();
            IsUIAnim = true;
        }
    }
    public void BuildingViewAnimationUP()
    {
        BuildingView.DOPlayForward();
    }
    public void BuildingViewAnimationDown()
    {
        BuildingView.DOPlayBackwards();
    }
    // Update is called once per frame


    public void Toggle_UITirgger(GameObject UI)
    {
        ToggleList_Init();
        UI.SetActive(true);

    }
    void ToggleList_Init()
    {
        foreach(var toggle in ToggleList)
        {
            toggle.GetComponent<Toggle>().isOn = false;
        }
        foreach (var toggle_UI in ToggleList_Control)
        {
           
            toggle_UI.SetActive(false);
        }
    }


    public void PlayerBuilding(string index)
    {
        if (!Player_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding)
        {
            GameObject temp;
            int n;
            if(int.TryParse(index,out n))
            {
                switch (n)
                {
                    case 0:
                        temp = Building1;
                        break;
                    case 1:
                        temp = Building2;
                        break;
                    case 2:
                        temp = Building3;
                        break;
                    default:
                        temp = Building1;
                        break;
                }
            }
            else
            {
                temp = Building1;
            }
            if(temp.GetComponent<Incubator>().ResourceCost<=GameManager.PlayerResources)
            {
                GameManager.PlayerResources -= temp.GetComponent<Incubator>().ResourceCost;
                GameObject go = Instantiate(temp, Player_CurrentBuildingBase.transform);
                go.tag = "PlayerControl";
                Incubator buildingscript = go.GetComponent<Incubator>();
                buildingscript.UI_View = Player_CurrentBuildingBase.GetComponent<BulidingControl>().UI_View;

                Player_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding = go;
                GetBuildingData();
            }
            else
            {
                WarningInfo.GetComponent<Text>().text = "资源不足";
                Instantiate(WarningInfo, WarningParent);
            }

            
        }
        else
        {
            WarningInfo.GetComponent<Text>().text = "此基座上已有建筑";
            Instantiate(WarningInfo, WarningParent);             
        }
    }

    public void EnemyBuilding(int index,out GameObject currentbuilding)
    {
        GameObject temp;

        switch (index)
        {
            case 0:
                temp = Building1;
                break;
            case 1:
                temp = Building2;
                break;
            case 2:
                temp = Building3;
                break;
            default:
                temp = Building1;
                break;
        }
        if (!Enemy_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding)
        {
            if(temp.GetComponent<Incubator>().ResourceCost <= GameManager.EnemyResources)
            {
                GameManager.EnemyResources -= temp.GetComponent<Incubator>().ResourceCost;
                GameObject go = Instantiate(temp, Enemy_CurrentBuildingBase.transform);
                go.tag = "EnemyControl";
                Incubator buildingscript = go.GetComponent<Incubator>();
                buildingscript.UI_View = Enemy_CurrentBuildingBase.GetComponent<BulidingControl>().UI_View;

                Enemy_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding = go;

                go.transform.Rotate(go.transform.up, 180);

                currentbuilding = go;
            }
            else
            {
                currentbuilding = null;
            }        
        }
        else
        {
            currentbuilding = Enemy_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding;
        }

    }
    public void SetBuildingData(GameObject current_base)
    {
        if (current_base.tag == "EnemyControl")
        {
            Enemy_CurrentBuildingBase = current_base;
        }
        else if (current_base.tag == "PlayerControl")
        {
            Player_CurrentBuildingBase = current_base;
        }
    }

     void BuidingInfo_Init()
    {
        Transform current = BuildingInfo.GetChild(0);
        var cur_InfoList = current.GetComponentsInChildren<Text>();
        cur_InfoList[0].text = "";
        cur_InfoList[1].text = "";
        cur_InfoList[2].text = "";
        cur_InfoList[3].text = "";
        cur_InfoList[4].text = "";
        current.GetChild(0).GetComponent<Image>().enabled = false;

        Transform NextLevel = BuildingInfo.GetChild(1);
        var next_InfoList = NextLevel.GetComponentsInChildren<Text>();
        next_InfoList[0].text = "";
        next_InfoList[1].text = "";
        next_InfoList[2].text = "";
        next_InfoList[3].text = "";
        next_InfoList[4].text = "";
        NextLevel.GetChild(0).GetComponent<Image>().enabled = false;

        BuildingInfo.GetChild(3).gameObject.SetActive(false);
        BuildingRemove.GetChild(0).GetChild(2).GetComponent<Image>().gameObject.SetActive(false);
    }

    public void GetBuildingData()
    {
        ToggleList_Init();
        BuidingInfo_Init();
        GameObject cur_building = Player_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding;
        if (cur_building)
        {
            Transform current = BuildingInfo.GetChild(0);
            var cur_InfoList = current.GetComponentsInChildren<Text>();
            var robotInfo = cur_building.GetComponent<Incubator>().Robot.GetComponent<RobotInfo>();
            cur_InfoList[0].text = robotInfo.Name;
            cur_InfoList[1].text = "装甲值:"+robotInfo.MaxHP;
            cur_InfoList[2].text = "攻击力:"+robotInfo.Attack;
            cur_InfoList[3].text = "行动力:"+robotInfo.MoveSpeed;
            cur_InfoList[4].text = "建造时间:"+robotInfo.BuildingTime;
            cur_InfoList[5].text = "消耗资源:" + robotInfo.Resources;
            current.GetChild(0).GetComponent<Image>().enabled = true;
            current.GetChild(0).GetComponent<Image>().sprite = robotInfo.Icon;
            BuildingRemove.GetChild(0).GetChild(2).GetComponent<Image>().gameObject.SetActive(true);
            BuildingRemove.GetChild(0).GetChild(2).GetComponent<Image>().sprite = robotInfo.Icon;
            var Buildingscript = cur_building.GetComponent<Incubator>();
            if(Buildingscript.NextLevel)
            {
                Transform NextLevel = BuildingInfo.GetChild(1);
                var next_Infolist = NextLevel.GetComponentsInChildren<Text>();
                var LevelUpRobotInfo = Buildingscript.NextLevel.GetComponent<Incubator>().Robot.GetComponent<RobotInfo>();
                next_Infolist[0].text = LevelUpRobotInfo.Name;
                next_Infolist[1].text = "装甲值:" + LevelUpRobotInfo.MaxHP;
                next_Infolist[2].text = "攻击力:" + LevelUpRobotInfo.Attack;
                next_Infolist[3].text = "行动力:" + LevelUpRobotInfo.MoveSpeed;
                next_Infolist[4].text = "建造时间:" + LevelUpRobotInfo.BuildingTime;
                next_Infolist[5].text = "消耗资源" + LevelUpRobotInfo.Resources;
                NextLevel.GetChild(0).GetComponent<Image>().enabled = true;
                NextLevel.GetChild(0).GetComponent<Image>().sprite = LevelUpRobotInfo.Icon;
            }
            else
            {
                BuildingInfo.GetChild(3).gameObject.SetActive(true);
            }
        }  
      
        
        
    }



    public void PlayerBuilding_LevelUp()
    {
        GameObject cur_building = Player_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding;
        if(cur_building)
        {
            var Buildingscript = cur_building.GetComponent<Incubator>();
            if(Buildingscript.NextLevel)
            {
                if(Buildingscript.NextLevel.GetComponent<Incubator>().ResourceCost<=GameManager.PlayerResources)
                {
                    GameManager.PlayerResources -= Buildingscript.NextLevel.GetComponent<Incubator>().ResourceCost;
                    GameObject go = Instantiate(Buildingscript.NextLevel, Player_CurrentBuildingBase.transform);
                    Destroy(cur_building);
                    go.tag = "PlayerControl";
                    Incubator buildingscript = go.GetComponent<Incubator>();
                    buildingscript.UI_View = Player_CurrentBuildingBase.GetComponent<BulidingControl>().UI_View;
                    Player_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding = go;
                    GetBuildingData();
                }
                else
                {
                    WarningInfo.GetComponent<Text>().text = "资源不足";
                    Instantiate(WarningInfo, WarningParent);
                }
               
            }
            else
            {
                WarningInfo.GetComponent<Text>().text = "建筑已达到最大等级";
                Instantiate(WarningInfo, WarningParent);
                
            }
        }
        else
        {
            Debug.Log("CurrentBuilding is empty");
        }
    }

    public void EnemyBuilding_LevelUp(out GameObject current)
    {
        GameObject cur_building = Enemy_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding;
        var Buildingscript = cur_building.GetComponent<Incubator>();
        if (Buildingscript.NextLevel.GetComponent<Incubator>().ResourceCost <= GameManager.EnemyResources)
        {
            GameManager.EnemyResources -= Buildingscript.NextLevel.GetComponent<Incubator>().ResourceCost;
            GameObject go = Instantiate(Buildingscript.NextLevel, Enemy_CurrentBuildingBase.transform);
            go.transform.Rotate(go.transform.up, 180);
            Destroy(cur_building);
            go.tag = "EnemyControl";
            Incubator buildingscript = go.GetComponent<Incubator>();
            buildingscript.UI_View = Enemy_CurrentBuildingBase.GetComponent<BulidingControl>().UI_View;
            Enemy_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding = go;
            current = go;

        }
        else
        {
            current = null;
        }

    }
    public void Player_RemoveBuilding()
    {
        GameObject cur_building = Player_CurrentBuildingBase.GetComponent<BulidingControl>().CurrentBuilding;
        if(cur_building)
        {
            Destroy(cur_building);
        }
    }

    void ResourcesUI_Update()
    {
        var text1 = PlayerResourcesText.GetComponentsInChildren<Text>();
        text1[0].text = GameManager.PlayerControlResources.ToString();
        text1[1].text = GameManager.PlayerResources.ToString();
        text1[2].text = GameManager.PlayerResouresPerSec.ToString() + "/3S";

        var text2 = EnemyResourcesText.GetComponentsInChildren<Text>();
        text2[0].text = GameManager.EnemyControlResources.ToString();
        text2[1].text = GameManager.EnemyResources.ToString();
        text2[2].text = GameManager.EnemyResourcesPerSec.ToString() + "/3S";
    }

    void BaseHp_Update()
    {
        PlayerBaseUI.GetChild(0).GetComponent<Image>().fillAmount = (GameManager.PlayerBaseHP * 1f) / (GameManager.PlayerMaxBase * 1f);
        PlayerBaseUI.GetChild(1).GetComponent<Text>().text = GameManager.PlayerBaseHP.ToString() + "/3000";
        EnemyBaseUI.GetChild(0).GetComponent<Image>().fillAmount = (GameManager.EnemyBaseHP * 1f) / (GameManager.EnemyMaxBase * 1f);
        EnemyBaseUI.GetChild(1).GetComponent<Text>().text = GameManager.EnemyBaseHP.ToString() + "/3000";
    }

    
    void Update()
    {
        BaseHp_Update();
        ResourcesUI_Update(); 
    }


}

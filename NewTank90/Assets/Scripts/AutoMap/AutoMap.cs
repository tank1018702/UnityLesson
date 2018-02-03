using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapUnit
{
    public enum UnitType
    {
        Unkown =-1,
        Walls = 0,
        Water = 1,
        Grass = 2,
        wall = 3,
    }

    public UnitType SelfUnitType = UnitType.Unkown;
    public Vector2Int SelfUnitPos = Vector2Int.zero;
    public Sprite ImageSprite = null;
    public GameObject ImageOBJ = null;

    private BoxCollider2D SelfBoxCollider;


    public MapUnit LeftMapUnit {
        get
        {
            MapUnit unit =   this.mapUnitlist.Find(x=> (x.Key.SelfUnitPos.x == (this.SelfUnitPos.x - 1)
            && x.Key.SelfUnitPos.y == this.SelfUnitPos.y)).Key;
            return unit;
        }
    }
    public MapUnit RightMapUnit
    {
        get
        {
            MapUnit unit = this.mapUnitlist.Find(x => (x.Key.SelfUnitPos.x == (this.SelfUnitPos.x + 1)
            && x.Key.SelfUnitPos.y == this.SelfUnitPos.y)).Key;
            return unit;
        }
    }
    public MapUnit UpMapUnit
    {
        get
        {
            MapUnit unit = this.mapUnitlist.Find(x => (x.Key.SelfUnitPos.x == (this.SelfUnitPos.x)
            && (x.Key.SelfUnitPos.y + 1) == this.SelfUnitPos.y)).Key;
            return unit;
        }
    }
    public MapUnit DownMapUnit
    {
        get
        {
            MapUnit unit = this.mapUnitlist.Find(x => (x.Key.SelfUnitPos.x == (this.SelfUnitPos.x)
            && (x.Key.SelfUnitPos.y - 1) == this.SelfUnitPos.y)).Key;
            return unit;
        }
    }

    private Vector2 mapMax = Vector2.zero;
    private List<KeyValuePair<MapUnit, GameObject>> mapUnitlist;

    public MapUnit(GameObject go, Vector2Int pos,ref List<KeyValuePair<MapUnit, GameObject>> list ,Vector2 mapmax = default(Vector2))
    {
        this.ImageOBJ = go;
        this.SelfUnitPos = pos;
        this.mapMax = mapmax;
        this.mapUnitlist = list;
        this.SetSelfUnitType();
    }

    public void SetSelfUnitType()
    {
        int selftype = Random.Range(0,4);
        switch (selftype)
        {
            case 0:
                SelfUnitType = UnitType.Walls;
                ImageSprite = ResuorceManager.Instance.GetImageSpriteByName("walls");
                break;
            case 1:
                SelfUnitType = UnitType.Water;
                ImageSprite = ResuorceManager.Instance.GetImageSpriteByName("water");
                SelfBoxCollider = this.ImageOBJ.GetComponent<BoxCollider2D>();
                if (this.SelfBoxCollider != null)
                {
                    this.SelfBoxCollider.isTrigger = true;
                }
                break;
            case 2:
                SelfUnitType = UnitType.Grass;
                ImageSprite = ResuorceManager.Instance.GetImageSpriteByName("grass");
                this.SelfBoxCollider = this.ImageOBJ.GetComponent<BoxCollider2D>();
                if (this.SelfBoxCollider != null)
                {
                    this.SelfBoxCollider.isTrigger = true;
                }
                break;
            case 3:
                SelfUnitType = UnitType.wall;
                ImageSprite = ResuorceManager.Instance.GetImageSpriteByName("wall");
                break;
            default:
                SelfUnitType = UnitType.Unkown;
                break;
        }
    }

}

public class AutoMap : MonoBehaviour
{
    #region 建筑物
    public Image BigWall;
    public Image SmallWall;
    public Image Water;
    public Image Grass;
    #endregion

    public GameObject WallParent;
    public GameObject FirstWallPos;

    public Vector2 Map_W_H = new Vector2(1440, 840);

    public int BigWallW_H = 60;
    public int SmallallW_H = 15;
    public int WaterW_H = 60;
    public int GrassW_H = 60;

    public List<KeyValuePair<MapUnit, GameObject>> MapUnitList = null;

    /// <summary>
    /// each round enemy count
    /// </summary>
    public int EnmyCount { get { return Random.Range(3, 6); } }
    /// <summary>
    /// Enemｙround
    /// </summary>
    public int EnemyRound { get { return Random.Range(3, 6); } }
    /// <summary>
    /// All Enemy
    /// </summary>
    public int AllEnemyCount { get { return this.EnmyCount * this.EnemyRound; } }

    /// <summary>
    /// weather first map when player born;
    /// </summary>
    public bool IsFirstMap = false;
    /// <summary>
    /// first born pos in first map, otherwise is fourDoor pos.
    /// </summary>
    public Vector2Int PlayerBornPos = Vector2Int.zero;
    /// <summary>
    /// first born pos in first map, otherwise is fourDoor pos.
    /// </summary>
    public Vector2Int Player2BronPos = Vector2Int.one;

    public void Start()
    {
        this.OneKeyAutoMap();
    }

    public KeyValuePair<MapUnit, GameObject> LeftDoorPos {
        get
        {
            return this.MapUnitList.Find(x => (x.Key.SelfUnitPos.x == 0 && x.Key.SelfUnitPos.y == this.mapmax.y / 2));
        }
    }
    public KeyValuePair<MapUnit, GameObject> RightDoorPos
    {
        get
        {
            return this.MapUnitList.Find(x => (x.Key.SelfUnitPos.x == this.mapmax.x && x.Key.SelfUnitPos.y == this.mapmax.y / 2));
        }
    }
    public KeyValuePair<MapUnit, GameObject> UpDoorPos
    {
        get
        {
            return this.MapUnitList.Find(x => (x.Key.SelfUnitPos.x / 2 == 0 && x.Key.SelfUnitPos.y == this.mapmax.y));
        }
    }
    public KeyValuePair<MapUnit, GameObject> DownDoorPos
    {
        get
        {
            return this.MapUnitList.Find(x => (x.Key.SelfUnitPos.x / 2 == 0 && x.Key.SelfUnitPos.y == 0));
        }
    }


    /// <summary>
    /// weather get player pos or not
    /// </summary>
    private bool isgetPlayerPos = false;
    private MapUnit playerPosMapunit = null;
    private Vector2Int mapmax = Vector2Int.zero;
    /// <summary>
    /// 生成的几条路径
    /// </summary>
    private List<MapUnit> navgations;
    /// <summary>
    /// monster bron pos
    /// </summary>
    private List<Vector2Int> MonsterBronList;


    #region create wall and navgation
    private void GetBronPosAndShowNav(Vector2Int beginpos)
    {
        this.GetMapUnitEachStep(beginpos, navgations);
        for (int i = 0; i < this.navgations.Count; i++)
        {
            if (navgations[i] != null && navgations[i].ImageOBJ != null)
            {
                navgations[i].ImageOBJ.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 生成路径
    /// </summary>
    [ContextMenu("CreateNav")]
    public void CreateNav()
    {
        //如何生成路径呢？   随机一个点 （点与点之间 不能重合） 四个方向一直到找不到mapunit为止
        // 每一波敌人有多少个,就是生成几条路路径 
        //每一张地图都有 四个门 （第一张地图 还包含一个出生点） 

        if (navgations == null)
        {
            navgations = new List<MapUnit>();
        }
        for (int m = 0; m < this.EnmyCount; m++)
        {

            // 1 获取 不同的怪物随即点
            // 2 获取这个点四个方向上所有的点
            //1
            Vector2Int beginpos = this.GetMonsterBron();

            //2
            //Debug.LogError("beginpos " + beginpos);
            this.GetBronPosAndShowNav(beginpos);
        }

        //3 player的位置也要 生成路径 不然会被框起来 不能移动
        this.GetBronPosAndShowNav(this.PlayerBornPos);

    }

    private Vector2Int GetMonsterBron()
    {
        if (this.MonsterBronList == null) this.MonsterBronList = new List<Vector2Int>();
        Vector2Int pos = new Vector2Int(Random.Range(0, this.mapmax.x ), Random.Range(0, this.mapmax.y ));
        if (Vector2Int.Equals(this.PlayerBornPos, pos) || this.MonsterBronList.Contains(pos))
        {
            return this.GetMonsterBron();
        }
        this.MonsterBronList.Add(pos);
        return pos;
    }


    private void GetMapUnitEachStep(Vector2Int stepPos, List<MapUnit> navlist = null)
    {
        KeyValuePair<MapUnit, GameObject> newKV = new KeyValuePair<MapUnit, GameObject>();
        newKV = this.MapUnitList.Find(x => x.Key.SelfUnitPos.Equals(stepPos));
        navlist.Add(newKV.Key);
        if (newKV.Key != null)
        {
            // not null ，get next mapunit
            //this.GetMapUnitEachStep(this.GetRandomMap(navlist, newKV.Key), navlist);
            this.GetAllDrictionMapUnit(1, newKV.Key, navlist);
            this.GetAllDrictionMapUnit(2, newKV.Key, navlist);
            this.GetAllDrictionMapUnit(3, newKV.Key, navlist);
            this.GetAllDrictionMapUnit(4, newKV.Key, navlist);
        }
    }

    private void SubGetSingDrictionMapUnit(MapUnit unit, MapUnit dricUnit, int dric, ref List<MapUnit> navlist)
    {
        if (unit != null && dricUnit != null)
        {
            if (!navlist.Contains(dricUnit))
            {
                navlist.Add(dricUnit);
            }
            this.GetAllDrictionMapUnit(dric, dricUnit, navlist);
        }
    }

    private void GetAllDrictionMapUnit(int dirction, MapUnit unit, List<MapUnit> navlist)
    {

        switch (dirction)
        {
            case 1: // left
                //Debug.LogError("left");
                this.SubGetSingDrictionMapUnit(unit, unit.LeftMapUnit, dirction, ref navlist);
                break;
            case 2: // down
                //Debug.LogError("down");
                this.SubGetSingDrictionMapUnit(unit, unit.DownMapUnit, dirction, ref navlist);
                break;
            case 3: // right
                //Debug.LogError("right");
                this.SubGetSingDrictionMapUnit(unit, unit.RightMapUnit, dirction, ref navlist);
                break;
            case 4: // up
                //Debug.LogError("up");
                this.SubGetSingDrictionMapUnit(unit, unit.UpMapUnit, dirction, ref navlist);
                break;
            default: // unkown
                break;
        }
    }

    
    [ContextMenu("CreateWallsData")]
    public void CreateWallsData()
    {
        if (MapUnitList == null)
        {
            MapUnitList = new List<KeyValuePair<MapUnit, GameObject>>();
        }
        mapmax = new Vector2Int((int)this.Map_W_H.x / this.BigWallW_H, (int)this.Map_W_H.y / this.BigWallW_H);
        for (int i = 0; i < mapmax.x; i++)
        {
            for (int j = 0; j < mapmax.y; j++)
            {
                GameObject go = this.CreateWall();
                MapUnit mapunit = new MapUnit(go, new Vector2Int(i, j), ref MapUnitList, mapmax);

                KeyValuePair<MapUnit, GameObject> unit = new KeyValuePair<MapUnit, GameObject>(mapunit, go);
                MapUnitList.Add(unit);
                go.GetComponent<Image>().sprite = mapunit.ImageSprite;
                go.transform.localPosition = new Vector3(i * this.BigWallW_H + this.FirstWallPos.transform.localPosition.x,
                    j * this.BigWallW_H + this.FirstWallPos.transform.localPosition.y,
                    this.FirstWallPos.transform.localPosition.z);
                go.name = "wall_" + i.ToString() + " " + j.ToString();
            }
        }
    }

    public GameObject CreateWall()
    {
        GameObject go = null;
        go = GameObject.Instantiate(this.FirstWallPos, this.WallParent.transform);
        go.SetActive(true);
        go.transform.localScale = Vector3.one;
        return go;
    }
    #endregion

    #region crate monster and set player  pos
    [ContextMenu("SetPlayerPos")]
    public void SetPlayerPos()
    {
        SelfGameManager.Instance.Player_One.transform.localPosition = this.MapUnitList.Find(x => x.Key.SelfUnitPos.Equals(this.PlayerBornPos)).Value.transform.localPosition;

        SelfGameManager.Instance.Player_Two.transform.localPosition = this.MapUnitList.Find(x => x.Key.SelfUnitPos.Equals(this.Player2BronPos)).Value.transform.localPosition;
        
        SelfGameManager.Instance.Player_One.gameObject.SetActive(!SelfGameManager.Instance.IsTwoPlayer);
        SelfGameManager.Instance.Player_Two.gameObject.SetActive(SelfGameManager.Instance.IsTwoPlayer);
    }


    /// <summary>
    /// create monster
    /// </summary>
    [ContextMenu("CreateMonsters")]
    private void CreateMonsters()
    {
        for (int i= 0;i< this.MonsterBronList.Count ;i++)
        {
            GameObject go = ResuorceManager.Instance.GetPrefabByName("Monster");
            if (go == null) continue;
            go.transform.SetParent(SelfGameManager.Instance.UICanvas.transform);
            go.transform.localScale = Vector3.one;

            var mk = this.MapUnitList.Find(x => x.Key.SelfUnitPos.Equals(this.MonsterBronList[i])).Key;
            if (mk == null )
            {
                Debug.LogError("mk is null, vector2 is" + this.MonsterBronList[i]);
            }
            if (mk.ImageOBJ == null)
            {
                Debug.LogError("mk.imageobj is null");
            }
            var mv = mk.ImageOBJ.transform.localPosition;
            go.transform.localPosition = mv;

            //set foward

        }
    }
    [ContextMenu("CreateSingleMonster")]
    private void CreateSingleMonster()
    {
        GameObject mon = ResuorceManager.Instance.GetPrefabByName("Monster");
    }

    [ContextMenu("OneKeyAutoMap % h % j")]
    public void OneKeyAutoMap()
    {
        this.CreateWallsData();
        this.CreateNav();
        this.CreateMonsters();
        this.SetPlayerPos();
    }

    #endregion


    #region test
    public Vector2Int TextV = new Vector2Int(1,1);
    [ContextMenu("TestFindAroundMapUnit")]
    public void TestFindAroundMapUnit()
    {
        KeyValuePair<MapUnit, GameObject> value = this.MapUnitList.Find(x => (x.Key.SelfUnitPos.x == TextV.x && x.Key.SelfUnitPos.y == TextV.y));

        if (value.Key == null)
        {
            return;
        }

        MapUnit unit = value.Key;

        GameObject go = null;
        if (unit.LeftMapUnit != null)
        {
             go =  this.MapUnitList.Find(x => x.Key == unit.LeftMapUnit).Value;
            go.SetActive(false);
        }
        if (unit.RightMapUnit != null)
        {
            go = this.MapUnitList.Find(x => x.Key == unit.RightMapUnit).Value;
            go.SetActive(false);
        }

        if (unit.UpMapUnit != null)
        {
            go = this.MapUnitList.Find(x => x.Key == unit.UpMapUnit).Value;
            go.SetActive(false);
        }
        if (unit.DownMapUnit != null)
        {
            go = this.MapUnitList.Find(x => x.Key == unit.DownMapUnit).Value;
            go.SetActive(false);
        }
    }

    #endregion



}

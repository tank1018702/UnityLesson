using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Map : MonoBehaviour
{
    const int Height = 32;
    const int Width = 32;

    int[,] map = new int[Height, Width];

    const int StartPoint = 8;
    const int EndPoint = 9;
    const int Wall = 1;

    public GameObject _Wall;
    public GameObject _StartPoint;
    public GameObject _EndPoint;
    public GameObject _Way;
    public GameObject _path;


    Pos StartPosition = null;
    Pos EndPosition = null;

    delegate bool Func(Pos cur, int ox, int oy);


    void Start()
    {
        ReadMapFile();
        StartCoroutine(InitMap());

    }



    void Update()
    {


    }
    IEnumerator InitMap()
    {
        GameObject Walls = new GameObject();
        Walls.name = "Walls";
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (map[i, j] == Wall)
                {
                    GameObject go = Instantiate(_Wall, new Vector3(j * 1, 0.5f, i * 1), Quaternion.identity, Walls.transform);
                    yield return new WaitForSeconds(0.01f);
                }
                if (map[i, j] == StartPoint)
                {
                    GameObject go = Instantiate(_StartPoint, new Vector3(j * 1, 0.5f, i * 1), Quaternion.identity, Walls.transform);
                    StartPosition = new Pos(j, i);
                    yield return new WaitForSeconds(0.01f);
                }
                if (map[i, j] == EndPoint)
                {
                    GameObject go = Instantiate(_EndPoint, new Vector3(j * 1, 0.5f, i * 1), Quaternion.identity, Walls.transform);
                    EndPosition = new Pos(j, i);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        yield return new WaitForSeconds(1f);

        StartCoroutine(Astar_search());

        yield return null;
    }
    public void ReadMapFile()
    {
        string path = Application.dataPath + "//" + "map.txt";
        if (!File.Exists(path))
        {
            return;
        }
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader read = new StreamReader(fs, Encoding.Default);

        string strReadline = "";
        int y = 0;

        read.ReadLine();
        strReadline = read.ReadLine();

        while (strReadline != null && y < Height)
        {
            for (int x = 0; x < Width && x < strReadline.Length; x++)
            {
                int t;
                switch (strReadline[x])
                {
                    case '1':
                        t = 1;
                        break;
                    case '8':
                        t = 8;
                        break;
                    case '9':
                        t = 9;
                        break;
                    default:
                        t = 0;
                        break;
                }
                map[y, x] = t;
            }
            y += 1;
            strReadline = read.ReadLine();
        }
        read.Dispose();
        fs.Close();
    }
    short[,] bfs_search = null;
    int cur_depth = 0;
    IEnumerator BFS_Search()
    {
        bfs_search = new short[map.GetLength(0), map.GetLength(1)];

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                bfs_search[i, j] = short.MaxValue;
            }
        }
        List<Pos> searchlist = new List<Pos>();
        searchlist.Add(StartPosition);
        bfs_search[StartPosition.y, StartPosition.x] = 0;

        Func func = (Pos cur, int ox, int oy) =>
        {
            if (map[cur.y + oy, cur.x + ox] == EndPoint)
            {
                bfs_search[cur.y + oy, cur.x + ox] = (short)(bfs_search[cur.y, cur.x] + 1);
                Pos temp = new Pos(cur.x + ox, cur.y + oy);
                temp.Next = cur;
                EndPosition.Next = temp;
                Debug.Log("寻路完成");
                //DarwLine(EndPosition);
                StartCoroutine(BFS_DrawLine(EndPosition));
                return true;

            }
            if (map[cur.y + oy, cur.x + ox] == 0)
            {
                if (bfs_search[cur.y + oy, cur.x + ox] > bfs_search[cur.y, cur.x] + 1)
                {
                    bfs_search[cur.y + oy, cur.x + ox] = (short)(bfs_search[cur.y, cur.x] + 1);
                    Pos temp = new Pos(cur.x + ox, cur.y + oy);
                    temp.Next = cur;
                    searchlist.Add(temp);
                }
            }
            return false;
        };

        while (searchlist.Count > 0)
        {
            Pos cur = searchlist[0];

            searchlist.RemoveAt(0);
            if (cur.y > 0)
            {
                if (func(cur, 0, -1))
                {
                    break;
                }
            }
            if (cur.y < Height - 1)
            {
                if (func(cur, 0, 1))
                {
                    break;
                }
            }
            if (cur.x > 0)
            {
                if (func(cur, -1, 0))
                {
                    break;
                }
            }
            if (cur.x < Width - 1)
            {
                if (func(cur, 1, 0))
                {
                    break;
                }
            }
            if (bfs_search[cur.y, cur.x] > cur_depth)
            {
                cur_depth = bfs_search[cur.y, cur.x];
                Bfs_Show_Search(bfs_search);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    AScore[,] astar_search;
    IEnumerator Astar_search()
    {
        astar_search = new AScore[map.GetLength(0), map.GetLength(1)];

        List<Pos> list = new List<Pos>();

        astar_search[StartPosition.y, StartPosition.x] = new AScore(0, 0);
        list.Add(StartPosition);

        // 每一个点的处理函数
        Func func = (Pos cur, int ox, int oy) =>
        {
            var o_score = astar_search[cur.y + oy, cur.x + ox];
            if (o_score != null && o_score.closed)
            {
                return false;
            }
            var cur_score = astar_search[cur.y, cur.x];
            Pos o_pos = new Pos(cur.x + ox, cur.y + oy);
            if (map[cur.y + oy, cur.x + ox] == EndPoint)
            {
                var a = new AScore(cur_score.G + 1, 0);
                a.parent = cur;
                astar_search[cur.y + oy, cur.x + ox] = a;
                Debug.Log("寻路完成");
                return true;
            }
            if (map[cur.y + oy, cur.x + ox] == 0)
            {
                if (o_score == null)
                {
                    var a = new AScore(cur_score.G+1, Pos.AStarDistance(o_pos, EndPosition));
                    a.parent = cur;
                    astar_search[cur.y + oy, cur.x + ox] = a;
                    list.Add(o_pos);
                }
                else if (o_score.G > cur_score.G + 1)
                {
                    o_score.G = cur_score.G + 1;
                    o_score.parent = cur;
                    if (!list.Contains(o_pos))
                    {
                        list.Add(o_pos);
                    }
                }
            }
            return false;
        };


        while (list.Count > 0)
        {
            list.Sort((Pos p1, Pos p2) =>
            {
                AScore a1 = astar_search[p1.y, p1.x];
                AScore a2 = astar_search[p2.y, p2.x];
                //return a1.H.CompareTo(a2.H);
                return a1.CompareTo(a2);
            });
            Pos cur = list[0];
            list.RemoveAt(0);
            astar_search[cur.y, cur.x].closed = true;

 
            if (cur.y > 0)
            {
                if (func(cur, 0, -1)) { break; }
            }
    
            if (cur.y < Height - 1)
            {
                if (func(cur, 0, 1)) { break; }
            }
     
            if (cur.x > 0)
            {
                if (func(cur, -1, 0)) { break; }
            }
           
            if (cur.x < Width - 1)
            {
                if (func(cur, 1, 0)) { break; }
            }

            short[,] temp_map = new short[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; ++j)
                {
                    temp_map[i, j] = short.MaxValue;
                    //if (map_search[i,j] != null && map_search[i,j].closed)
                    if (astar_search[i, j] != null)
                    {
                        temp_map[i, j] = (short)astar_search[i, j].F;
                    }
                }
            }
            Show_Search(temp_map);

            yield return new WaitForSeconds(0.01f);
        }
        
        yield return null;


    }
    void GetMinimumCostPoint(List<Pos> search_list)
    {
        for (int i = 0; i < search_list.Count; i++)
        {

        }
    }

    IEnumerator BFS_DrawLine(Pos end)
    {
        while (end.Next != null)
        {
            GameObject go = Instantiate(_path, new Vector3(end.x, 0.11f, end.y), Quaternion.identity);
            end = end.Next;
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("路径生成完毕");
        yield return null;

    }
    void Bfs_Show_Search(short[,] temp_map)
    {
        Debug.Log("11111111111111");

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (map[i, j] == 0 && temp_map[i, j] != short.MaxValue)
                {
                    GameObject go = Instantiate(_Way, new Vector3(j * 1, 0.1f, i * 1), Quaternion.identity);

                    go.transform.GetChild(0).GetComponent<TextMesh>().text =bfs_search[i,j].ToString();
                }
            }
        }
    }

    void Show_Search(short[,] temp_map)
    {
        Debug.Log("11111111111111");
        
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (map[i, j] == 0 && temp_map[i, j] != short.MaxValue)
                {
                    GameObject go = Instantiate(_Way, new Vector3(j * 1, 0.1f, i * 1), Quaternion.identity);

                    go.transform.GetChild(0).GetComponent<TextMesh>().text = astar_search[i, j].F.ToString();
                }
            }
        }
    }
    public class Pos
    {
        public int x = 0;
        public int y = 0;

        public Pos Next;
        public Pos()
        {
            Next = null;
        }

        public Pos(Pos p)
        {
            x = p.x;
            y = p.y;
            Next = null;
        }

        public static float AStarDistance(Pos p1, Pos p2)
        {
            float d1 = Mathf.Abs(p1.x - p2.x);
            float d2 = Mathf.Abs(p1.y - p2.y);
            return d1 + d2;
        }

        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
            Next = null;
        }
        public bool Equals(Pos p)
        {
            return x == p.x && y == p.y;

        }
    }
    public class AScore
    {
        // G是从起点出发的步数
        public float G = 0;
        // H是估算的离终点距离
        public float H = 0;

        public bool closed = false;

        public Pos parent = null;

        public AScore(float g, float h)
        {
            G = g;
            H = h;
            closed = false;
        }

        public float F
        {
            get
            {
                return G + H;
            }
        }

        public int CompareTo(AScore a2)
        {
            if (F == a2.F)
            {
                return 0;
            }
            if (F > a2.F)
            {
                return 1;
            }
            return -1;
        }

        public bool Equals(AScore a)
        {
            if (a.F == F)
            {
                return true;
            }
            return false;
        }

    }


}

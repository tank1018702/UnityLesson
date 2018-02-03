using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
/// <summary>
/// save loaded Resuorces
/// </summary>
public class ResuorcesLoadedManager:Singleton<ResuorcesLoadedManager>
{
#if UNITY_EDITOR
    public bool IsEditor = true;
#else
    public bool IsEditor = false;
#endif


    public Dictionary<string,GameObject> LoadedPrefab;
    public Dictionary<string, Sprite> LoadedImageSprite;
    /// <summary>
    /// 子弹管理
    /// </summary>
    public List<GameObject> Bullets;
    public List<GameObject> BoomEffects;

    private ResuorcesLoadedManager()
    {
        this.LoadedPrefab = new Dictionary<string, GameObject>();
        this.LoadedImageSprite = new Dictionary<string, Sprite>();
        this.Bullets = new List<GameObject>();
        this.BoomEffects = new List<GameObject>();
    }


    public void Add<T,K>(Dictionary<T,K> a,T b,K c) where K : UnityEngine.Object
    {
        if (a == null) a = new Dictionary<T, K>();
        if (!a.ContainsKey(b))
        {
            a.Add(b, c);
        }
        else
        {
            a[b] = c;
        }
    }

    /// <summary>
    /// 将 加载出的prefab 资源 存放在 对应的 缓存里面
    /// </summary>
    /// <param name="go"></param>
    private void LoadedPrefabAdd(string name, GameObject go)
    {
        this.Add<string,GameObject>(this.LoadedPrefab,name,go);
    }
    
    private void LoadedIamgeSpriteAdd(string name,Sprite spr)
    {
        this.Add<string, Sprite>(this.LoadedImageSprite, name, spr);
    }

    public GameObject GetPrefabObj(string xname)
    {
        if (this.LoadedPrefab == null) this.LoadedPrefab = new Dictionary<string, GameObject>();
        if (!this.LoadedPrefab.ContainsKey(xname))
        {
            //todo: 记得修改 加载方式 AB加载
            GameObject go = Resources.Load<GameObject>(xname);
            this.Add<string, GameObject>(this.LoadedPrefab,xname,go);
            return go;
        }
        return this.LoadedPrefab[xname];
    }

    public Sprite GetSpriteObj(string xname)
    {
        if (this.LoadedImageSprite == null) this.LoadedImageSprite = new Dictionary<string, Sprite>();
        if (!this.LoadedImageSprite.ContainsKey(xname))
        {
            //todo: 记得修改 加载方式 AB加载
            Sprite go = ResuorceManager.Instance.GetImageSpriteByName(xname);
            this.Add<string, Sprite>(this.LoadedImageSprite, xname, go);
            return go;
        }
        return this.LoadedImageSprite[xname];
    }

    public T GetElseSetLoadedObject<T>(object a, string name = default(string) )where T :UnityEngine.Object
    {
        if (a == typeof(GameObject))
        {
            if (this.LoadedPrefab == null) this.LoadedPrefab = new Dictionary<string, GameObject>();
            if (!this.LoadedPrefab.ContainsKey(name))
            {
                GameObject go =Resources.Load<GameObject>(name);
                this.Add<string, GameObject>(this.LoadedPrefab,name, go);
            }

            return this.LoadedPrefab[name] as T;
        }
        else if (a == typeof(Sprite))
        {
            if (this.LoadedImageSprite == null) this.LoadedImageSprite = new Dictionary<string, Sprite>();
            if (!this.LoadedImageSprite.ContainsKey(name))
            {
                Sprite go = Resources.Load<Sprite>(name);
                this.Add<string,Sprite>(this.LoadedImageSprite,name, go);
            }
            return this.LoadedImageSprite[name] as T;
        }
        return null;
    }

    public T GetElseCreateT<T>(List<T> list, string name) where T:UnityEngine.Object
    {
        if (list != null && list.Count > 0)
        {
            T t = list[0];
            list.RemoveAt(0);
            return t;
        }
        else
        {
            //不存在就 创建出来
            T t = this.GetElseSetLoadedObject<T>(typeof(T), name);

            if (typeof(T) == typeof(GameObject))
            {
                GameObject go = GameObject.Instantiate(t) as GameObject;
               
                return go as T;
            }
            list.Add(t);
            return t;
        }

        //return null;
    }

    public void AddToListT<T>(T t,List<T> list) where T:UnityEngine.Object
    {
        list.Add(t);
    }

    public void RecoverBullet(GameObject go)
    {
        this.AddToListT<GameObject>(go, this.Bullets);
    }

    public void RecoverBoomEffect(GameObject go)
    {
        this.AddToListT<GameObject>(go, this.BoomEffects);
    }

    public BulletBase GetElseCreateBullet(string bulletname)
    {
        return this.GetElseCreateT<GameObject>(this.Bullets, bulletname).GetComponent<BulletBase>();
    }

    public GameObject GetElseCreateBoomEffect(string boomEffectname)
    {
        return this.GetElseCreateT<GameObject>(this.BoomEffects, boomEffectname);
    }



}

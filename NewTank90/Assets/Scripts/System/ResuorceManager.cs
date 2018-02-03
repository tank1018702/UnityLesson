using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResuorceManager :Singleton<ResuorceManager>
{

    public string RESUORCE_IMAGE_PATH = "Picture/";
    public string RESUORCE_PREFAB_APAT = "Prefabs/";
    public string RESUORCE_AUDIO_APAT = "Audio/";
    public string RESUORCE_SCENE_APAT = "Scene/";

    private ResuorcesLoadedManager LoadedResuorces;

    private ResuorceManager()
    {
        this.LoadedResuorces = ResuorcesLoadedManager.Instance;
    }

    public Sprite GetImageSpriteByName(string imagename)
    {
        Sprite spr = LoadedResuorces.GetElseSetLoadedObject<Sprite>(typeof(Sprite), RESUORCE_IMAGE_PATH + imagename); ;
        return spr;
    }

    public GameObject GetPrefabByName(string prefabename,Transform mparent = null)
    {
        GameObject go = null;
        go = LoadedResuorces.GetElseSetLoadedObject<GameObject>(typeof(GameObject), RESUORCE_PREFAB_APAT + prefabename);
        go = GameObject.Instantiate<GameObject>(go);
        if (mparent != null) go.transform.SetParent(mparent);
        return go;
    }

    public BulletBase GetElseCreateBullet(string bulletname)
    {
        return this.LoadedResuorces.GetElseCreateBullet(RESUORCE_PREFAB_APAT + bulletname);
    }

    public GameObject GetElseCreateBoomEffect(string boomEffectname)
    {
        return this.LoadedResuorces.GetElseCreateBoomEffect(RESUORCE_PREFAB_APAT + boomEffectname);
    }

    public void RecoverBullet(GameObject go)
    {
        this.LoadedResuorces.RecoverBullet(go);
    }

    public void RecoverBoomEffect(GameObject go)
    {
        this.LoadedResuorces.RecoverBoomEffect(go);
    }

}

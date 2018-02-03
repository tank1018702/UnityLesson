using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfGameManager : Singleton<SelfGameManager>
{

    private SelfGameManager()
    {
    }
    private PlayerCharacter playerone;
    private PlayerCharacter playertwo;
    private GameObject uiCanvas = null;
    public PlayerCharacter Player_One
    {
        get {
            if (this.playerone == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("PlayerOne");
                this.playerone =  go.GetComponent<PlayerCharacter>();
            }
            return this.playerone;
        }
    }
    public PlayerCharacter Player_Two
    {
        get
        {
            if (this.playertwo == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("PlayerTwo");
                this.playertwo = go.GetComponent<PlayerCharacter>();
            }
            return this.playertwo;
        }
    }

    public GameObject UICanvas
    {
        get {
            if (this.uiCanvas == null)
            {
                this.uiCanvas = GameObject.FindGameObjectWithTag("UICanvas");
            }
            return uiCanvas;
        }
    }

    private bool isTwoPlayer = false;
    /// <summary>
    /// 一个人玩 false 还是 两个人玩 true
    /// </summary>
    public bool IsTwoPlayer {

        get {
            return this.isTwoPlayer;
        }
    }

    public void SetPlayerNumber(bool enable)
    {
        this.isTwoPlayer = enable; 
    }

}

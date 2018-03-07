using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHP : MonoBehaviour
{
    public RectTransform rectBloodPos;//血条的位置 
    GameObject HPBar;//血条
    public int curHP;
    public int maxHP;
    public Slider maxHPSlider;





    // Use this for initialization
    void Awake()
    {
        GameObject go = GameObject.Find("UI");
        Object hPBarPrefab = Resources.Load("Object/Ui/HPBar", typeof(GameObject));
        HPBar = Instantiate(hPBarPrefab, go.transform) as GameObject;
        this.rectBloodPos = HPBar.GetComponent<RectTransform>();
        rectBloodPos.transform.localScale = new Vector3(.25f, .15f, 0);
        maxHPSlider = HPBar.GetComponent<Slider>();

    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //血条位置跟随
        Vector2 vec2 = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        rectBloodPos.anchoredPosition = new Vector2(vec2.x - Screen.width / 2 + 0, vec2.y - Screen.height / 2 + 62);
        Show_HP();

        Debug.Log(curHP + "/" + maxHP+":"+(curHP*1.0f)/maxHP);
    }
    //public void Cur_HP_Update()
    //{
    //   //curHP = _maxHP;
    //   //maxHP = _maxHP;
    //}

    public void Show_HP()
    {
       
        maxHPSlider.value = (curHP * 1.0f) / maxHP;
    }






}

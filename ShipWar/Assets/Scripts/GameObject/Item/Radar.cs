using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public float SpottingScope;

    public List<GameObject> Targets;

    public GameObject textobj;

    [ContextMenu("text")]
    public void Text()
    {
        Targets.Add(textobj);
    }

    private void Start()
    {

    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Missile")
        {
            Targets.Add(collision.gameObject);
        }    
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        


    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Missile")
        {
            Targets.Remove(collision.gameObject);
        }  
    }




}

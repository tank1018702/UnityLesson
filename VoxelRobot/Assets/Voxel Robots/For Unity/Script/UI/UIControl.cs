using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public Text DeathInfo;

    public GameObject Death;
    public GameObject Player;


    private void Start()
    {

        Death.SetActive(false);
    }
    private void Update()
    {
        if(!Player)
        {
            Death.SetActive(true);
            Time.timeScale = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion1 : MonoBehaviour
{
    private Timer timer;
    void Start()
    {

    }
    private void OnEnable()
    {
        timer = new Timer(2f);
        timer.OnEnd = () =>
        {
            gameObject.SetActive(false);
        };
        timer.Start();
    }

    void Update()
    {



    }
}